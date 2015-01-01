using System.Windows.Forms;
using ShortcutRunner.ShortcutManagement;

namespace ShortcutRunner
{
    public partial class Form1 : Form
    {
        public Form1(IShortcutManager shortcutManager)
        {
            shortcutManager.Initialize();
            InitializeComponent();
        }
    }
}
