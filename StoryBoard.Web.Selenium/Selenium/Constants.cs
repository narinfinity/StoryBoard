using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Web.Selenium
{
    public static class Constants
    {
        //public static Random r = new Random(DateTime.Now.Millisecond);

        //Remote Web Driver's Configuration
        public const string DefaultHub = "http://hub.browserstack.com/wd/hub/";
        public const string Browser = "Firefox";
        public const string BrowserVersion = "23.0";
        public const string Os = "Windows";
        public const string OsVersion = "7";
        public const string BrowserstackDebug = "true";
        public const string BrowserstackUser = "narekmkrtchyan";
        public const string BrowserstackKey = "GQitx1tCssEUpZHKDvnQ";
        public const string Resolution = "1280x1024";
        public const string FullScreen = "true";

        public const string Build = "Development";
        public const string Project = "StoryBoard Default Capability";

        //TimeSpan seconds for driver's waiting untill finding the HTML element
        public static int SecondsDriverWait = 6;
        public const int SecondsFindElementWait = 12;

        //LoginPage Selector details
        public static string LoginPageNavigateUrl = "http://localhost:7919/";

        public static string UserName = "test@test.com";
        public static string Password = "Test123#";

        public static readonly By UserNameInputSelector = By.Name("Email");
        public static readonly By PasswordInputSelector = By.Name("Password");
        public static readonly By LoginInputSelector = By.CssSelector("#btn-login");
        public static readonly By WelcomeUserSelector = By.CssSelector("#logoutForm ul li a");
        //StoryPage selector details        
        public static readonly By IsAtStoryListPageSelector = By.CssSelector(".container .jumbotron h1");
        public static string StoryListPageHeaderText = "My Stories";
        public static readonly By AddEditStoryLinkSelector = By.CssSelector("#add-edit-story");
        public const string GetStoryCountScript = "return $('.k-listview-item').length";

        //AddEditStoryPage selector details
        public static readonly By IsAtAddEditStoryPageSelector = By.CssSelector(".container .jumbotron h1");
        public static string AddEditStoryPageHeaderText = "Add a story";

        public static readonly By StoryTitleInputSelector = By.CssSelector("#txt-story-title");
        public static string StoryTitleText = "Test title";
        public static readonly By StoryDescriptionInputSelector = By.CssSelector("#txt-story-description");
        public static string StoryDescriptionText = "Test description";
        public static readonly By StoryContentInputSelector = By.CssSelector("#txt-story-content");
        public static string StoryContentText = "Test content";
        public const string StoryGroupSelectScript = "var options = $('#myGroup-dropDown-list ul li.k-item'); $(options.get(0)).click();";
        public static readonly By StorySaveButtonSelector = By.CssSelector("#save-story");
        //Actions' selectors        
        public const string ActionsOwnerScript = "return $('div.sessionsStyle:eq(0)').find('div.nextclass div.milBlock div.divtitle:eq(0)').find('div.nextclass div.divactionClass:eq(0)').find('ul.actionClass li.sLi div.divgray span').text()";


    }
}
