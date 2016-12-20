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
            OptIn(email);
            Delete(email);//clean up
        }
       
        private void OptIn(SubscriptionPreferences email)
        {
            var result = email.Update();

            Assert.IsTrue(String.IsNullOrEmpty(result));

            Assert.IsFalse(email.GetEmail() == null);
        }

        [TestMethod]
        public void OptOut()
        {
            var email = GetEmail(true);
            OptIn(email);

            email = GetEmail(false);

            var result = email.Update();

            Assert.IsTrue(String.IsNullOrEmpty(result));
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
