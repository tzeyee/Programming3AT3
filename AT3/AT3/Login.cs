using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AT3.Password;

//Tze Yee Hon P466426
// AT3 05/12/2019

namespace AT3
{
    public partial class Login : Form
    {
        // Dummy repository class for DB operations
        static MockUserReposity userRepo = new MockUserReposity();
        //Use password manager class to generate password and salt
        static PasswordManager pwdManager = new PasswordManager();

        public Login()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            SignUp();
        }

        private void SignUp()
        {
            string id = tbSignupID.Text;
            string password = tbSignupPassword.Text;
            string salt = null;

            string passwordHash = pwdManager.GeneratePasswordHash(password, out salt);

            //save the value in database
            User user = new User
            {
                UserId = id,
                PasswordHash = passwordHash,
                Salt = salt
            };

            //add user to database
            userRepo.AddUser(user);

            MessageBox.Show("Sign up successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            tbSignupID.Clear(); // clear textbox  after signed up
            tbSignupPassword.Clear(); //clear textbox after signed up
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin();
        }

        private void UserLogin()
        {
            string id = tbID.Text;
            string password = tbPassword.Text;

            // Retrieve the values from the database
            User user2 = userRepo.GetUser(id);

            bool result = pwdManager.IsPasswordMatch(password, user2.Salt, user2.PasswordHash);

            //if textbox is empty or null, display message box to inform user
            if (string.IsNullOrEmpty(tbID.Text) || string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Please enter login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //if login detail is correct, display message box to inform user and open main form
            else if(result == true)
            { 
                MessageBox.Show("Login successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main main = new Main();
                main.ShowDialog();
            }
            //if login detail is incorrect, display message box to inform user 
            else
            {
                MessageBox.Show("Login failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
