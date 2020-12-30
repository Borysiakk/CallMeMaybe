using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using CallMeMaybe.Builder;
using CallMeMaybe.Args;

namespace CallMeMaybe.UI
{
    public partial class MainForm : Form
    {

        private Connection _connection;


        private Image _imageCloseTabPage;
        private ImageList _imageStatusList;
        private List<string> _imageNameList;
        private ResourceManager _resourceManager;

        private Point _imageLocation = new Point(20, 4);
        private Point _imgHitArea = new Point(20, 4);
        public MainForm()
        {
            InitializeComponent();
            TabControlChat.Padding = new Point(20, 4);
            TabControlChat.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {

            _resourceManager = Properties.Resources.ResourceManager;

            _imageStatusList = new ImageList();
            // ReSharper disable once ResourceItemNotResolved
            _imageCloseTabPage = (Bitmap)_resourceManager.GetObject("close");
            _imageNameList = new List<string>() {"False","True"};
            
            foreach (var imageName in _imageNameList)
            {
                _imageStatusList.Images.Add((Bitmap)_resourceManager?.GetObject(imageName));
            }
            listViewFriends.SmallImageList = _imageStatusList;

            ConnectionBuilderConfiguration builderConfiguration = new ConnectionBuilderConfiguration
            {
                UpdateFriendsStatusDelegate = args =>
                {
                    if (args.Status == true)
                    {
                        listViewFriends.Items[args.UserName].ImageIndex = 1;
                        if (TabControlChat.Controls.Count != 0)
                        {
                            if (TabControlChat.SelectedTab == TabControlChat.TabPages[args.UserName])
                            {
                                sendMessageButton.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        listViewFriends.Items[args.UserName].ImageIndex = 0;
                        if (TabControlChat.Controls.Count != 0)
                        {
                            if (TabControlChat.SelectedTab == TabControlChat.TabPages[args.UserName])
                            {
                                sendMessageButton.Enabled = false;
                            }
                        }
                    }
                    
                },
                ReceiveMessageDelegate = args =>
                {
                    if (TabControlChat.TabPages[args.UserName] == null)
                    {
                        TabPage page = new TabPage(args.UserName) {Name = args.UserName};
                        RichTextBox richTextBox = new RichTextBox
                        {
                            Text = args.UserName + @": " + args.Message, Dock = DockStyle.Fill, ReadOnly = true
                        };
                        page.Controls.Add(richTextBox);
                        TabControlChat.Controls.Add(page);
                    }
                    else
                    {
                        var page = TabControlChat.TabPages[args.UserName];
                        RichTextBox richTextBox = page.Controls[0] as RichTextBox;
                        richTextBox.AppendText("\r\n");
                        richTextBox.AppendText(args.UserName + @": " + args.Message);
                    }
                },
                SendMessageDelegate = args =>
                {
                    TabPage page = TabControlChat.TabPages[args.UserName];
                    RichTextBox richTextBox = page.Controls[0] as RichTextBox;
                    richTextBox.AppendText("\r\n");
                    richTextBox?.AppendText(_connection.AuthorizationManager.AuthenticateResult.User + ":" + args.Message);
                }
                
            };



            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                builderConfiguration.AuthorizationManager = loginForm.AuthorizationManager;
                _connection = await ConnectionBuilder.Create(builderConfiguration);

                foreach(var friend in _connection.Friends)
                {
                    if (friend.Value == true)
                        listViewFriends.Items.Add(friend.Key, friend.Key, 1);
                    else
                        listViewFriends.Items.Add(friend.Key, friend.Key, 0);
                }
            }
        }

        private async void sendMessageButton_Click(object sender, EventArgs e)
        {
            string userName = TabControlChat.SelectedTab.Text;
            await _connection.Send(userName, messageText.Text);
        }
        
        private void listViewFriends_ItemActivate(object sender, EventArgs e)
        {

            var itemFriendSelect = listViewFriends.SelectedItems[0];
            string nameFriendSelect = listViewFriends.SelectedItems[0].Text;
            
            var itemFriendChat = TabControlChat?.TabPages[itemFriendSelect.Text];
            bool statusFriendUser = itemFriendSelect.ImageIndex != 0;
            
            if (itemFriendChat == null)
            {

                if (statusFriendUser)
                {
                    TabPage page = new TabPage(nameFriendSelect) {Name = nameFriendSelect};
                    
                    RichTextBox richTextBox = new RichTextBox { Dock = DockStyle.Fill, ReadOnly = true};
                    page.Controls.Add(richTextBox);
                    TabControlChat?.Controls.Add(page);
                }
            }
            else
            {
                if(statusFriendUser)
                {
                    TabControlChat.SelectedTab = TabControlChat.TabPages[nameFriendSelect];
                }
            }
        }

        private void TabControlChat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControlChat.Controls.Count != 0)
            {
                ListView view = sender as ListView;
                var pageName = TabControlChat.SelectedTab.Text;
                var listFriendItem = listViewFriends.Items[pageName];

                if (listFriendItem.ImageIndex == 1)
                {
                    sendMessageButton.Enabled = true;
                }
                else
                {
                    sendMessageButton.Enabled = false;
                }
            }
        }

        private void TabControlChat_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle r = e.Bounds;
            r = TabControlChat.GetTabRect(e.Index);
            r.Offset(2, 2);
            Brush titleBrush = new SolidBrush(Color.Black);
            Font f = this.Font;
            string title = TabControlChat.TabPages[e.Index].Text;
            e.Graphics.DrawString(title, f, titleBrush, new PointF(r.X, r.Y));
            e.Graphics.DrawImage(_imageCloseTabPage,
                new PointF(r.X + (TabControlChat.GetTabRect(e.Index).Width - _imageLocation.X), 15 - ( (TabControlChat.GetTabRect(e.Index).Height - _imageLocation.Y) / 2)));
        }

        private void TabControlChat_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            var width = this.TabControlChat.GetTabRect(TabControlChat.SelectedIndex).Width - (_imgHitArea.X);
            Rectangle r = this.TabControlChat.GetTabRect(TabControlChat.SelectedIndex);
            r.Offset(width, _imgHitArea.Y);
            r.Width = 16;
            r.Height = 16;
            if (TabControlChat.SelectedIndex >= 0)
            {
                if (r.Contains(p))
                {
                    TabPage tabPage = (TabPage)TabControlChat.TabPages[TabControlChat.SelectedIndex];
                    TabControlChat.TabPages.Remove(tabPage);
                }
            }
        }
    }
}