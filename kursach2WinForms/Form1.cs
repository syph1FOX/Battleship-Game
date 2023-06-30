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
    public partial class FormMenu : Form
    {
        internal static string nickname = "игрок";
        public FormMenu()
        {
            InitializeComponent();
        }
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            FormGame form = new FormGame();
            form.ShowDialog();
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonRecords_Click(object sender, EventArgs e)
        {
            FormRecords form = new FormRecords();

            form.ShowDialog();

        }

        private void labelNick_Click(object sender, EventArgs e)
        {
            FormEnterNickname form = new FormEnterNickname();

            form.ShowDialog();

            if (FormEnterNickname.nick.Length < 3 || FormEnterNickname.nick.Length > 12)
                return;

            nickname = FormEnterNickname.nick;

            labelNick.Text = nickname;
        }
    }
}
