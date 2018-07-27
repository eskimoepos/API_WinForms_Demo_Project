using System;

namespace Win_Forms_Client
{
    partial class frmMain
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
            this.tvwActions = new System.Windows.Forms.TreeView();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pgeProperties = new System.Windows.Forms.TabPage();
            this.propGrid = new System.Windows.Forms.PropertyGrid();
            this.btnExecute = new System.Windows.Forms.Button();
            this.pgeResults = new System.Windows.Forms.TabPage();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.tvwResults = new System.Windows.Forms.TreeView();
            this.pgeRequest = new System.Windows.Forms.TabPage();
            this.txtRequestString = new System.Windows.Forms.TextBox();
            this.pgeResponse = new System.Windows.Forms.TabPage();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.lblBaseURL = new System.Windows.Forms.Label();
            this.lblBaseURLTitle = new System.Windows.Forms.Label();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.tabControl.SuspendLayout();
            this.pgeProperties.SuspendLayout();
            this.pgeResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.pgeRequest.SuspendLayout();
            this.pgeResponse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tvwActions
            // 
            this.tvwActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvwActions.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwActions.Location = new System.Drawing.Point(18, 18);
            this.tvwActions.Margin = new System.Windows.Forms.Padding(4);
            this.tvwActions.Name = "tvwActions";
            this.tvwActions.Size = new System.Drawing.Size(360, 578);
            this.tvwActions.TabIndex = 0;
            this.tvwActions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwActions_AfterSelect);
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(505, 49);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(537, 26);
            this.txtUserName.TabIndex = 2;
            this.txtUserName.Text = "APIDemoUser@EskimoEPOS.com";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(505, 84);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(537, 26);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "DemoUser123$";
            // 
            // lblUserName
            // 
            this.lblUserName.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(395, 54);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(126, 22);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "User Name";
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(395, 89);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(126, 22);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Text = "Password";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.pgeProperties);
            this.tabControl.Controls.Add(this.pgeResults);
            this.tabControl.Controls.Add(this.pgeRequest);
            this.tabControl.Controls.Add(this.pgeResponse);
            this.tabControl.Location = new System.Drawing.Point(387, 125);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(659, 471);
            this.tabControl.TabIndex = 8;
            // 
            // pgeProperties
            // 
            this.pgeProperties.Controls.Add(this.propGrid);
            this.pgeProperties.Controls.Add(this.btnExecute);
            this.pgeProperties.Location = new System.Drawing.Point(4, 28);
            this.pgeProperties.Margin = new System.Windows.Forms.Padding(4);
            this.pgeProperties.Name = "pgeProperties";
            this.pgeProperties.Padding = new System.Windows.Forms.Padding(4);
            this.pgeProperties.Size = new System.Drawing.Size(651, 439);
            this.pgeProperties.TabIndex = 0;
            this.pgeProperties.Text = "Properties";
            this.pgeProperties.UseVisualStyleBackColor = true;
            // 
            // propGrid
            // 
            this.propGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propGrid.Font = new System.Drawing.Font("Cambria", 12F);
            this.propGrid.Location = new System.Drawing.Point(8, 8);
            this.propGrid.Margin = new System.Windows.Forms.Padding(4);
            this.propGrid.Name = "propGrid";
            this.propGrid.Size = new System.Drawing.Size(634, 365);
            this.propGrid.TabIndex = 8;
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecute.Enabled = false;
            this.btnExecute.Location = new System.Drawing.Point(529, 380);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(112, 42);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // pgeResults
            // 
            this.pgeResults.Controls.Add(this.PictureBox);
            this.pgeResults.Controls.Add(this.lblRowCount);
            this.pgeResults.Controls.Add(this.grdData);
            this.pgeResults.Controls.Add(this.tvwResults);
            this.pgeResults.Location = new System.Drawing.Point(4, 28);
            this.pgeResults.Margin = new System.Windows.Forms.Padding(4);
            this.pgeResults.Name = "pgeResults";
            this.pgeResults.Padding = new System.Windows.Forms.Padding(4);
            this.pgeResults.Size = new System.Drawing.Size(651, 439);
            this.pgeResults.TabIndex = 1;
            this.pgeResults.Text = "Results";
            this.pgeResults.UseVisualStyleBackColor = true;
            // 
            // grdData
            // 
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.Location = new System.Drawing.Point(214, 123);
            this.grdData.Margin = new System.Windows.Forms.Padding(4);
            this.grdData.Name = "grdData";
            this.grdData.Size = new System.Drawing.Size(336, 261);
            this.grdData.TabIndex = 11;
            // 
            // tvwResults
            // 
            this.tvwResults.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwResults.Location = new System.Drawing.Point(56, 63);
            this.tvwResults.Margin = new System.Windows.Forms.Padding(4);
            this.tvwResults.Name = "tvwResults";
            this.tvwResults.Size = new System.Drawing.Size(533, 338);
            this.tvwResults.TabIndex = 0;
            // 
            // pgeRequest
            // 
            this.pgeRequest.Controls.Add(this.txtRequestString);
            this.pgeRequest.Location = new System.Drawing.Point(4, 28);
            this.pgeRequest.Name = "pgeRequest";
            this.pgeRequest.Size = new System.Drawing.Size(651, 439);
            this.pgeRequest.TabIndex = 2;
            this.pgeRequest.Text = "Request";
            this.pgeRequest.UseVisualStyleBackColor = true;
            // 
            // txtRequestString
            // 
            this.txtRequestString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRequestString.Location = new System.Drawing.Point(0, 0);
            this.txtRequestString.Multiline = true;
            this.txtRequestString.Name = "txtRequestString";
            this.txtRequestString.ReadOnly = true;
            this.txtRequestString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRequestString.Size = new System.Drawing.Size(651, 439);
            this.txtRequestString.TabIndex = 0;
            // 
            // pgeResponse
            // 
            this.pgeResponse.Controls.Add(this.txtResponse);
            this.pgeResponse.Location = new System.Drawing.Point(4, 28);
            this.pgeResponse.Name = "pgeResponse";
            this.pgeResponse.Size = new System.Drawing.Size(651, 439);
            this.pgeResponse.TabIndex = 3;
            this.pgeResponse.Text = "Response";
            this.pgeResponse.UseVisualStyleBackColor = true;
            // 
            // txtResponse
            // 
            this.txtResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponse.Location = new System.Drawing.Point(0, 0);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ReadOnly = true;
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(651, 439);
            this.txtResponse.TabIndex = 1;
            // 
            // lblBaseURL
            // 
            this.lblBaseURL.AutoSize = true;
            this.lblBaseURL.Location = new System.Drawing.Point(501, 18);
            this.lblBaseURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaseURL.Name = "lblBaseURL";
            this.lblBaseURL.Size = new System.Drawing.Size(44, 19);
            this.lblBaseURL.TabIndex = 9;
            this.lblBaseURL.Text = "https";
            // 
            // lblBaseURLTitle
            // 
            this.lblBaseURLTitle.AutoSize = true;
            this.lblBaseURLTitle.Location = new System.Drawing.Point(395, 18);
            this.lblBaseURLTitle.Name = "lblBaseURLTitle";
            this.lblBaseURLTitle.Size = new System.Drawing.Size(75, 19);
            this.lblBaseURLTitle.TabIndex = 10;
            this.lblBaseURLTitle.Text = "Base URL";
            // 
            // lblRowCount
            // 
            this.lblRowCount.AutoSize = true;
            this.lblRowCount.Location = new System.Drawing.Point(7, 9);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(97, 19);
            this.lblRowCount.TabIndex = 12;
            this.lblRowCount.Text = "{Row Count}";
            // 
            // PictureBox
            // 
            this.PictureBox.BackColor = System.Drawing.Color.Maroon;
            this.PictureBox.Location = new System.Drawing.Point(11, 123);
            this.PictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(119, 212);
            this.PictureBox.TabIndex = 14;
            this.PictureBox.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 614);
            this.Controls.Add(this.lblBaseURLTitle);
            this.Controls.Add(this.lblBaseURL);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.tvwActions);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUserName);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eskimo API Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.ResizeForm_Resize);
            this.tabControl.ResumeLayout(false);
            this.pgeProperties.ResumeLayout(false);
            this.pgeResults.ResumeLayout(false);
            this.pgeResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.pgeRequest.ResumeLayout(false);
            this.pgeRequest.PerformLayout();
            this.pgeResponse.ResumeLayout(false);
            this.pgeResponse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.TreeView tvwActions;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TabControl tabControl;
        internal System.Windows.Forms.TabPage pgeProperties;
        private System.Windows.Forms.PropertyGrid propGrid;
        private System.Windows.Forms.Button btnExecute;
        internal System.Windows.Forms.TabPage pgeResults;
        private System.Windows.Forms.TreeView tvwResults;

        private System.Windows.Forms.DataGridView grdData;

        #endregion

        private System.Windows.Forms.Label lblBaseURL;
        private System.Windows.Forms.Label lblBaseURLTitle;
        private System.Windows.Forms.TabPage pgeRequest;
        private System.Windows.Forms.TextBox txtRequestString;
        private System.Windows.Forms.TabPage pgeResponse;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.PictureBox PictureBox;
    }
}