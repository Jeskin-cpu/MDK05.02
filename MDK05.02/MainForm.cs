
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace MDK05._02
{
    public partial class MainForm : Form
    {
        private SQLiteConnection connection;
        private SQLiteDataAdapter dataAdapter;
        private DataTable dataTable;

        public MainForm()
        {
            InitializeComponent();

            // Установка соединения с базой данных SQLite
            string connectionString = "Data Source=pvkkkbased.db;Version=3;";
            connection = new SQLiteConnection(connectionString);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTable = comboBox.SelectedItem.ToString();

            // Подготовка SQL-запроса
            string query = $"SELECT * FROM {selectedTable}";

            // Создание и настройка адаптера данных
            dataAdapter = new SQLiteDataAdapter(query, connection);
            dataTable = new DataTable();

            // Заполнение DataGridView
            dataAdapter.Fill(dataTable);
            dataGridView.DataSource = dataTable;
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            // Заполнение ComboBox и установка обработчика события
            comboBox.Items.Add("Clients");
            comboBox.Items.Add("Orders");
            comboBox.Items.Add("Rabotniki");
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
        }
        private void ExecuteQuery(string query)
        {
            // Создание объекта команды SQLiteCommand
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                // Создание адаптера данных SQLiteDataAdapter и таблицы DataTable
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();

                    // Заполнение таблицы данными из базы данных
                    adapter.Fill(dataTable);

                    // Установка таблицы в качестве источника данных для DataGridView
                    dataGridView.DataSource = dataTable;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT Имя, Фамилия, Отчество FROM Clients";
            ExecuteQuery(query);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Orders WHERE [Тип товара] = 'Электроника'";
            ExecuteQuery(query);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            decimal amount = 2000; // замените на актуальное значение суммы
            string query = $"SELECT * FROM Orders WHERE Сумма < {amount}";
            ExecuteQuery(query);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            decimal amount = 5000; // замените на актуальное значение суммы
            string query = $"SELECT * FROM Orders WHERE Сумма > {amount}";
            ExecuteQuery(query);
        }
    }
}
