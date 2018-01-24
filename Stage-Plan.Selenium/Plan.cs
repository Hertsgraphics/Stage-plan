using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Drawing.Imaging;
using OpenQA.Selenium.Support.UI;
using System.Security.Cryptography;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;

namespace Stage_Plan.Selenium
{

    [TestClass]
    public class Plan : SeleniumBaseTests
    {
        private string _url = "https://stage-plan.com/Stageplan/plan/test";

        //[TestCleanup()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //    CloseWebBrowser();
        //}


        [TestCleanup()]
        public void TestCleanup()
        {
            QuitWebDriver();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            Setup();
        }

        [TestMethod]
        [TestCategory("Selenium-Plan")]
        ///Test file, try to open expand
        public void DoesMenuExpandInPlan()
        {
            WebDriver.Navigate().GoToUrl(_url);

            System.Threading.Thread.Sleep(1000);

            var accept = WebDriver.SwitchTo().Alert();
            accept.Accept();

            IWebElement menu = WebDriver.FindElement(By.Id("lmsResponsiveMenuLink"));

            menu.Click();
            System.Threading.Thread.Sleep(500);

            var screenshot = WebDriver.GetScreenshot();
            
            var fileName = "expandMenuInPlan.png";
            var origFile = TestImagesPersistentPath + fileName;
            //screenshot.SaveAsFile(origFile, OpenQA.Selenium.ScreenshotImageFormat.Png);


            // Save screenshot if required
            var newFile = TestImagesTempForTestRunPath + fileName;
            screenshot.SaveAsFile(newFile, OpenQA.Selenium.ScreenshotImageFormat.Png);

            if (!DoFilesMatch(origFile, newFile))
            {
                SaveFailedImage(origFile, newFile, fileName);
            }
        }


    }
}
