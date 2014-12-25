using System.Windows.Forms;
using ShortcutRunner.HotkeyRegistration;

namespace ShortcutRunner
{
    public partial class Form1 : Form
    {
        private readonly IKeyboardHook _keyboardHook;
        private readonly IShortcutDescriptionCreator _creator;

        public Form1(IKeyboardHook keyboardKeyboardHook, IShortcutDescriptionCreator creator)
        {
            _keyboardHook = keyboardKeyboardHook;
            _creator = creator;

            InitializeComponent();

            // register the event that is fired after the key press.
            _keyboardHook.KeyPressed += KeyboardHookKeyPressed;

            // register the control + alt + F12 combination as hot key.
            //var description = _creator.Parse("Ctrl + Alt + F12");

            var description = new ShortcutDescription(HotkeyRegistration.ModifierKeys.None, Keys.A);

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
