using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace MDK05._02
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = LoginText.Text;
            string password = PassText.Text;

            if (CheckCredentials(username, password))
            {
                if (IsAdmin(username))
                {
                    // Открыть форму администратора
                    MainForm adminForm = new MainForm();
                    adminForm.Show();
                    this.Hide();
                }
                else
                {
                    // Открыть главную форму пользователя
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Неправильное имя пользователя или пароль");
            }
        }
        private bool CheckCredentials(string username, string password)
        {
            string connectionString = "Data Source=pvkkkbased.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Clients WHERE Имя = @username AND Пароль = @password";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        private bool IsAdmin(string username)
        {
            string connectionString = "Data Source=pvkkkbased.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Rabotniki WHERE Имя = @username";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
    }
}
