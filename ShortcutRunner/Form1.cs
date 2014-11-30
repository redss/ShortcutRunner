using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutRunner
{
    public partial class Form1 : Form
    {
        private readonly KeyboardHook _hook = new KeyboardHook();

        public Form1()
        {
            InitializeComponent();

            // register the event that is fired after the key press.
            _hook.KeyPressed += hook_KeyPressed;

            // register the control + alt + F12 combination as hot key.
            _hook.RegisterHotKey(ShortcutRunner.ModifierKeys.Control | ShortcutRunner.ModifierKeys.Alt, Keys.F12);
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.

            MessageBox.Show(e.Modifier.ToString());

            //label1.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
        }
    }
}
