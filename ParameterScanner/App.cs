using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace ParameterScanner
{
    public class App : IExternalApplication
    {
        const string RIBBON_TAB = "Parameters";
        const string RIBBON_PANEL = "Parameter Scanner";

        public Result OnStartup(UIControlledApplication application)
        {

            String assemblyName = Assembly.GetExecutingAssembly().Location;
            String path = System.IO.Path.GetDirectoryName(assemblyName);

            // Create the ribbon tab
            application.CreateRibbonTab(RIBBON_TAB);

            // Create / Get the ribbon panel
            RibbonPanel panel = null;
            List<RibbonPanel> panels = application.GetRibbonPanels(RIBBON_TAB);

            foreach(RibbonPanel pnl in panels)
            {
                if (panel.Name == RIBBON_PANEL)
                {
                    panel = pnl;
                    break;
                }
            }

            if (panel == null)
            {
                panel = application.CreateRibbonPanel(RIBBON_TAB, RIBBON_PANEL);
            }

            // Create the PushButton data
            PushButtonData btnData = new PushButtonData(
                "ParameterScanner",
                "Parameter Scanner",
                assemblyName,
                "ParameterScanner.Command"
                )
            {

                // Add tooltip
                ToolTip = "Configure the parameter name and value to analyze your parameters" ,

                // Add image
                Image = new BitmapImage(new Uri(path + @"\scanner_icon.ico")),
                LargeImage = new BitmapImage(new Uri(path + @"\scanner_icon.ico")),

            };

            // Create the btn in panel
            PushButton button = panel.AddItem(btnData) as PushButton;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Failed;
        }

    }
}
