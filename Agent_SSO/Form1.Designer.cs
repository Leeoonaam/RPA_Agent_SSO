namespace Agent_SSO
{
    partial class frmIndex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndex));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVersao = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OptDeslogado = new System.Windows.Forms.RadioButton();
            this.OptLogado = new System.Windows.Forms.RadioButton();
            this.btnVerifica_sites = new System.Windows.Forms.Button();
            this.btnOutros_Sites = new System.Windows.Forms.Button();
            this.btnPortal_Simples = new System.Windows.Forms.Button();
            this.btnPortalSimples = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblstatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(66)))), ((int)(((byte)(90)))));
            this.panel1.Controls.Add(this.lblVersao);
            this.panel1.Controls.Add(this.lblLogin);
            this.panel1.Controls.Add(this.pictureBox2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lblVersao
            // 
            resources.ApplyResources(this.lblVersao, "lblVersao");
            this.lblVersao.ForeColor = System.Drawing.Color.White;
            this.lblVersao.Name = "lblVersao";
            // 
            // lblLogin
            // 
            resources.ApplyResources(this.lblLogin, "lblLogin");
            this.lblLogin.ForeColor = System.Drawing.Color.White;
            this.lblLogin.Name = "lblLogin";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.OptDeslogado);
            this.panel2.Controls.Add(this.OptLogado);
            this.panel2.Controls.Add(this.btnVerifica_sites);
            this.panel2.Controls.Add(this.btnOutros_Sites);
            this.panel2.Controls.Add(this.btnPortal_Simples);
            this.panel2.Controls.Add(this.btnPortalSimples);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // OptDeslogado
            // 
            resources.ApplyResources(this.OptDeslogado, "OptDeslogado");
            this.OptDeslogado.Name = "OptDeslogado";
            this.OptDeslogado.UseVisualStyleBackColor = true;
            // 
            // OptLogado
            // 
            resources.ApplyResources(this.OptLogado, "OptLogado");
            this.OptLogado.Checked = true;
            this.OptLogado.Name = "OptLogado";
            this.OptLogado.TabStop = true;
            this.OptLogado.UseVisualStyleBackColor = true;
            // 
            // btnVerifica_sites
            // 
            this.btnVerifica_sites.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnVerifica_sites, "btnVerifica_sites");
            this.btnVerifica_sites.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnVerifica_sites.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnVerifica_sites.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnVerifica_sites.ForeColor = System.Drawing.Color.Gray;
            this.btnVerifica_sites.Name = "btnVerifica_sites";
            this.btnVerifica_sites.UseVisualStyleBackColor = false;
            this.btnVerifica_sites.Click += new System.EventHandler(this.btnVerifica_sites_Click);
            // 
            // btnOutros_Sites
            // 
            this.btnOutros_Sites.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnOutros_Sites, "btnOutros_Sites");
            this.btnOutros_Sites.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnOutros_Sites.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnOutros_Sites.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOutros_Sites.ForeColor = System.Drawing.Color.Gray;
            this.btnOutros_Sites.Name = "btnOutros_Sites";
            this.btnOutros_Sites.UseVisualStyleBackColor = false;
            this.btnOutros_Sites.Click += new System.EventHandler(this.btnOutros_Sites_Click);
            // 
            // btnPortal_Simples
            // 
            this.btnPortal_Simples.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPortal_Simples, "btnPortal_Simples");
            this.btnPortal_Simples.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnPortal_Simples.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnPortal_Simples.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPortal_Simples.ForeColor = System.Drawing.Color.Gray;
            this.btnPortal_Simples.Name = "btnPortal_Simples";
            this.btnPortal_Simples.UseVisualStyleBackColor = false;
            this.btnPortal_Simples.Click += new System.EventHandler(this.btnPortal_Simples_Click);
            // 
            // btnPortalSimples
            // 
            this.btnPortalSimples.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPortalSimples, "btnPortalSimples");
            this.btnPortalSimples.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnPortalSimples.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnPortalSimples.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPortalSimples.ForeColor = System.Drawing.Color.Gray;
            this.btnPortalSimples.Name = "btnPortalSimples";
            this.btnPortalSimples.UseVisualStyleBackColor = false;
            this.btnPortalSimples.Click += new System.EventHandler(this.btnPortalSimples_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(28)))), ((int)(((byte)(60)))));
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblstatus);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Name = "label1";
            // 
            // lblstatus
            // 
            resources.ApplyResources(this.lblstatus, "lblstatus");
            this.lblstatus.ForeColor = System.Drawing.Color.White;
            this.lblstatus.Name = "lblstatus";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.button1, "button1");
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.ForeColor = System.Drawing.Color.Gray;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmIndex
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmIndex";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIndex_FormClosing);
            this.Load += new System.EventHandler(this.frmIndex_Load);
            this.Resize += new System.EventHandler(this.frmIndex_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblstatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnPortalSimples;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton OptDeslogado;
        private System.Windows.Forms.RadioButton OptLogado;
        private System.Windows.Forms.Button btnVerifica_sites;
        private System.Windows.Forms.Button btnOutros_Sites;
        private System.Windows.Forms.Button btnPortal_Simples;
        private System.Windows.Forms.Timer timer2;
    }
}

