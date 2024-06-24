using VCRevitRibbonUtil;
using Autodesk.Revit.UI;
using MUD.Properties;
using System.Resources;

namespace MUD
{
    internal class MainPanel : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            Ribbon.GetApplicationRibbon(a)
                .Tab("MUD")
                .Panel("ModelUpdate")
                .CreateButton ("Info","Утилита обновления и подгрузки акутальных моделей", typeof (Mud_start),
                              btn => btn
                                .SetLargeImage(Resources.мад32)
                                .SetSmallImage(Resources.vfl16))

                .CreateSeparator();
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication a) 
        {
            return Result.Succeeded;
        }

    }
}
