
using System.Net;
using System.Windows.Forms;
using CallMeMaybe;
using CallMeMaybe.Domain.Contract.Requests;
using CallMeMaybe.Domain.Contract.Result;


namespace CallMeMaybe.UI
{
    public partial class LoginForm : Form
    {

        public HttpAuthorizationResult AuthorizationResult { get; set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {
            LoginModelView login = new LoginModelView()
            {
                Email = LogincomboBox.Text,
                Password = PasswordcomboBox.Text,
            };

            AuthorizationResult = await HttpRestClient.LoginAsync(login);
            if (AuthorizationResult.Code == HttpStatusCode.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Błędny login lub hasło");
            }
        }
    }
}
