using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace kursach2WinForms
{
    public partial class FormRecords : Form
    {
        private void ReadRecords()
        {
            using (StreamReader fstream = new StreamReader(FormGame.recPath))
            { 

                string textFromFile = fstream.ReadToEnd();

                fstream.Close();

                string[] words = textFromFile.Split(new char[] { ' ', '\n', '\0' });

                int length = int.Parse(words[0]), y = 50;

                Label label;

                if (length > 0)
                {
                    for (int i = 1; i < length * 2; i += 2)
                    {
                        label = new Label();
                        label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
                        label.AutoSize = true;
                        label.BackColor = System.Drawing.Color.Transparent;
                        label.Font = new System.Drawing.Font("Microsoft Uighur", 19.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        label.ForeColor = System.Drawing.Color.Orange;
                        label.Size = new System.Drawing.Size(0, 43);

                        label.Text = words[i] + " - " + words[i + 1];
                        label.Location = new System.Drawing.Point(69, y);
                        Controls.Add(label);
                        y += 50;
                    }
                }
                else
                {
                    label = new Label();
                    label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                    label.AutoSize = true;
                    label.BackColor = System.Drawing.Color.Transparent;
                    label.Font = new System.Drawing.Font("Microsoft Uighur", 19.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    label.ForeColor = System.Drawing.Color.Orange;
                    label.Size = new System.Drawing.Size(0, 43);
                    label.Text = "А рекордов нет(прям как в жизни)";
                    label.Location = new System.Drawing.Point(69, y);
                    Controls.Add(label);
                }
            }
        }
        public FormRecords()
        {
            InitializeComponent();
            ReadRecords();
        }

        private void label_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
