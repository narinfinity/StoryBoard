using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StoryBoard.Web.Selenium
{
    public class Capability
    {
        public static DesiredCapabilities Instance { get; private set; }

        private static void InitializeAsDefaultCapability()
        {
            Instance = DesiredCapabilities.HtmlUnitWithJavaScript();

            Instance.SetCapability("browser", Constants.Browser);
            Instance.SetCapability("browser_version", Constants.BrowserVersion);
            Instance.SetCapability("os", Constants.Os);
            Instance.SetCapability("os_version", Constants.OsVersion);
            Instance.SetCapability("browserstack.debug", Constants.BrowserstackDebug);
            Instance.SetCapability("browserstack.user", Constants.BrowserstackUser);
            Instance.SetCapability("browserstack.key", Constants.BrowserstackKey);
            Instance.SetCapability("full_screen", Constants.FullScreen);
            Instance.SetCapability("build", Constants.Build);
            Instance.SetCapability("project", Constants.Project);
            Instance.SetCapability("resolution", Constants.Resolution);
        }

        public static bool ReadRemoteDriverCapability()
        {
            string configFileUrl = Path.Combine(Environment.CurrentDirectory, "BrowserStackConfig.xml");
            try
            {
                if (!File.Exists(configFileUrl))
                {
                    Capability.InitializeAsDefaultCapability();
                    return false;
                }


                XElement xElement = XElement.Load(configFileUrl);

                var activeConfig = xElement.Elements().FirstOrDefault(el => el.Attributes()
                            .FirstOrDefault(atr => (atr.Name == "active") && (atr.Value == "on")) != null);

                if (activeConfig == null)
                {
                    Capability.InitializeAsDefaultCapability();
                }
                else
                {
                    var signupUrl = xElement.Elements().FirstOrDefault(el => el.Name == "SignUpUrl");
                    if (null != signupUrl)
                    {
                        Constants.LoginPageNavigateUrl = signupUrl.Value;
                    }

                    Instance = new DesiredCapabilities { IsJavaScriptEnabled = true };

                    Instance.SetCapability("browserstack.user", Constants.BrowserstackUser);
                    Instance.SetCapability("browserstack.key", Constants.BrowserstackKey);

                    foreach (var element in activeConfig.Elements())
                    {
                        Instance.SetCapability(element.Name.LocalName, element.Value);
                    }

                    Instance.SetCapability("browserstack.debug", Constants.BrowserstackDebug);
                    Instance.SetCapability("full_screen", Constants.FullScreen);

                    if (!Instance.BrowserName.ToLower().Contains("ipad"))
                    {
                        Instance.SetCapability("resolution", Constants.Resolution);

                    }

                    // var browser = Instance.GetCapability("browser");
                    // if (browser != null && browser.ToString().ToLower().Contains("safari"))
                    //Instance.SetCapability("acceptSslCert", true);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}
