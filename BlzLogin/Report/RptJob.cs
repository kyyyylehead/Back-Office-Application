using BlzLogin.Data;
using BlzLogin.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlzLogin.Report
{
    public class RptJob
    {
        #region Declaration
        int _maxColumn = 3;
        Document _document;
        PdfPTable _pdfTable1 = new PdfPTable(1);
        PdfPTable _pdfTable3 = new PdfPTable(3);
        PdfPCell _pdfCell;
        Font _fontStyle;
        MemoryStream _memoryStream = new MemoryStream();
        Job _job = new Job();
        #endregion

        public byte[] Report(Job job)
        {
            _job = job;
            _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);
            _pdfTable1.WidthPercentage = 100;
            _pdfTable1.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfTable3.WidthPercentage = 100;
            _pdfTable3.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfTable3.SpacingAfter = 10;
            _fontStyle = FontFactory.GetFont("Tahoma", 8f, 1);
            PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();

            float[] sizes1 = new float[1];
            sizes1[0] = 300;
            _pdfTable1.SetWidths(sizes1);

            float[] sizes3 = new float[_maxColumn];
            for (var i = 0; i < _maxColumn; i++)
            {
                sizes3[i] = 100;
            }
            _pdfTable3.SetWidths(sizes3);

            this.ReportHeader();
            this.ReportBody();

            _pdfTable3.HeaderRows = 2;
            _document.Add(_pdfTable3);
            _document.Add(_pdfTable1);
            _document.Close();
            return _memoryStream.ToArray();

        }

        private void ReportHeader()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 18f, 1);
            _pdfCell = new PdfPCell(new Phrase(_job.Name, _fontStyle));
            _pdfCell.Colspan = _maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable3.AddCell(_pdfCell);
            _pdfTable3.CompleteRow();

            _pdfCell = new PdfPCell(new Phrase(_job.SuperiorWorkOrderNumber.ToString(), _fontStyle));
            _pdfCell.Colspan = _maxColumn;
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.Border = 0;
            _pdfCell.ExtraParagraphSpace = 0;
            _pdfTable3.AddCell(_pdfCell);
            _pdfTable3.CompleteRow();
        }

        private void ReportBody()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            var fontStyle = FontFactory.GetFont("Tahoma", 9f, 0);

            #region Table Header 1
            _pdfCell = new PdfPCell(new Phrase("Client Name", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Superior Work Order #", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Client Order #", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);
            #endregion

            #region Table Body 1
            _pdfCell = new PdfPCell(new Phrase(_job.Name, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.SuperiorWorkOrderNumber.ToString(), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.CustomerOrderNumber.ToString(), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfTable3.CompleteRow();
            #endregion

            #region Table Header 2
            _pdfCell = new PdfPCell(new Phrase("Date Recieved", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Reciever", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Customer Part #", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);
            #endregion

            #region Table Body 2
            _pdfCell = new PdfPCell(new Phrase(_job.Date.ToString(), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.JobReciever, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.CustomerPartNumber.ToString(), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfTable3.CompleteRow();
            #endregion

            #region Table Header 3
            _pdfCell = new PdfPCell(new Phrase("Quantity/Size", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("ITAR Required?", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("HOT?", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);
            #endregion

            #region Table Body 3
            _pdfCell = new PdfPCell(new Phrase(_job.Quantity + " / " + _job.Size, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(ToYesOrNo(_job.ITAR), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(ToYesOrNo(_job.HOT), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfTable3.CompleteRow();
            #endregion

            #region Table Header 4
            _pdfCell = new PdfPCell(new Phrase("Metal", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Minimum Thickness", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Maximum Thichness", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);
            #endregion

            #region Table Body 4
            _pdfCell = new PdfPCell(new Phrase(_job.Metal, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.ThicknessMin.ToString(), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.ThicknessMax.ToString(), fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfTable3.CompleteRow();
            #endregion

            #region Table Header 5
            _pdfCell = new PdfPCell(new Phrase("Serial Numbers", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Shipping Carrier", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase("Shipping Speed", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable3.AddCell(_pdfCell);
            #endregion

            #region Table Body 5
            _pdfCell = new PdfPCell(new Phrase(_job.SerialNumbers, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.ShippingCarrier, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfCell = new PdfPCell(new Phrase(_job.ShippingSpeed, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable3.AddCell(_pdfCell);

            _pdfTable3.CompleteRow();
            #endregion

            #region Table Header 5
            _pdfCell = new PdfPCell(new Phrase("Overall Requirements", _fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.Gray;
            _pdfTable1.AddCell(_pdfCell);

            #endregion

            #region Table Body 5
            _pdfCell = new PdfPCell(new Phrase(_job.OverallRequirements, fontStyle));
            _pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfCell.BackgroundColor = BaseColor.White;
            _pdfTable1.AddCell(_pdfCell);

            _pdfTable1.CompleteRow();
            #endregion
        }

        public static string ToYesOrNo(bool value)
        {
            return value ? "Yes" : "No";
        }
    }
}
