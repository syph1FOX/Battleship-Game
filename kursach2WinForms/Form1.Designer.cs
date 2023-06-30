using System.Drawing;
using System.Windows.Forms;

namespace kursach2WinForms
{
    partial class FormMenu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.labelGameName = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonRecords = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.labelNick = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelGameName
            // 
            this.labelGameName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelGameName.AutoSize = true;
            this.labelGameName.BackColor = System.Drawing.Color.Transparent;
            this.labelGameName.Font = new System.Drawing.Font("Monotype Corsiva", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGameName.ForeColor = System.Drawing.Color.Red;
            this.labelGameName.Location = new System.Drawing.Point(217, 0);
            this.labelGameName.Name = "labelGameName";
            this.labelGameName.Size = new System.Drawing.Size(337, 72);
            this.labelGameName.TabIndex = 0;
            this.labelGameName.Text = "Морской Бой";
            this.labelGameName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonStart.BackColor = System.Drawing.Color.Transparent;
            this.buttonStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonStart.BackgroundImage")));
            this.buttonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStart.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.Red;
            this.buttonStart.Location = new System.Drawing.Point(262, 125);
            this.buttonStart.MaximumSize = new System.Drawing.Size(400, 100);
            this.buttonStart.MinimumSize = new System.Drawing.Size(150, 40);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(240, 40);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Начать игру";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonExit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonExit.BackgroundImage")));
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExit.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Red;
            this.buttonExit.Location = new System.Drawing.Point(272, 310);
            this.buttonExit.MaximumSize = new System.Drawing.Size(400, 100);
            this.buttonExit.MinimumSize = new System.Drawing.Size(150, 40);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(220, 40);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // buttonRecords
            // 
            this.buttonRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonRecords.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRecords.BackColor = System.Drawing.Color.Transparent;
            this.buttonRecords.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonRecords.BackgroundImage")));
            this.buttonRecords.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRecords.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRecords.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRecords.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRecords.ForeColor = System.Drawing.Color.Red;
            this.buttonRecords.Location = new System.Drawing.Point(272, 223);
            this.buttonRecords.MaximumSize = new System.Drawing.Size(400, 100);
            this.buttonRecords.MinimumSize = new System.Drawing.Size(150, 40);
            this.buttonRecords.Name = "buttonRecords";
            this.buttonRecords.Size = new System.Drawing.Size(220, 40);
            this.buttonRecords.TabIndex = 2;
            this.buttonRecords.Text = "Рекорды";
            this.buttonRecords.UseVisualStyleBackColor = true;
            this.buttonRecords.Click += new System.EventHandler(this.buttonRecords_Click);
            // 
            // label
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Cursor = System.Windows.Forms.Cursors.Default;
            this.label.Font = new System.Drawing.Font("MV Boli", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.Snow;
            this.label.Location = new System.Drawing.Point(224, 72);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(221, 26);
            this.label.TabIndex = 4;
            this.label.Text = "Добро пожаловать,";
            this.label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelNick
            // 
            this.labelNick.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelNick.AutoSize = true;
            this.labelNick.BackColor = System.Drawing.Color.Transparent;
            this.labelNick.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNick.Font = new System.Drawing.Font("MV Boli", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNick.ForeColor = System.Drawing.Color.Violet;
            this.labelNick.Location = new System.Drawing.Point(463, 72);
            this.labelNick.Name = "labelNick";
            this.labelNick.Size = new System.Drawing.Size(68, 26);
            this.labelNick.TabIndex = 5;
            this.labelNick.Text = "игрок";
            this.labelNick.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelNick.Click += new System.EventHandler(this.labelNick_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(791, 433);
            this.Controls.Add(this.labelNick);
            this.Controls.Add(this.label);
            this.Controls.Add(this.buttonRecords);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelGameName);
            this.MaximizeBox = false;
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Главное меню";
            this.KeyPreview = true;
            this.KeyDown += FormMenu_KeyDown;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FormMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        #endregion

        private System.Windows.Forms.Label labelGameName;
        private Button buttonStart;
        private Button buttonExit;
        private Button buttonRecords;
        private Label label;
        private Label labelNick;
    }
}

