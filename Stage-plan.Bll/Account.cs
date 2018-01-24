using Stage_Plan.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public class Account: DataAccess, IAccount
    {
        public string EmailAddress { get; set; }
        public string TempPassword { get; set; }
        public bool IsEnabled { get; set; }
        public int VenueTemplateLimit { get; set; }
        public int Id { get; set; }

        public IAccount Save(out string message)
        {
            message = String.Empty;

            message = ValidateForSave();

            if (!String.IsNullOrEmpty(message))
                return null;

            var account = new Dal.Account()
            {
                EmailAddress = this.EmailAddress,
                TempPassword = this.TempPassword,
                IsEnabled = true,
                VenueTemplateLimit = 1                                
            };

            base.DataContext.Accounts.Add(account);
            if (base.SaveToDatabase())
            {
                return account;
            }

            if (base.DataContext.Accounts.Any(a => a.EmailAddress == this.EmailAddress))
                message = "The email address is already in use. ";

            return null;
        }

        private string ValidateForSave()
        {
            var message = String.Empty;
            if (String.IsNullOrEmpty(this.TempPassword))
                message += "Please enter a password. ";

            if (String.IsNullOrEmpty(this.EmailAddress))
                message += "Please enter an email address. ";

            return message;
        }

        internal void Delete(int accountId)
        {
            var account = base.DataContext.Accounts.SingleOrDefault(a => a.Id == accountId);
            if (account != null)
            {
                base.DataContext.Accounts.Remove(account);
                base.SaveToDatabase();
            }
        }
    }
}
