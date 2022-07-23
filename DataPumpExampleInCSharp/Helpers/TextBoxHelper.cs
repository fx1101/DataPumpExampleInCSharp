using System.Windows.Forms;
using System.Threading;

namespace DataPumpExampleInCSharp.Helpers
{
    public static class TextBoxHelper
    {
        private delegate void SetTextBoxTextCallback(TextBox textBox, string text);
        private delegate string GetTextBoxTextCallback(TextBox textBox);
        private static int tries = 0;

        // Set the text in a TextBox on another thread
        public static void SetTextBoxText(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                if (tries++ >= 3)
                    Thread.Sleep(300);
                SetTextBoxTextCallback d = SetTextBoxText;
                textBox.Invoke(d, new object[] { textBox, text });
            }
            else
            {
                tries = 0;
                textBox.Text = text;
            }
        }

        // Get the text in a TextBox on another thread
        public static string GetTextBoxText(TextBox textBox)
        {
            if (textBox.InvokeRequired)
            {
                if (tries++ >= 3)
                    Thread.Sleep(300);
                GetTextBoxTextCallback d = GetTextBoxText;
                return (string)textBox.Invoke(d, new object[] { textBox });
            }
            else
            {
                tries = 0;
                return textBox.Text;
            }
        }
    }
}
