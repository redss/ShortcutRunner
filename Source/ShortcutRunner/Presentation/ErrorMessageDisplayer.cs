using System.Windows.Forms;
using ShortcutRunner.Properties;

namespace ShortcutRunner.Presentation
{
    public interface IErrorMessageDisplayer
    {
        void DisplayErrorMessage(string text);
    }

    public class ErrorMessageDisplayer : IErrorMessageDisplayer
    {
        public void DisplayErrorMessage(string text)
        {
            MessageBox.Show(text, Resources.ErrorMessageCaption);
        }
    }
}