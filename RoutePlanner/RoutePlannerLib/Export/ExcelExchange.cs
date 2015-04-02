using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Export
{
    public class ExcelExchange
    {
        public void WriteToFile(String fileName, City from, City to, List<Link> links)
        {

            Excel.Application xlApp = new Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }
            xlApp.Visible = true;

            Excel.Workbook wb = xlApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];

            if (ws == null)
            {
                Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
            }

            // Select the Excel cells, in the range c1 to c4 in the worksheet.
            Excel.Range aRange = ws.get_Range("C1", "C4");

            if (aRange == null)
            {
                Console.WriteLine("Could not get a range. Check to be sure you have the correct versions of the office DLLs.");
            }

            //Add table headers going cell by cell.
            ws.Cells[1, 1] = "From";
            ws.Cells[1, 2] = "To";
            ws.Cells[1, 3] = "Distance";
            ws.Cells[1, 4] = "Transport Mode";

            //Format A1:D1 as bold, font size 14 and vertical alignment = center.
            ws.get_Range("A1", "D1").Font.Bold = true;
            ws.get_Range("A1", "D1").Font.Size = 14;
            ws.get_Range("A1", "D1").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            ws.get_Range("A1", "D1").Borders.Weight = Excel.XlBorderWeight.xlThin;
            ws.get_Range("A1", "D1").VerticalAlignment =
                Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            
            // Fill the cells in the C1 to C4 range of the worksheet with the parameters.
            Object[] args = new Object[1];
            args[0] = from.Name;
            args[1] = to.Name;
            args[2] = from.Location.Distance(to.Location).ToString();
            args[3] = new Link(from, to, from.Location.Distance(to.Location)).TransportMode.ToString();
            
           /* aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);*/
    
            
            xlApp.DisplayAlerts = false;
            wb.SaveAs(fileName);
        

        }
    }
}
