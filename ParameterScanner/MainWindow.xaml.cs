using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ParameterScanner
{
    public partial class MainWindow : Window
    {
        public UIDocument uidoc { get; }
        public Autodesk.Revit.DB.Document doc { get; }

        private ICollection<ElementId> elementIds;
        private ElementId parameterId;
        public MainWindow(UIDocument UiDoc)
        {
            uidoc = UiDoc;
            doc = UiDoc.Document;

            InitializeComponent();

            Title = "Parameter Scanner";
        }

        private void IsolateElementsInView(object sender, RoutedEventArgs e)
        {
            using (var t = new Transaction(doc, "Add filter"))
            {
                t.Start();

                // Reflect all changes before search
                doc.Regenerate();

                parameterId = SearchParameterIdBy(parameter_Name.Text);

                if (parameterId != null)
                {
                    elementIds = SearchElementsIdsBy(parameterId, parameter_Value.Text);

                    // Setting the warning message
                    var taskDialog = new TaskDialog("Elements scanned successfully")
                    {
                        MainInstruction = $"For Parameter name '{parameter_Name.Text}', value: '{parameter_Value.Text}':",

                        MainContent = $"Number of elements found: {elementIds.Count}",

                        FooterText = "Notice - Number of elements within the active view"
                    };

                    taskDialog.Show();

                    uidoc.ActiveView.IsolateElementsTemporary(elementIds);
                    
                }
                else
                {
                    TaskDialog.Show("Parameter not found", $"Parameter not found for '{parameter_Name.Text}', with value '{parameter_Value.Text}'");
                }

                t.Commit();

                Close();
            }
        }

        private void SelectElementsInView(object sender, RoutedEventArgs e)
        {
            using (var t = new Transaction(doc, "Add filter"))
            {

                t.Start();

                // Reflect all changes before search
                doc.Regenerate();

                parameterId = SearchParameterIdBy(parameter_Name.Text);

                if (parameterId != null)
                {
                    elementIds = SearchElementsIdsBy(parameterId, parameter_Value.Text);

                    // Setting the warning message
                    var taskDialog = new TaskDialog("Elements scanned successfully")
                    {
                        MainInstruction = $"For Parameter name '{parameter_Name.Text}', value: '{parameter_Value.Text}':",

                        MainContent = $"Number of elements found: {elementIds.Count}",

                        FooterText = "Notice - Number of elements within the active view"
                    };

                    taskDialog.Show();

                    uidoc.Selection.SetElementIds(elementIds);
                    
                } else
                {
                    TaskDialog.Show("Parameter not found", $"Parameter not found for parameter '{parameter_Name.Text}', with value '{parameter_Value.Text}'");
                }

                t.Commit();

                Close();
            }
        }

        private ElementId SearchParameterIdBy(String parameterName)
        {
            try
            {
                // Filter elements in active view
                var elements = new FilteredElementCollector(doc, doc.ActiveView.Id)
                    .WhereElementIsNotElementType()
                    .ToElements();

                // Filter parameter id by name specified
                var parameterId = elements
                    .SelectMany(e => e.Parameters.Cast<Parameter>())
                    .First(p => p.Definition.Name.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase))
                    .Id;

                return parameterId ?? null;

            } catch(Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }

        private ICollection<ElementId> SearchElementsIdsBy(ElementId parameterId, String parameterValue)
        {
            try
            {
                // Create a parameter value provider for the given parameter ID
                ParameterValueProvider pvp = new ParameterValueProvider(parameterId);

                // Define a string rule evaluator for equality comparison
                FilterStringRuleEvaluator fnrvStr = new FilterStringEquals();

                // Create a string rule filter based on the parameter value
                FilterStringRule paramFilter = new FilterStringRule(pvp, fnrvStr, parameterValue);

                // Create an element parameter filter using the defined string rule
                ElementParameterFilter epf = new ElementParameterFilter(paramFilter);

                // Collect elements in the active view that pass the filter
                FilteredElementCollector collectorElem = new FilteredElementCollector(doc, doc.ActiveView.Id)
                    .WherePasses(epf)
                    .WhereElementIsNotElementType();

                return collectorElem.ToElementIds();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return null;
            }
        }
    }
}
