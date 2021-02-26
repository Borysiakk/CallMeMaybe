using System.ComponentModel;

namespace CallMeMaybe.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCall = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonCallDeclined = new System.Windows.Forms.Button();
            this.buttonCallAccepted = new System.Windows.Forms.Button();
            this.listViewFriends = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.sendMessageButton = new System.Windows.Forms.Button();
            this.messageText = new System.Windows.Forms.TextBox();
            this.TabControlChat = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonCall);
            this.panel1.Controls.Add(this.buttonCancel);
            this.panel1.Controls.Add(this.buttonCallDeclined);
            this.panel1.Controls.Add(this.buttonCallAccepted);
            this.panel1.Controls.Add(this.listViewFriends);
            this.panel1.Controls.Add(this.sendMessageButton);
            this.panel1.Controls.Add(this.messageText);
            this.panel1.Controls.Add(this.TabControlChat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 450);
            this.panel1.TabIndex = 0;
            // 
            // buttonCall
            // 
            this.buttonCall.Image = ((System.Drawing.Image)(resources.GetObject("buttonCall.Image")));
            this.buttonCall.Location = new System.Drawing.Point(660, 14);
            this.buttonCall.Name = "buttonCall";
            this.buttonCall.Size = new System.Drawing.Size(214, 97);
            this.buttonCall.TabIndex = 10;
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.buttonCall_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.Location = new System.Drawing.Point(660, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(214, 97);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonCallDeclined
            // 
            this.buttonCallDeclined.Image = ((System.Drawing.Image)(resources.GetObject("buttonCallDeclined.Image")));
            this.buttonCallDeclined.Location = new System.Drawing.Point(770, 14);
            this.buttonCallDeclined.Name = "buttonCallDeclined";
            this.buttonCallDeclined.Size = new System.Drawing.Size(104, 97);
            this.buttonCallDeclined.TabIndex = 9;
            this.buttonCallDeclined.UseVisualStyleBackColor = true;
            this.buttonCallDeclined.Click += new System.EventHandler(this.buttonCallDeclined_Click);
            // 
            // buttonCallAccepted
            // 
            this.buttonCallAccepted.Image = ((System.Drawing.Image)(resources.GetObject("buttonCallAccepted.Image")));
            this.buttonCallAccepted.Location = new System.Drawing.Point(660, 14);
            this.buttonCallAccepted.Name = "buttonCallAccepted";
            this.buttonCallAccepted.Size = new System.Drawing.Size(104, 97);
            this.buttonCallAccepted.TabIndex = 8;
            this.buttonCallAccepted.UseVisualStyleBackColor = true;
            this.buttonCallAccepted.Click += new System.EventHandler(this.buttonCallAccepted_Click);
            // 
            // listViewFriends
            // 
            this.listViewFriends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewFriends.HideSelection = false;
            this.listViewFriends.Location = new System.Drawing.Point(660, 117);
            this.listViewFriends.Name = "listViewFriends";
            this.listViewFriends.Size = new System.Drawing.Size(214, 316);
            this.listViewFriends.TabIndex = 6;
            this.listViewFriends.UseCompatibleStateImageBehavior = false;
            this.listViewFriends.View = System.Windows.Forms.View.Details;
            this.listViewFriends.ItemActivate += new System.EventHandler(this.listViewFriends_ItemActivate);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Użytkownik";
            this.columnHeader1.Width = 165;
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.Location = new System.Drawing.Point(584, 404);
            this.sendMessageButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.Size = new System.Drawing.Size(70, 29);
            this.sendMessageButton.TabIndex = 5;
            this.sendMessageButton.Text = "Wyślij";
            this.sendMessageButton.UseVisualStyleBackColor = true;
            this.sendMessageButton.Click += new System.EventHandler(this.sendMessageButton_Click);
            // 
            // messageText
            // 
            this.messageText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.messageText.Location = new System.Drawing.Point(14, 404);
            this.messageText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.messageText.Name = "messageText";
            this.messageText.Size = new System.Drawing.Size(562, 29);
            this.messageText.TabIndex = 4;
            // 
            // TabControlChat
            // 
            this.TabControlChat.Location = new System.Drawing.Point(12, 14);
            this.TabControlChat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.TabControlChat.Name = "TabControlChat";
            this.TabControlChat.SelectedIndex = 0;
            this.TabControlChat.Size = new System.Drawing.Size(646, 388);
            this.TabControlChat.TabIndex = 0;
            this.TabControlChat.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControlChat_DrawItem);
            this.TabControlChat.SelectedIndexChanged += new System.EventHandler(this.TabControlChat_SelectedIndexChanged);
            this.TabControlChat.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabControlChat_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 450);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl TabControlChat;
        private System.Windows.Forms.Button sendMessageButton;
        private System.Windows.Forms.TextBox messageText;
        private System.Windows.Forms.ListView listViewFriends;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Button buttonCallDeclined;
        private System.Windows.Forms.Button buttonCallAccepted;
    }
}