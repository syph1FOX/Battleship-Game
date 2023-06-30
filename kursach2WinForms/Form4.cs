using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach2WinForms
{
    public partial class FormEnterNickname : Form
    {
        public static string nick = "";
        public FormEnterNickname()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            nick =  textBoxNick.Text;
            Close();
        }
    }
}
