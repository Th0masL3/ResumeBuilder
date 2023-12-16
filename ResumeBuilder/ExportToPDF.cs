using Microsoft.Win32;
using PdfSharp.Drawing.Layout;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;

namespace ResumeBuilder
{
    internal class ExportToPDF
    {
        public static void exportToPDF(DatabaseHandler dbHandler)
        {
            // Create a PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Resume PDF Document";

            // Create an empty page
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            // Get XGraphics
            XGraphics gfx = XGraphics.FromPdfPage(page);

           // Create fonts 
           // XFont fontTitle = new XFont("Verdana", 22, XFontStyle.BoldItalic);
           // XFont fontBody = new XFont("Times New Roman", 12, XFontStyle.Regular);


            //We had a problem with the package so the fonts didn't where. Therefore, there's nothing displayed in the pdf file


            // Create XTextFormatter
            XTextFormatter tf = new XTextFormatter(gfx);

            // Title
            XRect rect = new XRect(0, 10, page.Width, 40);
            tf.Alignment = XParagraphAlignment.Center;
           // tf.DrawString("Resume", fontTitle, XBrushes.Black, rect);

            // Fetch data from database
            var personalInfos = dbHandler.ReadAllPersonalInfo();
            var contactInfos = dbHandler.ReadAllContactInfo();
            var educations = dbHandler.ReadAllEducation();
            var references = dbHandler.ReadAllReferences();
            var workExperiences = dbHandler.ReadAllWorkExperiences();

            // Combine data into a single string
            string text = "Personal Information:\n";
            foreach (var info in personalInfos)
            {
                text += $"Name: {info.Name}, Email: {info.Email}, Phone: {info.Phone}\n";
            }
            text += "\nContact Information:\n";
            foreach (var info in contactInfos)
            {
                text += $"Description: {info.Description}, Telephone: {info.Telephone}, Address: {info.Address}\n";
            }

            // Write data to PDF
            rect = new XRect(10, 60, page.Width - 20, page.Height - 70);
            tf.Alignment = XParagraphAlignment.Left;
            //tf.DrawString(text, fontBody, XBrushes.Black, rect);

            // Save the document
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files(*.pdf)|*.pdf|All files(*.*)|(*.*)";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = "Resume.pdf";
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                document.Save(saveFileDialog.FileName);
            }
        }
    }
}
    
    

   
