using OfficeOpenXml;
using Excel = OfficeOpenXml.ExcelPackage;
using System.Collections.Generic;
using System.IO;

namespace Lab2
{
    class ExcelDocumentCreator<T, P>
    {
        public Excel pack;

        public ExcelDocumentCreator(FileInfo fileInfo)
        {
            Excel.LicenseContext = LicenseContext.NonCommercial;
            pack = new Excel(fileInfo);
        }

        public void createWorksheet(string name)
        {
            if (pack.Workbook.Worksheets[name] == null)
                pack.Workbook.Worksheets.Add(name);
        }

        public int addValuesFromDict(Dictionary<T, P> dict, string worksheet, int rowFirst)
        {
            int col = 1;
            foreach (KeyValuePair<T, P> x in dict)
            {
                pack.Workbook.Worksheets[worksheet].Cells[rowFirst == 0 ? 1 : rowFirst, col].Value = x.Key;
                pack.Workbook.Worksheets[worksheet].Cells[rowFirst == 0 ? 2 : rowFirst + 1, col++].Value = x.Value;
            }
            return col;
        }

        public void SaveAndClose()
        {
            pack.Save();
            pack.Dispose();
        }
    }
}
