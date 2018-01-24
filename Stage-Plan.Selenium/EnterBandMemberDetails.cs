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
    public class EnterBandMemberDetails : SeleniumBaseTests
    {
        private string _url = "https://stage-plan.com/Stageplan";


        [TestCleanup()]
        public void TestExit()
        {
            QuitWebDriver();
        }

        [TestInitialize()]
        public void TestInit()
        {
            Setup();
        }

        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen, band filled in but no memebers added but click continue 
        public void AddBandWithNoBandMembersInStagePlan()
        {
            WebDriver.Navigate().GoToUrl(this._url);

            IWebElement bandName = WebDriver.FindElement(By.Id("enterBandDetails_BandName"));

            bandName.SendKeys("Some Band");

            IWebElement enterBandBtn = WebDriver.FindElement(By.Id("enterBandDetails_Submit"));

            enterBandBtn.Click();

            var accept = WebDriver.SwitchTo().Alert();
            var acceptText = accept.Text;

            Assert.IsTrue(acceptText == "Please enter your band member(s).");
        }

        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen, band name is empty but no memebers added but click continue 
        public void AddEmptyBandWithNoBandMembersInStagePlan()
        {
            WebDriver.Navigate().GoToUrl(this._url);

            IWebElement bandName = WebDriver.FindElement(By.Id("enterBandDetails_BandName"));

            bandName.SendKeys("  ");

            IWebElement enterBandBtn = WebDriver.FindElement(By.Id("enterBandDetails_Submit"));

            enterBandBtn.Click();

            var accept = WebDriver.SwitchTo().Alert();
            var acceptText = accept.Text;
            Assert.IsTrue(acceptText == "Please enter band name. Please enter your band member(s).");

        }


        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen, with band name but empty members added but click continue 
        public void AddBandWithEmptyBandMembersInStagePlan()
        {
            WebDriver.Navigate().GoToUrl(this._url);

            IWebElement bandName = WebDriver.FindElement(By.Id("enterBandDetails_BandName"));

            bandName.SendKeys("Some Band");

            IWebElement bandMembers = WebDriver.FindElement(By.Id("enterBandDetails_BandMemberNames"));
            bandMembers.SendKeys("    ");

            IWebElement enterBandBtn = WebDriver.FindElement(By.Id("enterBandDetails_Submit"));

            enterBandBtn.Click();

            var accept = WebDriver.SwitchTo().Alert();
            var acceptText = accept.Text;
            Assert.IsTrue(acceptText == "Please enter your band member(s).");

        }


        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen, with band name but empty members added but click continue 
        public void AddEmptyBandAndEmptyBandMembersInStagePlan()
        {
            WebDriver.Navigate().GoToUrl(this._url);

            IWebElement bandName = WebDriver.FindElement(By.Id("enterBandDetails_BandName"));

            bandName.SendKeys(" ");

            IWebElement bandMembers = WebDriver.FindElement(By.Id("enterBandDetails_BandMemberNames"));
            bandMembers.SendKeys("    ");

            IWebElement enterBandBtn = WebDriver.FindElement(By.Id("enterBandDetails_Submit"));

            enterBandBtn.Click();

            var accept = WebDriver.SwitchTo().Alert();
            var acceptText = accept.Text;
            Assert.IsTrue(acceptText == "Please enter band name. Please enter your band member(s).");
        }

        //Currently not accurate
        //[TestMethod]
        //[TestCategory("Selenium-EnterBandMemberDetails")]
        /////The default screen, with band name but duplicate members added but click continue 
        //public void AddBandAndDuplicateBandMembersInStagePlan()
        //{
        //    WebDriver.Navigate().GoToUrl(this._url);

        //    IWebElement bandName = WebDriver.FindElement(By.Id("enterBandDetails_BandName"));

        //    bandName.SendKeys("Some Band");

        //    IWebElement bandMembers = WebDriver.FindElement(By.Id("enterBandDetails_BandMemberNames"));
        //    bandMembers.SendKeys("Andy" + Keys.Return + "Ben" + Keys.Return + "Orange" + Keys.Return + "Ben");

        //    IWebElement enterBandBtn = WebDriver.FindElement(By.Id("enterBandDetails_Submit"));

        //    enterBandBtn.Click();

        //    var accept = WebDriver.SwitchTo().Alert();
        //    var acceptText = accept.Text;
        //    Assert.IsTrue(acceptText == "Please enter your band member(s).");
        //}


        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen, no band or memebers added but click continue 
        public void AddNoBandOrBandMembersInStagePlan()
        {
            WebDriver.Navigate().GoToUrl(this._url);

            IWebElement noBandNames = WebDriver.FindElement(By.Id("enterBandDetails_Submit"));

            noBandNames.Click();

            var accept = WebDriver.SwitchTo().Alert();
            var acceptText = accept.Text;
            Assert.IsTrue(acceptText == "Please enter band name. Please enter your band member(s).");

        }



        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen when you visit stage-plan.com/stageplan
        public void StagePlanShowOptionToEnterBandNameAndMembers()
        {
            WebDriver.Navigate().GoToUrl(this._url);
            var fileName = "defaultNewStagePlan.png";
            var origFile = TestImagesPersistentPath + fileName;
            var screenshot = WebDriver.GetScreenshot();
            //screenshot.SaveAsFile(origFile, OpenQA.Selenium.ScreenshotImageFormat.Png);

            // Save screenshot if required
            var newFile = TestImagesTempForTestRunPath + fileName;
            screenshot.SaveAsFile(newFile, OpenQA.Selenium.ScreenshotImageFormat.Png);

            if (!DoFilesMatch(origFile, newFile))
            {
                SaveFailedImage(origFile, newFile, fileName);
            }
        }

        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen when you visit stage-plan.com/stageplan
        public void AddBandAndBandMembersInStagePlan()
        {
            WebDriver.Navigate().GoToUrl(this._url);

            IWebElement bandName = WebDriver.FindElement(By.Id("enterBandDetails_BandName"));

            bandName.SendKeys("Some Band");

            IWebElement bandMembers = WebDriver.FindElement(By.Id("enterBandDetails_BandMemberNames"));
            bandMembers.SendKeys("Ian");

            IWebElement enterBandBtn = WebDriver.FindElement(By.Id("enterBandDetails_Submit"));

            enterBandBtn.Click();

            var screenshot = WebDriver.GetScreenshot();

            var fileName = "defaultStagePlanAfterBandDetailsEntered.png";
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


        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///The default screen when you visit stage-plan.com/stageplan
        public void AddInstrumentAndCancelInStagePlan()
        {
            DoesMenuExpandInStagePlan();

            IWebElement showAddInstrumentLink = WebDriver.FindElement(By.Id("showAddInstrumentLink"));

            showAddInstrumentLink.Click();

            IWebElement microphone = WebDriver.FindElement(By.Id("Microphone"));

            microphone.Click();

            IWebElement microphoneName = WebDriver.FindElement(By.Id("instrumentName"));
            microphoneName.SendKeys("My Microphone");

            IWebElement addInstrumentExtraDetail = WebDriver.FindElement(By.Id("addInstrumentExtraDetail"));
            microphoneName.SendKeys("SM58");

            IWebElement cancelNewInstrumentButton = WebDriver.FindElement(By.Id("cancelEditButton"));

            cancelNewInstrumentButton.Click();


            var screenshot = WebDriver.GetScreenshot();

            var fileName = "addMicrophoneAndCancelToReturnToDefault.png";
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


        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///Enter band details,  and then test expand
        public void DoesMenuExpandInStagePlan()
        {
            AddBandAndBandMembersInStagePlan();

            IWebElement menu = WebDriver.FindElement(By.Id("lmsResponsiveMenuLink"));

            menu.Click();

            var screenshot = WebDriver.GetScreenshot();

            var fileName = "expandMenuInStagePlan.png";
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


        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///Enter band details,  and then test expand
        public void AddMicrophoneToPlan()
        {
            AddBandAndBandMembersInStagePlan();

            IWebElement menu = WebDriver.FindElement(By.Id("lmsResponsiveMenuLink"));

            menu.Click();

            IWebElement showAddInstrumentLink = WebDriver.FindElement(By.Id("showAddInstrumentLink"));

            showAddInstrumentLink.Click();

            IWebElement microphone = WebDriver.FindElement(By.Id("Microphone"));

            microphone.Click();

            IWebElement microphoneName = WebDriver.FindElement(By.Id("instrumentName"));
            microphoneName.SendKeys("My Microphone");

            IWebElement addInstrumentExtraDetail = WebDriver.FindElement(By.Id("addInstrumentExtraDetail"));
            microphoneName.SendKeys("SM58");

            IWebElement addNewInstrumentButton = WebDriver.FindElement(By.Id("addNewInstrumentButton"));

            addNewInstrumentButton.Click();


            var screenshot = WebDriver.GetScreenshot();

            var fileName = "showMicrophoneOnlyInStagePlan.png";
            var origFile = TestImagesPersistentPath + fileName;
            // screenshot.SaveAsFile(origFile, OpenQA.Selenium.ScreenshotImageFormat.Png);


            // Save screenshot if required
            var newFile = TestImagesTempForTestRunPath + fileName;
            screenshot.SaveAsFile(newFile, OpenQA.Selenium.ScreenshotImageFormat.Png);

            if (!DoFilesMatch(origFile, newFile))
            {
                SaveFailedImage(origFile, newFile, fileName);
            }
        }

        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///Enter band details,  and then test expand
        public void AddMicrophoneToPlanAndEditForDrum()
        {
            AddMicrophoneToPlan();

            IWebElement editBtn = WebDriver.FindElement(By.Id("editOnStagePlan_0"));

            editBtn.Click();

            IWebElement drum = WebDriver.FindElement(By.Id("Drums"));

            drum.Click();

            IWebElement addInstrumentExtraDetail = WebDriver.FindElement(By.Id("addInstrumentExtraDetail"));
            addInstrumentExtraDetail.SendKeys(" on the snare");

            IWebElement saveEditButton = WebDriver.FindElement(By.Id("saveEditButton"));

            saveEditButton.Click();

            var screenshot = WebDriver.GetScreenshot();

            var fileName = "showDrumOnlyInStagePlan.png";
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


        [TestMethod]
        [TestCategory("Selenium-EnterBandMemberDetails")]
        ///Enter band details,  and then test expand
        public void AddMicrophoneAndClickSave()
        {
            AddMicrophoneToPlan();

            IWebElement showSaveOptionsLink = WebDriver.FindElement(By.Id("showSaveOptionsLink"));

            showSaveOptionsLink.Click();


            //Can't save as image due to captcha code is always different

            IWebElement saveStagePlanPopUp = WebDriver.FindElement(By.Id("saveStagePlanPopUp"));
            var css = saveStagePlanPopUp.GetAttribute("class");
            Assert.IsFalse(css.Contains("hide"));
        }



    }
}
