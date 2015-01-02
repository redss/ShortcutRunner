using System.Windows.Forms;

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
            MessageBox.Show(text);
        }
    }
}