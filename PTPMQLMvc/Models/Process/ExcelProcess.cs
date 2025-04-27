using System.Data;
using System.Xml;
using OfficeOpenXml;
using PTPMQLMvc.Models.Process;

namespace PTPMQLMvc.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string strPath)
        {
            FileInfo fi = new FileInfo(strPath);
            ExcelPackage excelPackage = new ExcelPackage(fi);
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            // Check if the worksheet is completely empty
            if (worksheet.Dimension == null)
            {
                return dt;
            }
            // Create a list to hold the column names
            List<string> columnNames = new List<string>();
            // Needed to keep track of empty column headers
            int currentColumn = 1;
            // Loop all columns in the sheet and add them to the datatable
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();
                // Check if the previous header was empty and add it if it was
                if (cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }
                // Add the column name to the list to count the duplicates
                columnNames.Add(columnName);
                // Count the duplicate column names and make them unique to avoid the exception
                // A column named 'Name' already belongs to this DataTable
                int occurrences = columnNames.Count(x => x.Equals(columnName));
                if (occurrences > 1)
                {
                    columnName = columnName + "_" + occurrences;
                }
                // Add the column to the datatable
                dt.Columns.Add(columnName);
                currentColumn++;
            }

            // Start adding the contents of the excel file to the datatable
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                // Loop all cells in the row
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
    }
}
