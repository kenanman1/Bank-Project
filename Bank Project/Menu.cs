using ClassLibrary1;
using Nancy.Json;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static DataBase;

namespace Bank_Project
{
    public partial class Menu : Form
    {
        //events

        UserType userType;
        public Menu(UserType type)
        {
            InitializeComponent();
            userType = type;
            UpdateInfo();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void buttonDeposit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxDeposit.Text))
            {
                DataBase data = new DataBase();
                data.MakeDeposit(userType, double.Parse(textBoxDeposit.Text));
                UpdateInfo();
                textBoxDeposit.Text = "";
                MessageBox.Show("Completed", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Enter correct deposit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void UpdateInfo()
        {
            DataBase data = new DataBase();
            if (userType == UserType.Simple)
            {
                User? user = data.FindUser(userType, Settings1.Default.MailOrCode) as User;
                labelName.Text = user.Name;
                labelSurname.Text = user.Surname;
                labelMailOrCode.Text = user.Email;
                labelType.Text = userType.ToString();
                labelId.Text = user.Id.ToString();
                labelAmount.Text = user.BankAccount.Amount.ToString() + "$";
                labelPercent.Text = user.BankAccount.Percent.ToString();
            }
            else
            {
                VIP_User? user = data.FindUser(userType, Settings1.Default.MailOrCode) as VIP_User;
                labelName.Text = user.Name;
                labelSurname.Text = user.Surname;
                labelMailOrCodeName.Text = "VIP Code:";
                labelMailOrCode.Text = user.VIP_Number;
                labelType.Text = userType.ToString();
                labelId.Text = user.Id.ToString();
                labelAmount.Text = user.AccountVIP.Amount.ToString() + "$";
                labelPercent.Text = user.AccountVIP.Percent.ToString();
            }
        }

        private void textBoxDeposit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void buttonGetHistory_Click(object sender, EventArgs e)
        {
            DataBase data = new DataBase();
            List<string> list = data.GetAmountHistories(userType);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter writer = new StreamWriter(path + "\\AmountHistory.txt", false))
            {
                foreach (var item in list)
                {
                    writer.WriteLine(item);

                }
            }
            MessageBox.Show("File was created at desktop", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonWithdraw_Click(object sender, EventArgs e)
        {
            bool check = !string.IsNullOrWhiteSpace(textBoxWithdraw.Text);
            if (check)
            {
                DataBase data = new DataBase();
                check = data.MakeWithdraw(userType, double.Parse(textBoxWithdraw.Text));
                if (check)
                {
                    UpdateInfo();
                    textBoxDeposit.Text = "";
                    MessageBox.Show("Completed", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (!check)
                MessageBox.Show("Enter correct withdraw", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
