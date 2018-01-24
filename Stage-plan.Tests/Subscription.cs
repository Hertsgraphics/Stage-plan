using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Stage_Plan.Bll;

namespace Stage_Plan.Tests
{
    [TestClass]
    public class Subscription
    {
        [TestMethod]
        public void OptIn()
        {
            var email = GetEmail(true);
            var result = email.Update();
            Assert.IsTrue(String.IsNullOrEmpty(result));
            Delete(email);//clean up
        }


        [TestMethod]
        public void OptInThenOptOut()
        {
            var email = GetEmail(true);
            var result = email.Update();
            Assert.IsTrue(String.IsNullOrEmpty(result));

            email = GetEmail(false);
            result = email.Update();//attempt opt out
            Assert.IsTrue(String.IsNullOrEmpty(result));//expected no error message as there is an email to opt out
            
            var emailFromDb = new Bll.SubscriptionPreferences().GetEmailByAddress(email.EmailAddress);

            Assert.IsFalse(emailFromDb.IsOptIn);

            Delete(email);//clean up if needed - it shouldn't be as entry should not be saved to database

        }


        [TestMethod]
        public void OptOutOnly()
        {
            var email = GetEmail(false);
            var result = email.Update();//attempt opt out

            Assert.IsTrue(!String.IsNullOrEmpty(result));//expected error message

            Delete(email);//clean up if needed - it shouldn't be as entry should not be saved to database
        }

        [TestMethod]
        public void OptInFromOptOut()
        {
            //opt out first so we can re join later
            var optIn = GetEmail(false);
            var result = optIn.Update();
            Assert.IsTrue(!String.IsNullOrEmpty(result));

            //now attempt to opt in
            var email = GetEmail(true);
            result = email.Update();
            Assert.IsTrue(String.IsNullOrEmpty(result));
            
            var emailFromDb = new Bll.SubscriptionPreferences().GetEmailByAddress(email.EmailAddress);

            Assert.IsTrue(emailFromDb.IsOptIn);

            Delete(email);//clean up
        }

        [TestMethod]
        public void OptOutWithUnrecognisedEmailAddress()
        {
            var optin = GetEmail(true);
            var result = optin.Update();
            Assert.IsTrue(String.IsNullOrEmpty(result));

            var email = GetEmail(false);
            email.EmailAddress = email.EmailAddress + "asdf";
            result = email.Update();
            Assert.IsTrue(!String.IsNullOrEmpty(result));//expecting error message
            
            Delete(optin);//clean up
        }

        [TestMethod]
        public void OptOutWithIncompleteModel()
        {
            var optin = GetEmail(true);
            optin.EmailAddress = "";
            var result = optin.Update();

            Assert.IsTrue(!String.IsNullOrEmpty(result));//expecting error message

            Delete(optin);//clean up
        }

        private Bll.SubscriptionPreferences GetEmail(bool isOptIn)
        {
            return new Bll.SubscriptionPreferences()
            {
                EmailAddress = "dave@stage-plan-test.co.uk",
                Name = "My test name",
                IsOptIn = isOptIn
            };
        }

        private bool Delete(Bll.SubscriptionPreferences email)
        {

            Dal.StageplanEntities dc = new Dal.StageplanEntities();
            var ml = dc.MailingLists.Where(a => a.EmailAddress == email.EmailAddress).SingleOrDefault();

            if (ml != null)
            {
                try
                {
                    dc.MailingLists.Remove(ml);
                    dc.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
            return false;
        }
    }
}
