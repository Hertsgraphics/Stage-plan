using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage_Plan.Bll
{
    public class Instrument : IInstrument
    {
        public string Text { get; set; }
        public string Detail { get; set; }
        public string Src { get; set; }
        public decimal Left { get; set; }
        public decimal Top { get; set; }
        public string BandMemberName { get; set; }
        public string SelectedInstrument { get; set; }
        public bool IsFixedPosition { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Zindex { get; set; }
        public int RotateAngle { get; set; }


        /// <summary>
        /// Gets the instruments based upon the token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Instruments GetInstruments(string token)
        {
            var dc = new Dal.StageplanEntities();
            var plan = dc.Stageplans.Where(a => a.Token == token).SingleOrDefault();

            if (plan == null)
                return null;

            var result = new Instruments();
            result.BandName = plan.BandName;
            result.Height = plan.Height;
            result.Width = plan.Width;
            result.AllInstruments = new List<Instrument>();

            foreach (var item in plan.StageplanInstruments)
            {
                var detail = String.IsNullOrEmpty(item.DataDetail) ? " " : HttpContext.Current.Server.UrlDecode(item.DataDetail);

                result.AllInstruments.Add(new Instrument()
                {
                    Detail = detail,
                    Left = item.X,
                    Src = item.Src,
                    Text = String.IsNullOrEmpty(item.DataText) ? " " : item.DataText,
                    Top = item.Y,
                    BandMemberName = item.BandMember,
                    IsFixedPosition = item.IsFixedPosition,
                    Width = item.Width.HasValue ? (int)item.Width : 75,
                    Height = item.Height.HasValue ? (int)item.Height : 75,
                    Zindex = item.Zindex,
                     SelectedInstrument = item.SelectedInstrument,
                     RotateAngle = item.RotateAngle
                });
            }

            return result;
        }

        public string GetMappedPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
    }

}