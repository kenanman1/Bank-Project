using ClassLibrary1;
using DB;
using Microsoft.EntityFrameworkCore;
using static DataBase;

namespace Bank_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            //string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //FileStream stream = new FileStream(path+"\\kek.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            
            comboBox1.SelectedIndex = 0;
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            RegistrationUser registration = new RegistrationUser();
            registration.ShowDialog();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            UserType type = comboBox1.SelectedIndex switch
            {
                0 => type = UserType.Simple,
                1 => type = UserType.VIP,
                _ => throw new Exception()
            };

            DataBase data = new DataBase();
            bool check = data.CheckUser(type, textBoxMailOrCode.Text, textBoxPassword.Text);
            if (check)
            {
                Settings1.Default.MailOrCode = textBoxMailOrCode.Text;
                Hide();
                Menu menu = new Menu(type);
                menu.ShowDialog();
            }
            else
            {
                MessageBox.Show("Check if the input is correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelMailOrCode.Text = comboBox1.SelectedIndex switch
            {
                0 => "Mail",
                1 => "VIP Code",
                _ => ""
            };
        }
    }
}