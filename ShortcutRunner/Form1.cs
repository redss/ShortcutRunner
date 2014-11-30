using System.Windows.Forms;

namespace ShortcutRunner
{
    public partial class Form1 : Form
    {
        private readonly IKeyboardHook _keyboardHook;

        public Form1(IKeyboardHook keyboardKeyboardHook)
        {
            _keyboardHook = keyboardKeyboardHook;

            InitializeComponent();

            // register the event that is fired after the key press.
            _keyboardHook.KeyPressed += KeyboardHookKeyPressed;

            // register the control + alt + F12 combination as hot key.
            _keyboardHook.RegisterHotKey(ShortcutRunner.ModifierKeys.Control | ShortcutRunner.ModifierKeys.Alt, Keys.F12);
        }

        void KeyboardHookKeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.

            MessageBox.Show(e.Modifier.ToString());

            //label1.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
        }
    }
}
