using Nancy.Json;
using System.Net.Mail;
using System.Text.Json;
using static DataBase;

namespace Bank_Project
{
    
    public partial class RegistrationUser : Form
    {
        public RegistrationUser()
        {
            
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            UserType? userType = null;
            DataBase dataBase = new DataBase();
            if (comboBoxType.SelectedIndex == -1)
                MessageBox.Show("Choose user type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (comboBoxType.SelectedIndex == 0)
            {
                MailAddress? mailAddress = null;
                MailAddress.TryCreate(textBoxMail.Text, out mailAddress);
                if (string.IsNullOrWhiteSpace(textBoxName.Text) || string.IsNullOrWhiteSpace(textBoxSurname.Text)
                    || string.IsNullOrWhiteSpace(textBoxMail.Text) || string.IsNullOrWhiteSpace(textBoxPassword.Text)
                    || mailAddress == null)
                {
                    MessageBox.Show("Enter all field correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    userType = UserType.Simple;
                    dataBase.CreateUser(userType, textBoxName.Text, textBoxSurname.Text, textBoxMail.Text, textBoxPassword.Text);
                    Close();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBoxName.Text) || string.IsNullOrWhiteSpace(textBoxSurname.Text)
                    || string.IsNullOrWhiteSpace(textBoxMail.Text) || string.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    MessageBox.Show("Enter all field correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    userType = UserType.VIP;
                    dataBase.CreateUser(userType, textBoxName.Text, textBoxSurname.Text, textBoxMail.Text, textBoxPassword.Text);
                    Close();
                }
            }
        }

        private void comboBoxType_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Text = comboBoxType.SelectedIndex switch
            {
                0 => "Mail",
                1 => "VIP Code",
                _ => ""
            };
        }
    }
}
