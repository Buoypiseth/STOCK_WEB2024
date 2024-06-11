using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace WebApplication.Models
{
    public class AutoPrint
    {
        private DataContext context;
        public AutoPrint()
        {
            this.context = new DataContext();
        }
        private static List<Stream> m_streams;
        private static int m_currentPageIndex;
        public bool PrintAuto(string name)
        {
            //var data = context.tbl_App_Setting.FirstOrDefault(x => x.Name == name);
            //if (data.Value == 1)
            //{
            //    return true;
            //}
            return false;
        }
        public string PrinterName(string paperSize = "Small")
        {
            return context.tbl_App_Setting_Options.FirstOrDefault(x => x.Name == paperSize).PrinterName; ;
        }
        public void SetPrint(LocalReport report, string ptName = "")
        {
            Export(report, ptName);
        }
        public static void Export(LocalReport report, string ptName, bool print = true)
        {
            string deviceInfo =
             @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>2.8in</PageWidth>
                <PageHeight>8.3in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
             </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;

            if (print)
            {
                Print(ptName);
            }
        }
        public static void Print(string ptName)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument pd = new PrintDocument();
            if (!pd.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                pd.PrintPage += new PrintPageEventHandler(PrintPage);
                pd.PrinterSettings.PrinterName = ptName;
                m_currentPageIndex = 0;
                pd.Print();
            }
        }
        public static Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        public static void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);
            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);
            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);
            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        public static void DisposePrint()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}