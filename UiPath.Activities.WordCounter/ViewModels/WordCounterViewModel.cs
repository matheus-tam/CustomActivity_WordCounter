using System.Activities.DesignViewModels;
using System.Diagnostics;

namespace UiPath.Activities.WordCounter.ViewModels
{
    public class WordCounterViewModel : DesignPropertiesViewModel
    {
        /*
         * Properties names must match the names and generic type arguments of the properties in the activity
         * Use DesignInArgument for properties that accept a variable
         */
        public DesignInArgument<int> TextToRead { get; set; }
        public DesignInArgument<int> WordFilter { get; set; }
        /*
         * The result property comes from the activity's base class
         */
        public DesignOutArgument<int> Result { get; set; }

        public WordCounterViewModel(IDesignServices services) : base(services)
        {
        }

        protected override void InitializeModel()
        {
            //Debugger.Break(); 
            /*
             * The base call will initialize the properties of the view model with the values from the xaml or with the default values from the activity
             */
            base.InitializeModel();

            PersistValuesChangedDuringInit(); // just for heads-up here; it's a mandatory call only when you change the values of properties during initialization

            var orderIndex = 0;

            TextToRead.DisplayName = Resources.WordCounter_TextToRead_DisplayName;
            TextToRead.Tooltip = Resources.WordCounter_TextToRead_Tooltip;
            /*
             * Required fields will automatically raise validation errors when empty.
             * Unless you do custom validation, required activity properties should be marked as such both in the view model and in the activity:
             *   -> in the view model use the IsRequired property
             *   -> in the activity use the [RequiredArgument] attribute.
             */
            TextToRead.IsRequired = true;

            TextToRead.IsPrincipal = true; // specifies if it belongs to the main category (which cannot be collapsed)
            TextToRead.OrderIndex = orderIndex++; // indicates the order in which the fields appear in the designer (i.e. the line number);

            WordFilter.DisplayName = Resources.WordCounter_WordFilter_DisplayName;
            WordFilter.Tooltip = Resources.WordCounter_WordFilter_Tooltip;
            WordFilter.IsPrincipal = true;
            WordFilter.OrderIndex = orderIndex++;


            /*
             * Output properties are never mandatory.
             * By convention, they are not principal and they are placed at the end of the activity.
             */
            Result.DisplayName = Resources.WordCounter_Result_DisplayName;
            Result.Tooltip = Resources.WordCounter_Result_Tooltip;
            Result.OrderIndex = orderIndex;
        }
    }
}
