
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using mud;



namespace MUD
{
    [Transaction(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class Mud_start : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
          

            var Window = new MUDView();
            ViewModel viewModel = new ViewModel();

            Window.Show();

            return Result.Succeeded;
        }
        static public class CommonData
        {
            public static ExternalCommandData CommandData;
            public static Document docum;

        }
    }
    }   
    




