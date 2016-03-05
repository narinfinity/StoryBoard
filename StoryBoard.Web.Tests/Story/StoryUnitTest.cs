using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryBoard.Web.Selenium;

namespace StoryBoard.Web.Tests
{
    [TestClass]
    public class StoryUnitTest //: TestBase
    {
        [TestMethod]
        public void User_Can_Login()
        {
            //Driver Initialization
            //Uncoment in order to run test cases on Browserstack.com (not for localhost)
            //Driver.InitializeAsRemoteWebDriver(); 
            Driver.InitializeAsLocalWebDrtiver();

            if (!(Driver.ForSafari || Driver.ForiPad))
            {
                Driver.SetPageLoadTimeout(12);
            }

            //Login Page's test step
            LoginPage.GoToLoginPage();
            LoginPage.Login();
            Driver.SetPageLoadTimeout(5);
            Assert.IsTrue(LoginPage.LoggedIn, "Failed to login.");

            Assert.IsTrue(StoriesPage.IsAt, "Could not load stories' page.");
        }

        
        [TestMethod]
        public void User_Can_Add_Story()
        {
            //Uncomment in order to debug this test case
            User_Can_Login();
            AddEditStoryPage.SelectStoryGroupAtIndex(0);
            var storyCount = StoriesPage.StoryCount;

            StoriesPage.GoToAddEditStoryPage();
            Driver.SetPageLoadTimeout(5);
            Assert.IsTrue(AddEditStoryPage.IsAt, "Could not load add/edit story page.");

            AddEditStoryPage.FillOutStoryData();
            AddEditStoryPage.SelectStoryGroupAtIndex(0);
            AddEditStoryPage.SaveStory();
            Driver.SetPageLoadTimeout(15);

            AddEditStoryPage.SelectStoryGroupAtIndex(0);
            Assert.IsTrue((storyCount + 1) == StoriesPage.StoryCount, "Could not load stories' page.");

        }
        


    }
}
