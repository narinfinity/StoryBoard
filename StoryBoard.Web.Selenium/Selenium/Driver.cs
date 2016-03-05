using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Web.Selenium
{
    public class Driver
    {
        public static IWebDriver Instance { get; private set; }

        //Initialize as RemoteWebDriver
        public static void InitializeAsRemoteWebDriver()
        {
            Capability.ReadRemoteDriverCapability();            
            Instance = new RemoteWebDriver(new Uri(Constants.DefaultHub), Capability.Instance);            
        }

        //Init as local WebDriver
        public static void InitializeAsLocalWebDrtiver()
        {           
            var browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
            if (browserKeys == null)
                browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
            string[] browserNames = browserKeys.GetSubKeyNames();
            if (browserNames.Contains("FIREFOX.EXE"))
            {
                Instance = new OpenQA.Selenium.Firefox.FirefoxDriver();
            }
            else if (browserNames.Contains("Google Chrome"))
            {
                Instance = new OpenQA.Selenium.Chrome.ChromeDriver();
            }            
            else if(browserNames.Contains("IEXPLORE.EXE"))
            {
                Instance = new OpenQA.Selenium.IE.InternetExplorerDriver();
            }            
        }

        public static object Execute(string somescript)
        {
            return ((IJavaScriptExecutor)Instance).ExecuteScript(somescript);
        }

        public static void SetScriptTimeout(int seconds)
        {
            Instance.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(seconds));
        }

        public static void SetPageLoadTimeout(int seconds)
        {
            Instance.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(seconds));
        }

        public static void Wait(int seconds)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        public static void SwitchToFrame(int id)
        {
            Instance.SwitchTo().Frame(id);
        }

        public static void Back()
        {
            Instance.Navigate().Back();
        }

        public static void Forward()
        {
            Instance.Navigate().Forward();
        }

        public static void Refresh()
        {
            Instance.Navigate().Refresh();
        }

        public static IAlert Alert
        {
            get
            {
                return Instance.SwitchTo().Alert();
            }
        }

        public static string AlertText
        {
            get { return Alert.Text; }
            set { Alert.SendKeys(value); }
        }

        public static void AcceptAlert()
        {
            Alert.Accept();
        }

        public static void Close()
        {
            Instance.Close();
        }

        public static void Quit()
        {
            Instance.Quit();
        }

        public static string Browser
        {
            get
            {
                object browser;
                try
                {
                    browser = Capability.Instance.GetCapability("browser");
                }
                catch (Exception ex)
                { return "Firefox"; }
                return (browser != null ? browser.ToString() : Capability.Instance.GetCapability("browserName").ToString());
            }
        }

        public static string BrowserVersion
        {
            get
            {
                return Capability.Instance.GetCapability("browser_version").ToString();
            }
        }


        public static bool ForSafari
        {
            get
            {
                return Browser.ToLower().Contains("safari");
            }
        }

        public static bool ForiPad
        {
            get
            {
                return Browser.ToLower().Contains("ipad");
            }
        }

        public static bool ForIe
        {
            get
            {
                return Browser.ToLower().Contains("ie");
            }
        }

        public static bool ForIe9
        {
            get
            {
                return
                    Browser.ToLower().Contains("ie")
                    &&
                    BrowserVersion == "9.0";
            }
        }


    }
}
