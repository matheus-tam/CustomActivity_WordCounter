using System.Activities;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using UiPath.Activities.WordCounter.Helpers;

namespace UiPath.Activities.WordCounter
{
    public class WordCounter : CodeActivity<int> // This base class exposes an OutArgument named Result
    {
        [RequiredArgument]
        public InArgument<string> TextToRead { get; set; } //InArgument allows a varriable to be set from the workflow

        public InArgument<string> WordFilter { get; set; }


        /*
         * The returned value will be used to set the value of the Result argument
         */
        protected override int Execute(CodeActivityContext context)
        {
            // This is how you can log messages from your activity. logs are sent to the Robot which will forward them to Orchestrator
            context.GetExecutorRuntime().LogMessage(new Robot.Activities.Api.LogMessage()
            {
                EventType = TraceEventType.Information,
                Message = "Executing WordCounter activity"
            });

            var textToRead = TextToRead.Get(context); //get the value from the workflow context (remember, this can be a variable)
            var wordFilter = WordFilter.Get(context);

            if (String.IsNullOrEmpty(textToRead))
            {
                throw new NullReferenceException("String to read cannot be null");
            }

            return ExecuteInternal(textToRead, wordFilter);
        }

        public int ExecuteInternal(string textToRead, string wordFilter= "")
        {
            if (string.IsNullOrEmpty(wordFilter))
            {
                int count = CountWords(textToRead);
                return count;
                
            }
            else
            {
                int count = CountOccurrencesWithRegex(textToRead, wordFilter);
                return count;
            }
        }

        static int CountOccurrencesWithRegex(string text, string word)
        {
            // Create a regex pattern to match the word
            string pattern = $@"\b{Regex.Escape(word)}\b";

            // Create a Regex object with the pattern and ignore case option
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // Find all matches in the text
            MatchCollection matches = regex.Matches(text);

            // Return the number of matches found
            return matches.Count;
        }

        static int CountWords(string text)
        {
            // Split the text into words using space and punctuation as delimiters
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            int words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            return words;
        }
    }
}
