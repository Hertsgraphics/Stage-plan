using System;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.IO;

namespace Stage_Plan.Bll.PdfWrapper
{
    public class Pdf
    {

        private double _pageWidth;
        private double _pageHeight;
        private readonly int _top = 40;
        public byte[] Get(Instruments instruments, bool isLive)
        {
            var document = new PdfDocument();

            Begin(document, instruments, isLive);

            var workStream = new MemoryStream();
            document.Save(workStream, false);
            byte[] byteInfo = workStream.ToArray();
            document.Close();

            return byteInfo;
        }

        public bool Create(IInstruments instruments, bool isLive)
        {
            var document = new PdfDocument();
            Begin(document, instruments, isLive);

            string filename = instruments.BandName + "_" + DateTime.Now.ToFileTimeUtc() + ".pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
            document.Close();

            return false;
        }

        private void Begin(PdfDocument document, IInstruments instruments, bool isLive)
        {
            SetTitle(document, instruments.BandName);
            SetPageProperties(instruments.Width, instruments.Height);
            AddPlan(document, instruments.Height, instruments.Width, instruments, isLive);
            AddDetails(document, instruments);
        }

        private void SetTitle(PdfDocument document, string bandName)
        {
            document.Info.Title = bandName + " Created by Stage Plan - https://stage-plan.com";
        }

        private void AddDetails(PdfDocument document, IInstruments instruments)
        {
            var gfx = GetGraphics(document);

            var font = new XFont("Verdana", 12, XFontStyle.Bold);
            var fontDetail = new XFont("Verdana", 10, XFontStyle.Regular);

            gfx.DrawString("Band Name: " + instruments.BandName, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 15, 25);

            var top = this._top;
            var secondColumn = 250;
            var currentMember = "";
            foreach (var instrument in instruments.AllInstruments)
            {
                if (currentMember != instrument.BandMemberName)
                {
                    top = top + 25;
                    gfx.DrawLine(new XPen(XColor.FromArgb(0, 0, 0)), 15, secondColumn, 16, secondColumn);
                    gfx.DrawString("Band member", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 15, top);
                    gfx.DrawString(instrument.BandMemberName, font, new XSolidBrush(XColor.FromArgb(0, 0, 0)), secondColumn, top);
                    top = top + 20;
                }

                currentMember = instrument.BandMemberName;

                gfx.DrawString("Equipment", fontDetail, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 15, top);

                gfx.DrawString(instrument.Text, fontDetail, new XSolidBrush(XColor.FromArgb(0, 0, 0)), secondColumn, top);

                if (!String.IsNullOrWhiteSpace(instrument.Detail))
                {
                    gfx.DrawString("Extra Detail", fontDetail, new XSolidBrush(XColor.FromArgb(0, 0, 0)), 15, top + 15);
                    gfx.DrawString(instrument.Detail, fontDetail, new XSolidBrush(XColor.FromArgb(0, 0, 0)), secondColumn, top + 15);
                }
                else
                {
                    top -= 20;
                }

                top += 35;

                if (top > gfx.PageSize.Height - 50)
                {
                    top = this._top + 40;
                    AddStageplanFooter(gfx, font, instruments.Width, instruments.Height);
                    gfx = GetGraphics(document);
                }
            }

            AddStageplanFooter(gfx, font, instruments.Width, instruments.Height);

        }

        private void AddStageplanFooter(XGraphics gfx, XFont font, decimal width, decimal height)
        {
            gfx.DrawString("https://stage-plan.com", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)),
     (int)width / 6 * 5, (int)height - 50, XStringFormats.BottomCenter);
        }

        private XGraphics GetGraphics(PdfDocument document)
        {

            if (this._pageHeight <= 0.00 || this._pageWidth <= 0.00)
                throw new Exception("Unexpected dimensions for stage plan.");

            var page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Height = this._pageHeight;
            page.Width = this._pageWidth;

            return XGraphics.FromPdfPage(page);
        }

        private void AddPlan(PdfDocument document, decimal stageHeight, decimal stageWidth, IInstruments instruments, bool isLive)
        {
            var gfx = GetGraphics(document);
            // Get an XGraphics object for drawing

            // Create a font
            var font = new XFont("Verdana", 12, XFontStyle.Bold | XFontStyle.Underline);

            foreach (var instrument in instruments.AllInstruments)
            {
                var path = "";
                if (!isLive)
                {
                    if (Environment.UserDomainName == "MR-TEE")
                        path = @"D:\Projects\Stageplan\stageplan\Stage-Plan.Ui\Content\Stageplan\Images\";
                    else
                        path = @"D:\GitHub\Stageplan\stageplan\Stage-Plan.Ui\Content\Stageplan\Images\";

                    path += Path.GetFileName(instrument.Src);
                }
                else
                {
                    path = instrument.GetMappedPath(new Uri(instrument.Src).AbsolutePath);
                }

                var img = XImage.FromFile(path);
                gfx.DrawImage(img, (int)instrument.Left, (int)instrument.Top, 60.00, 60.00);

                var fontInstrument = new XFont("Verdana", 10, XFontStyle.Regular);

                gfx.DrawString(instrument.Text, fontInstrument, new XSolidBrush(XColor.FromArgb(0, 0, 0)),
             (int)instrument.Left + 30, (int)instrument.Top + 75, XStringFormats.BottomCenter);
            }

            gfx.DrawString("Front of Stage. . .  https://stage-plan.com ", font, new XSolidBrush(XColor.FromArgb(0, 0, 0)),
         (int)instruments.Width / 2, (int)instruments.Height - 50, XStringFormats.BottomCenter);

        }

        private void SetPageProperties(decimal width, decimal height)
        {
            this._pageHeight = Convert.ToDouble(height);
            this._pageWidth = Convert.ToDouble(width);
        }
    }
}
