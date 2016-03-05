using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Web.Selenium
{
    public class StoriesPage
    {
        public static bool IsAt
        {
            get
            {
                var welcomeTopicHeader = Driver.Instance.FindElement(
                    Constants.IsAtStoryListPageSelector,
                    TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                    );
                return welcomeTopicHeader.Text.Contains(Constants.StoryListPageHeaderText);
            }
        }

        public static int StoryCount
        {
            get
            {
                var count = 0;
                return int.TryParse(Driver.Execute(Constants.GetStoryCountScript).ToString(), out count) ? count : 0;
            }
        }

        public static void GoToAddEditStoryPage()
        {
            var addEditLink = Driver.Instance.FindElement(
                Constants.AddEditStoryLinkSelector,
                TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                );
            addEditLink.Click();
        }










    }
}
