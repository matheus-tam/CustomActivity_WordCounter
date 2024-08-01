using Xunit;

namespace UiPath.Activities.WordCounter.Tests.Unit
{
    public class WordCounterUnitTests
    {
        [Theory]
        [InlineData("This is a test and it should display 1 as result","test")]
        [InlineData("We have 5 words here", "")]

        public void WordCounter_Test(string textToRead, string wordFilter)
        {
            var wordcounter = new WordCounter();
            var result = wordcounter.ExecuteInternal(textToRead, wordFilter);
            Assert.Equal(3, result);
        }
    }
}
