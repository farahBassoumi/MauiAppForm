using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.Maui.Controls;
using MauiAppForm.Services;
//[assembly: Dependency(typeof(MauiAppForm.Services.PrintService))]

namespace MauiAppForm.Services
{
    public class PrintService : IPrintService
    {

        private string _printData="";

        public void Print(string text)
        {
            _printData = text;
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintPage);
            printDocument.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(_printData, new System.Drawing.Font("Arial", 12), Brushes.Black, new System.Drawing.PointF(100, 100));
        }
    }
}
