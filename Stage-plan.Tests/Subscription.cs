using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stage_plan.Bll;

namespace Stage_plan.Tests
{
    [TestClass]
    public class Subscription
    {
        [TestMethod]
        public void OptIn()
        {
            var email = GetEmail(true);
            Save(email);
            Delete(email);//clean up
        }

        private void Save(SubscriptionPreferences email)
        {
            var result = email.Update();

            Assert.IsTrue(String.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void OptOut()
        {
            var email = GetEmail(true);
            Save(GetEmail(true));//opt in first so we can unsubscribe later
            email.IsOptIn = false;
            Save(email);//attempt opt out

            var emailFromDb = new Bll.SubscriptionPreferences().GetEmailByAddress(email.EmailAddress);

            Assert.IsTrue(emailFromDb.IsOptIn);

            Delete(email);//clean up
        }

        [TestMethod]
        public void OptInFromOptOut()
        {
        }

        [TestMethod]
        public void OptOutWithUnrecognisedEmailAddress()
        {
        }

        [TestMethod]
        public void OptOutWithIncompleteModel()
        {
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
