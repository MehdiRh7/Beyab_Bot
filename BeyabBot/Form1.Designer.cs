namespace BeyabBot
{
    partial class Form1
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtBotToken = new System.Windows.Forms.TextBox();
            this.lblToken = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.colIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChatId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpManagement = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblUserCount = new System.Windows.Forms.Label();
            this.grpMessage = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblPhoto = new System.Windows.Forms.Label();
            this.txtPhotoPath = new System.Windows.Forms.TextBox();
            this.btnBrowsePhoto = new System.Windows.Forms.Button();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.grpManagement.SuspendLayout();
            this.grpMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 674);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(875, 26);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(45, 20);
            this.toolStripStatusLabelStatus.Text = "آفلاین";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(10, 13);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(105, 35);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "انجام";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // txtBotToken
            // 
            this.txtBotToken.Location = new System.Drawing.Point(121, 21);
            this.txtBotToken.Name = "txtBotToken";
            this.txtBotToken.Size = new System.Drawing.Size(696, 22);
            this.txtBotToken.TabIndex = 4;
            // 
            // lblToken
            // 
            this.lblToken.AutoSize = true;
            this.lblToken.Location = new System.Drawing.Point(823, 22);
            this.lblToken.Name = "lblToken";
            this.lblToken.Size = new System.Drawing.Size(38, 17);
            this.lblToken.TabIndex = 3;
            this.lblToken.Text = "توکن :";
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIndex,
            this.colChatId,
            this.colUsername,
            this.colUser2});
            this.dgvUsers.Location = new System.Drawing.Point(10, 68);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.Size = new System.Drawing.Size(848, 347);
            this.dgvUsers.TabIndex = 6;
            // 
            // colIndex
            // 
            this.colIndex.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colIndex.HeaderText = "#";
            this.colIndex.MinimumWidth = 6;
            this.colIndex.Name = "colIndex";
            this.colIndex.ReadOnly = true;
            // 
            // colChatId
            // 
            this.colChatId.HeaderText = "ChatID";
            this.colChatId.MinimumWidth = 6;
            this.colChatId.Name = "colChatId";
            this.colChatId.ReadOnly = true;
            this.colChatId.Width = 125;
            // 
            // colUsername
            // 
            this.colUsername.HeaderText = "Username";
            this.colUsername.MinimumWidth = 6;
            this.colUsername.Name = "colUsername";
            this.colUsername.ReadOnly = true;
            this.colUsername.Width = 125;
            // 
            // colUser2
            // 
            this.colUser2.HeaderText = "User2";
            this.colUser2.MinimumWidth = 6;
            this.colUser2.Name = "colUser2";
            this.colUser2.ReadOnly = true;
            this.colUser2.Width = 125;
            // 
            // grpManagement
            // 
            this.grpManagement.Controls.Add(this.txtBotToken);
            this.grpManagement.Controls.Add(this.lblToken);
            this.grpManagement.Controls.Add(this.btnApply);
            this.grpManagement.Location = new System.Drawing.Point(0, 7);
            this.grpManagement.Name = "grpManagement";
            this.grpManagement.Size = new System.Drawing.Size(875, 55);
            this.grpManagement.TabIndex = 7;
            this.grpManagement.TabStop = false;
            this.grpManagement.Text = "مدیریت";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 421);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblUserCount
            // 
            this.lblUserCount.AutoSize = true;
            this.lblUserCount.Location = new System.Drawing.Point(150, 424);
            this.lblUserCount.Name = "lblUserCount";
            this.lblUserCount.Size = new System.Drawing.Size(77, 17);
            this.lblUserCount.TabIndex = 9;
            this.lblUserCount.Text = "تعداد کاربران :";
            // 
            // grpMessage
            // 
            this.grpMessage.Controls.Add(this.btnSendMessage);
            this.grpMessage.Controls.Add(this.btnBrowsePhoto);
            this.grpMessage.Controls.Add(this.txtPhotoPath);
            this.grpMessage.Controls.Add(this.lblPhoto);
            this.grpMessage.Controls.Add(this.txtMessage);
            this.grpMessage.Location = new System.Drawing.Point(11, 450);
            this.grpMessage.Name = "grpMessage";
            this.grpMessage.Size = new System.Drawing.Size(852, 208);
            this.grpMessage.TabIndex = 10;
            this.grpMessage.TabStop = false;
            this.grpMessage.Text = "پیغام";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(482, 21);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(364, 178);
            this.txtMessage.TabIndex = 0;
            // 
            // lblPhoto
            // 
            this.lblPhoto.AutoSize = true;
            this.lblPhoto.Location = new System.Drawing.Point(429, 24);
            this.lblPhoto.Name = "lblPhoto";
            this.lblPhoto.Size = new System.Drawing.Size(42, 17);
            this.lblPhoto.TabIndex = 1;
            this.lblPhoto.Text = "عکس :";
            // 
            // txtPhotoPath
            // 
            this.txtPhotoPath.Location = new System.Drawing.Point(47, 21);
            this.txtPhotoPath.Name = "txtPhotoPath";
            this.txtPhotoPath.Size = new System.Drawing.Size(376, 22);
            this.txtPhotoPath.TabIndex = 2;
            // 
            // btnBrowsePhoto
            // 
            this.btnBrowsePhoto.Location = new System.Drawing.Point(13, 21);
            this.btnBrowsePhoto.Name = "btnBrowsePhoto";
            this.btnBrowsePhoto.Size = new System.Drawing.Size(28, 23);
            this.btnBrowsePhoto.TabIndex = 3;
            this.btnBrowsePhoto.Text = "...";
            this.btnBrowsePhoto.UseVisualStyleBackColor = true;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSendMessage.Location = new System.Drawing.Point(13, 175);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(455, 24);
            this.btnSendMessage.TabIndex = 4;
            this.btnSendMessage.Text = "ارسال";
            this.btnSendMessage.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 700);
            this.Controls.Add(this.grpMessage);
            this.Controls.Add(this.lblUserCount);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grpManagement);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.statusStrip);
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.grpManagement.ResumeLayout(false);
            this.grpManagement.PerformLayout();
            this.grpMessage.ResumeLayout(false);
            this.grpMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtBotToken;
        private System.Windows.Forms.Label lblToken;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChatId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsername;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser2;
        private System.Windows.Forms.GroupBox grpManagement;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblUserCount;
        private System.Windows.Forms.GroupBox grpMessage;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Button btnBrowsePhoto;
        private System.Windows.Forms.TextBox txtPhotoPath;
        private System.Windows.Forms.Label lblPhoto;
        private System.Windows.Forms.TextBox txtMessage;
    }
}

