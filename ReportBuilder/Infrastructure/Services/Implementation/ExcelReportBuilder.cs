
using System;
using System.Collections.Generic;
using System.Linq;
using ReportBuilder.Infrastructure.Services.Abstract;
using ReportBuilder.ViewModels;
using Microsoft.Office.Interop.Excel;

namespace ReportBuilder.Infrastructure.Services.Implementation
{
    public class ExcelReportBuilder : IReportBuilder
    {
        private Microsoft.Office.Interop.Excel.Application xlApp;

        private Workbook xlWorkBook;

        public string CreateReportFile(IEnumerable<OrderViewModel> orders)
        {
            xlApp = new Application();

            if (xlApp == null)
            {
                throw new ReportBuilderException("Excel is not installed on the server");
            }

            xlWorkBook = xlApp.Workbooks.Add(1);
            var xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Cells[1, 1] = "Order ID";
            xlWorkSheet.Cells[1, 2] = "Order Date";
            xlWorkSheet.Cells[1, 3] = "Product name";
            xlWorkSheet.Cells[1, 4] = "Quantity";
            xlWorkSheet.Cells[1, 5] = "Unit price";
            xlWorkSheet.Cells[1, 6] = "Total price";

            //Range rng = xlWorkSheet.Range[2, 6]; //.Range["F2"];
            var countOfOrders = orders.Count();

            for (int i = 0; i < countOfOrders; i++)
            {
                Range cells = xlWorkSheet.Range[xlWorkSheet.Cells[2 + i, 6], xlWorkSheet.Cells[2 + countOfOrders, 6]];
                string cellRefQuantity = string.Format("D{0}", 2 + i);
                string cellRefPrice = string.Format("E{0}", 2 + i);
                cells.FormulaR1C1 = string.Format("={0}*{1}", cellRefQuantity, cellRefPrice);
            }
            string fileName = "C://Users/Public/Report.xls";
            
            xlWorkBook.SaveAs(fileName);

            return fileName;
        }

        public void DeleteLastReportFile()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (xlWorkBook != null)
            {
                xlWorkBook.Close();
                xlWorkBook = null;
            }

            if (xlApp != null)
            {
                xlApp.Quit();
                xlApp = null;
            }
        }
    }
}