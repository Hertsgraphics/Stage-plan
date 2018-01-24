using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stage_Plan.Dal;

namespace RemoveOldPosts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Removing any mail lists which are not confirmed and over 28 days old");

            var monthsOld = DateTime.Now.AddDays(-28);

            var dc = new Stage_Plan.Dal.StageplanEntities();
            foreach (var mail in dc.MailingLists)
            {
                if (mail.IsConfirmed)
                    continue;

                if (mail.DateOptInRequest < monthsOld)
                    dc.MailingLists.Remove(mail);
            }

            try
            {
                dc.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed...");
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
