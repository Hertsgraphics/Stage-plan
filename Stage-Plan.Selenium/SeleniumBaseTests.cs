using System;
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
    //[DeploymentItem("Dependencies\\IEDriverServer.exe")]
    public class SeleniumBaseTests
    {
        protected static ChromeDriver WebDriver;
        protected static string TestImagesPersistentPath;
        protected static string TestImagesTempForTestRunPath;
        private static string _testImagesTempForComparison;
        private static string _failFolderPath;
        private static string _tidyExePath;
        private static bool _wasFail;
        private static List<string> _imageFails;
        private static bool _isSetup = true;
        
        protected static void Setup()
        {
            _imageFails = new List<string>();

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--window-size=768,1024");
            var domain = Environment.MachineName;
            if (domain == "DAVE-LAPTOP")
            {
                WebDriver = new ChromeDriver(@"D:\GitHub\Stageplan\stageplan\Stage-Plan.Selenium\drivers\");
                TestImagesPersistentPath = @"D:\GitHub\Stageplan\stageplan\Stage-Plan.Selenium\TestImages\TestImagesPersistent\";
                TestImagesTempForTestRunPath = @"D:\GitHub\Stageplan\stageplan\Stage-Plan.Selenium\TestImages\SeleniumTemp\";
                _failFolderPath = @"D:\GitHub\Stageplan\stageplan\Stage-Plan.Selenium\Failures\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
                _tidyExePath = @"D:\GitHub\Stageplan\stageplan\Stage-Plan.Selenium.PostTests\bin\Release\Stage-Plan.Selenium.PostTests.exe";
            }
            else
            {
                WebDriver = new ChromeDriver(@"D:\Projects\Stageplan\stageplan\Stage-Plan.Selenium\drivers\");
                TestImagesPersistentPath = @"D:\Projects\Stageplan\stageplan\Stage-Plan.Selenium\TestImages\TestImagesPersistent\";
                TestImagesTempForTestRunPath = @"D:\Projects\Stageplan\stageplan\Stage-Plan.Selenium\TestImages\SeleniumTemp\";
                _failFolderPath = @"D:\Projects\Stageplan\stageplan\Stage-Plan.Selenium\Failures\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
                _tidyExePath = @"D:\Projects\Stageplan\stageplan\Stage-Plan.Selenium.PostTests\bin\Release\Stage-Plan.Selenium.PostTests.exe";
            }

            if (_isSetup)
            {
                if (Directory.Exists(TestImagesTempForTestRunPath))
                    Directory.Delete(TestImagesTempForTestRunPath, true);

                if (!Directory.Exists(TestImagesTempForTestRunPath))
                    Directory.CreateDirectory(TestImagesTempForTestRunPath);
            }

            _testImagesTempForComparison = TestImagesTempForTestRunPath + "comparison\\";

            if (Directory.Exists(_testImagesTempForComparison))
                Directory.Delete(_testImagesTempForComparison, true);

            if (!Directory.Exists(_testImagesTempForComparison))
                Directory.CreateDirectory(_testImagesTempForComparison);

            if (!Directory.Exists(_failFolderPath))
                Directory.CreateDirectory(_failFolderPath);

            _isSetup = false;
        }

        //[ClassCleanup()]
        //protected static void MyClassCleanupBase()
        //{
        //    QuitWebDriver();
        // }

        //private TestContext testContextInstance;
        //public TestContext TestContext
        //{
        //    get { return testContextInstance; }
        //    set { testContextInstance = value; }
        //}

        

        protected void SaveFailedImage(string originalLocation, string newLocation, string fileName)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            var previousMethodName = sf.GetMethod().Name;

            var newFolder = _failFolderPath + previousMethodName + "\\" + fileName;
            
            _imageFails.Add(originalLocation);
            _imageFails.Add(newLocation);
            _imageFails.Add(newFolder);

            _wasFail = true;
        }

        //protected void SaveFailedPrompt(string description)
        //{
        //    StackTrace st = new StackTrace();
        //    StackFrame sf = st.GetFrame(-1);
        //    MethodBase previousMethodName = sf.GetMethod();

        //    var newFolder = _failFolderPath + previousMethodName;
        //    if (!Directory.Exists(newFolder))
        //    {
        //        Directory.CreateDirectory(newFolder);
        //    }

        //    File.WriteAllText(newFolder, description);

        //    _wasFail = true;
        //}

        protected bool DoFilesMatch(string origFile, string newFile)
        {
            var result = Stage_Plan.ImageComparison.ImageComparer.CompareImages(origFile, newFile, .95, _testImagesTempForComparison, .95f);
            return result;
        }

        
        private string GetBytes(string file)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                var img = new Bitmap(file);
                ImageConverter converter = new ImageConverter();
                var bytes = (byte[])converter.ConvertTo(img, typeof(byte[]));
                return Convert.ToBase64String(sha1.ComputeHash(bytes));
            }
        }


        protected static void QuitWebDriver()
        {
            if (!_wasFail)
            {
                if (Directory.Exists(_failFolderPath))
                    Directory.Delete(_failFolderPath);
            }
            else
            {
                var failImageLocations = String.Empty;
                foreach (var item in _imageFails)
                {
                    failImageLocations += item + ";";
                }
                var process = new Process();
                process.StartInfo.FileName = _tidyExePath;
                process.StartInfo.Arguments = TestImagesTempForTestRunPath + ";" + _failFolderPath + ";" + failImageLocations;
                process.Start();
            }

            if (Directory.Exists(_testImagesTempForComparison))
                Directory.Delete(_testImagesTempForComparison, true);

            WebDriver.Quit();
        }
        
    }
}



//[TestMethod]
//[TestCategory("Selenium")]
//public void StagePlanTestIfDrumExists()
//{
//    // Test Case Steps
//    _webDriver.Navigate().GoToUrl("https://stage-plan.com/Stageplan/plan/test");

//    //WebDriverWait _wait = new WebDriverWait(_webDriver, new TimeSpan(0, 0, 25));

//    //_wait.Until(d => d.FindElement(By.Id("menu")));

//    System.Threading.Thread.Sleep(2000);

//    var accept = _webDriver.SwitchTo().Alert();
//    accept.Accept();

//    IWebElement menu = _webDriver.FindElement(By.Id("lmsResponsiveMenuLink"));

//    //IWebElement go = _webDriver.FindElement(By.Name("go"));

//    //search.SendKeys("james bond");

//    //go.Click();

//    Screenshot screenshot = _webDriver.GetScreenshot();
//    var origFile = _testImagesPersistent;
//    screenshot.SaveAsFile(origFile, OpenQA.Selenium.ScreenshotImageFormat.Png);

//    menu.Click();

//    // Save screenshot if required
//    screenshot = _webDriver.GetScreenshot();
//    var newFile = _testImagesForTestRun;
//    screenshot.SaveAsFile(newFile, OpenQA.Selenium.ScreenshotImageFormat.Png);

//    string hashOrig = GetBytes(origFile);
//    string hashNew = GetBytes(newFile);


//    //// Verfication
//    //IWebElement msWebsite =
//    //    _webDriver.FindElement(
//    //        By.XPath(

//    //            "//a[@href='http://en.wikipedia.org/wiki/James_Bond']"));

//    // IWebElement drum = _webDriver.FindElement(By.Id("https://stage-plan.com/Content/Stageplan/Images/Drums.png"));


//    Assert.IsTrue(hashOrig == hashNew, "Drums expected");

//    //Assert.IsNotNull(msWebsite, "Could not find wikipedia link for James Bond");
//}

