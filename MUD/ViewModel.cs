using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.IFC;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace mud
{
    [Transaction(TransactionMode.Manual)]
    public class ViewModel : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            // URL-адреса файлов IFC, которые необходимо загрузить
            List<string> ifcUrls = new List<string>
        {
            "https://disk.yandex.ru/d/iczMW0p9pQ8VbQ",
          
        };

            // Временная директория хранения загруженных файлов
            string tempDirectory = Path.GetTempPath();
            List<string> downloadedFiles = new List<string>();

            using (WebClient client = new WebClient())
            {
                foreach (string url in ifcUrls)
                {
                    string fileName = Path.GetFileName(url);
                    string localPath = Path.Combine(tempDirectory, fileName);

                    try
                    {
                        client.DownloadFile(url, localPath);
                        downloadedFiles.Add(localPath);
                    }
                    catch (Exception ex)
                    {
                        message = $"Error downloading {url}\\n{ex.Message}";
                        return Result.Failed;
                    }
                }
            }
            // импорт IFC в rvt
            IFCImportOptions importOptions = new IFCImportOptions();

            using (Transaction trans = new Transaction(doc, "Import IFC files"))
            {
                trans.Start();
                foreach (string filePath in downloadedFiles)
                {
                    try
                    {
                        doc.Import(filePath, importOptions, doc.ActiveView);
                    }
                    catch (Exception ex)
                    {
                        message = $"Error importing {filePath}\\n{ex.Message}";
                        trans.RollBack();
                        return Result.Failed;
                    }
                }
                trans.Commit();
            }

            // очистка временной директории
            foreach (string file in downloadedFiles)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    // Log cleanup error, but do not fail the command
                    TaskDialog.Show("Cleanup Error", $"Failed to delete temporary file {file}\\n{ex.Message}");
                }
            }

            return Result.Succeeded;
        }
    }
}