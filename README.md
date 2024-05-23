![alt text](ParameterScanner/Resources/scanner_icon.ico)
# Parameter Scanner Add-in for Revit

## Project Description

The Parameter Scanner add-in for Revit 2020 is designed to enhance the model review process by allowing users to scan model elements for specific parameter values. This tool provides the functionality to isolate or select elements based on their parameter status and the user who last modified them.

## Features

- **Ribbon Tab**: The add-in introduces a new Ribbon tab named "Parameters".
- **Parameter Scanner Button**: Within the Parameters tab, a button labeled "Parameter Scanner" opens a form.
- **Search Form**: The form allows users to specify a parameter name and value to search within the model elements. It also supports searching for elements with empty parameter values if the parameter value field is left blank.
- **UI/UX**: The form is designed to be intuitive, featuring interaction buttons to either isolate or select the identified elements.

## Setup Instructions

### Prerequisites

- **Revit 2020**: Ensure you have Revit 2020 installed.
- **.NET Framework 4.8**: The add-in is developed using .NET Framework 4.8.
- **Visual Studio 2019 or 2022**: You will need either of these versions of Visual Studio to build the project.

### Installation

1. **Clone the Repository**: Clone the project repository to your local machine.
    ```bash
    git clone https://github.com/viniciusmtsantos/cs-parameter_scanner.git
    ```
2. **Open in Visual Studio**: Open the project solution file (`ParameterScanner.sln`) in Visual Studio 2019 or 2022.
3. **Restore NuGet Packages**: Restore the required NuGet packages by right-clicking on the solution in the Solution Explorer and selecting "Restore NuGet Packages".
4. **Build the Solution**: Build the solution to compile the add-in. Ensure the build configuration is set to "Release".
5. **Add-in Manifest File**: Create an add-in manifest file (`ParameterScanner.addin`) and place it in the Revit Add-ins folder (`C:\ProgramData\Autodesk\Revit\Addins\2020`). Here is an example of what the manifest file might look like:

    ```xml
    <?xml version="1.0" encoding="utf-8" standalone="no"?>
    <RevitAddIns>
        <AddIn Type="Application">
            <Name>Parameter Scanner</Name>
            <Assembly>C:\Path\To\Your\Addin\ParameterScanner.dll</Assembly>
            <AddInId>GUID-GOES-HERE</AddInId>
            <VendorId>YourCompany</VendorId>
            <VendorDescription>Your Company Description</VendorDescription>
        </AddIn>
    </RevitAddIns>
    ```

### Usage

1. **Launch Revit 2020**: Open Revit 2020.
2. **Access the Add-in**: Navigate to the newly added "Parameters" tab in the Ribbon.
3. **Open Parameter Scanner**: Click the "Parameter Scanner" button to open the search form.
4. **Specify Search Parameters**: Enter the parameter name and value to search for. Leave the value field blank to search for elements with empty parameter values.
5. **Perform Actions**: Use the buttons in the form to isolate or select the identified elements.

## Contributing

We welcome contributions to the Parameter Scanner project. Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contact

For any questions or support, please contact [Vinicius_Santos] at [viniciusmtsantos@gmail.com].
