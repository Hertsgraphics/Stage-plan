using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage_Plan.Ui.Models.Login
{
    public class Login
    {
        public string Username { get;  set; }
        public string Password { get;  set; }

        public Login()
        {
        }

        public bool IsValid()
        {
            if (String.IsNullOrEmpty(this.Username) || String.IsNullOrEmpty(this.Password))
                return false; 

            return this.Username.ToLower() == this._validUsr && this.Password == this._validPwrd;
        }

        private string _validPwrd = "Unic0rn5";
        private string _validUsr = "unicorn";
    }
}