
using System.Windows.Forms;
using CallMeMaybe;
using CallMeMaybe.Domain.Contract.Requests;


namespace CallMeMaybe.UI
{
    public partial class LoginForm : Form
    {

        public AuthorizationManager AuthorizationManager { get; set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {
            LoginViewModel login = new LoginViewModel()
            {
                Email = LogincomboBox.Text,
                Password = PasswordcomboBox.Text,
            };
            AuthorizationManager = new AuthorizationManager(login);
            await AuthorizationManager.RefreshToken();

            if (AuthorizationManager.AuthenticateResult.Success == true)
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
