using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Web.Selenium
{
    public class LoginPage
    {
        public static void GoToLoginPage()
        {
            Driver.Instance.Url = Constants.LoginPageNavigateUrl;
        }

        public static void Login()
        {
            var userNameInput = Driver.Instance.FindElement(
                Constants.UserNameInputSelector,
                TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                );
            userNameInput.SendKeys(Constants.UserName);

            var passwordInput = Driver.Instance.FindElement(
                Constants.PasswordInputSelector,
                TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                );
            passwordInput.SendKeys(Constants.Password);

            var loginInput = Driver.Instance.FindElement(
                Constants.LoginInputSelector,
                TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                );
            loginInput.Click();
        }

        public static bool LoggedIn
        {
            get
            {
                var welcomeTopicHeader = Driver.Instance.FindElement(
                    Constants.WelcomeUserSelector,
                    TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                    );
                return welcomeTopicHeader.Text.Contains(Constants.UserName);
            }
        }
    }

}
