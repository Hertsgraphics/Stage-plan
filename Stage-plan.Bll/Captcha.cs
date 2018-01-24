using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public class Captcha : ICaptcha
    {


        public string CaptchaCode { get; set; }

        public string CaptchaPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFile">Only the name of the captcha file, not the path</param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool IsValid(ICaptcha model)
        {
            var path = Path.GetFileName(model.CaptchaPath);
            if (String.IsNullOrEmpty(path))
                return false;
            if (String.IsNullOrEmpty(model.CaptchaCode))
                return false;
            switch (path.ToUpper())
            {
                case "CAPTCHA-01.JPG":
                    return model.CaptchaCode.ToUpper() == "69JLPD";
                case "CAPTCHA-02.JPG":
                    return model.CaptchaCode.ToUpper() == "G5HIJM";
                case "CAPTCHA-03.JPG":
                    return model.CaptchaCode.ToUpper() == "1B2B94";
                case "CAPTCHA-04.JPG":
                    return model.CaptchaCode.ToUpper() == "ASD&56";
                case "CAPTCHA-05.JPG":
                    return model.CaptchaCode.ToUpper() == "ASA9B2";
                case "CAPTCHA-06.JPG":
                    return model.CaptchaCode.ToUpper() == "1792BR";
                default:
                    return false;
            }
        }
    }
}
