using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryBoard.Web.Selenium
{

    public class AddEditStoryPage
    {
        public static bool IsAt
        {
            get
            {
                var welcomeTopicHeader = Driver.Instance.FindElement(
                    Constants.IsAtAddEditStoryPageSelector,
                    TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                    );
                return welcomeTopicHeader.Text.Contains(Constants.AddEditStoryPageHeaderText);
            }
        }

        public static void FillOutStoryData()
        {
            var storyTitle = Driver.Instance.FindElement(
                Constants.StoryTitleInputSelector,
                TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                );
            storyTitle.SendKeys(Constants.StoryTitleText);

            var storyDescription = Driver.Instance.FindElement(
                Constants.StoryDescriptionInputSelector,
                TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                );
            storyDescription.SendKeys(Constants.StoryDescriptionText);

            var storyContent = Driver.Instance.FindElement(
                Constants.StoryContentInputSelector,
                TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                );
            storyContent.SendKeys(Constants.StoryContentText);
        }

        public static void SelectStoryGroupAtIndex(int index)
        {
            Driver.Execute(Constants.StoryGroupSelectScript.Replace(".get(0)", ".get(" + index + ")"));
        }

        public static void SaveStory()
        {
            var saveButton = Driver.Instance.FindElement(
                            Constants.StorySaveButtonSelector,
                            TimeSpan.FromSeconds(Constants.SecondsFindElementWait)
                            );
            saveButton.Click();
        }
    }
}
