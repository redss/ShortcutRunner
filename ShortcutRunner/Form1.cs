using System.Windows.Forms;

namespace ShortcutRunner
{
    public partial class Form1 : Form
    {
        private readonly IKeyboardHook _keyboardHook;
        private readonly IShortcutDescriptionParser _parser;

        public Form1(IKeyboardHook keyboardKeyboardHook, IShortcutDescriptionParser parser)
        {
            _keyboardHook = keyboardKeyboardHook;
            _parser = parser;

            InitializeComponent();

            // register the event that is fired after the key press.
            _keyboardHook.KeyPressed += KeyboardHookKeyPressed;

            // register the control + alt + F12 combination as hot key.
            //var description = _parser.Parse("Ctrl + Alt + F12");

            var description = new ShortcutDescription
            {
                Key = Keys.A
            };

            _keyboardHook.RegisterHotKey(description);
        }

        void KeyboardHookKeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.

            MessageBox.Show(e.ShortcutDescription.Modifiers.ToString());

            //label1.Text = e.Modifiers.ToString() + " + " + e.Key.ToString();
        }
    }
}
