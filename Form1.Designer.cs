namespace YoutubeForever
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txt_log = new System.Windows.Forms.TextBox();
            this.tmr_ctrlc = new System.Windows.Forms.Timer(this.components);
            this.rdb_mp4 = new System.Windows.Forms.RadioButton();
            this.rdb_mp4Best = new System.Windows.Forms.RadioButton();
            this.lbl_format = new System.Windows.Forms.Label();
            this.rdb_mp3 = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.rdb_1080 = new System.Windows.Forms.RadioButton();
            this.rdb_2k = new System.Windows.Forms.RadioButton();
            this.rdb_4k = new System.Windows.Forms.RadioButton();
            this.lbl_tortuga = new System.Windows.Forms.LinkLabel();
            this.pcb_thumb = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_thumb)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(339, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Baixar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(75, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Video Link";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 23);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txt_log
            // 
            this.txt_log.Location = new System.Drawing.Point(12, 113);
            this.txt_log.Multiline = true;
            this.txt_log.Name = "txt_log";
            this.txt_log.ReadOnly = true;
            this.txt_log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_log.Size = new System.Drawing.Size(420, 231);
            this.txt_log.TabIndex = 3;
            // 
            // tmr_ctrlc
            // 
            this.tmr_ctrlc.Enabled = true;
            this.tmr_ctrlc.Tick += new System.EventHandler(this.tmr_ctrlc_Tick);
            // 
            // rdb_mp4
            // 
            this.rdb_mp4.AutoSize = true;
            this.rdb_mp4.Checked = true;
            this.rdb_mp4.Location = new System.Drawing.Point(228, 55);
            this.rdb_mp4.Name = "rdb_mp4";
            this.rdb_mp4.Size = new System.Drawing.Size(85, 19);
            this.rdb_mp4.TabIndex = 4;
            this.rdb_mp4.TabStop = true;
            this.rdb_mp4.Text = "MP4 (720p)";
            this.rdb_mp4.UseVisualStyleBackColor = true;
            // 
            // rdb_mp4Best
            // 
            this.rdb_mp4Best.AutoSize = true;
            this.rdb_mp4Best.Location = new System.Drawing.Point(325, 75);
            this.rdb_mp4Best.Name = "rdb_mp4Best";
            this.rdb_mp4Best.Size = new System.Drawing.Size(115, 19);
            this.rdb_mp4Best.TabIndex = 5;
            this.rdb_mp4Best.Text = "MP4 Best Quality";
            this.rdb_mp4Best.UseVisualStyleBackColor = true;
            // 
            // lbl_format
            // 
            this.lbl_format.AutoSize = true;
            this.lbl_format.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_format.Location = new System.Drawing.Point(276, 10);
            this.lbl_format.Name = "lbl_format";
            this.lbl_format.Size = new System.Drawing.Size(78, 17);
            this.lbl_format.TabIndex = 6;
            this.lbl_format.Text = "File Format";
            // 
            // rdb_mp3
            // 
            this.rdb_mp3.AutoSize = true;
            this.rdb_mp3.Location = new System.Drawing.Point(228, 35);
            this.rdb_mp3.Name = "rdb_mp3";
            this.rdb_mp3.Size = new System.Drawing.Size(49, 19);
            this.rdb_mp3.TabIndex = 8;
            this.rdb_mp3.Text = "MP3";
            this.rdb_mp3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 9;
            this.button2.Text = "Limpar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btn_Clean_Click);
            // 
            // rdb_1080
            // 
            this.rdb_1080.AutoSize = true;
            this.rdb_1080.Location = new System.Drawing.Point(228, 75);
            this.rdb_1080.Name = "rdb_1080";
            this.rdb_1080.Size = new System.Drawing.Size(91, 19);
            this.rdb_1080.TabIndex = 10;
            this.rdb_1080.Text = "MP4 (1080p)";
            this.rdb_1080.UseVisualStyleBackColor = true;
            // 
            // rdb_2k
            // 
            this.rdb_2k.Location = new System.Drawing.Point(325, 35);
            this.rdb_2k.Name = "rdb_2k";
            this.rdb_2k.Size = new System.Drawing.Size(91, 19);
            this.rdb_2k.TabIndex = 11;
            this.rdb_2k.Text = "MP4 (2K)";
            this.rdb_2k.UseVisualStyleBackColor = true;
            // 
            // rdb_4k
            // 
            this.rdb_4k.Location = new System.Drawing.Point(325, 55);
            this.rdb_4k.Name = "rdb_4k";
            this.rdb_4k.Size = new System.Drawing.Size(91, 19);
            this.rdb_4k.TabIndex = 12;
            this.rdb_4k.Text = "MP4 (4K)";
            this.rdb_4k.UseVisualStyleBackColor = true;
            // 
            // lbl_tortuga
            // 
            this.lbl_tortuga.Location = new System.Drawing.Point(3, 61);
            this.lbl_tortuga.Name = "lbl_tortuga";
            this.lbl_tortuga.Size = new System.Drawing.Size(215, 49);
            this.lbl_tortuga.TabIndex = 13;
            this.lbl_tortuga.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_tortuga.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pcb_thumb
            // 
            this.pcb_thumb.Location = new System.Drawing.Point(201, 220);
            this.pcb_thumb.Name = "pcb_thumb";
            this.pcb_thumb.Size = new System.Drawing.Size(213, 120);
            this.pcb_thumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcb_thumb.TabIndex = 14;
            this.pcb_thumb.TabStop = false;
            this.pcb_thumb.Click += new System.EventHandler(this.pcb_thumb_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(444, 392);
            this.Controls.Add(this.pcb_thumb);
            this.Controls.Add(this.lbl_tortuga);
            this.Controls.Add(this.rdb_4k);
            this.Controls.Add(this.rdb_2k);
            this.Controls.Add(this.rdb_1080);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rdb_mp3);
            this.Controls.Add(this.lbl_format);
            this.Controls.Add(this.rdb_mp4Best);
            this.Controls.Add(this.rdb_mp4);
            this.Controls.Add(this.txt_log);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube Forever";
            ((System.ComponentModel.ISupportInitialize)(this.pcb_thumb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox textBox1;
        private TextBox txt_log;
        private System.Windows.Forms.Timer tmr_ctrlc;
        private RadioButton rdb_mp4;
        private RadioButton rdb_mp4Best;
        private Label lbl_format;
        private RadioButton rdb_mp3;
        private Button button2;
        private RadioButton rdb_1080;
        private RadioButton rdb_2k;
        private RadioButton rdb_4k;
        private LinkLabel lbl_tortuga;
        private PictureBox pcb_thumb;
    }
}