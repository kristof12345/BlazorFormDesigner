using System;
namespace BlazorFormDesigner.BusinessLogic.Exceptions
{
    public class FormException : Exception
    {
        public string Text { get; }

        public FormException(string text)
        {
            Text = text;
        }
    }
}
