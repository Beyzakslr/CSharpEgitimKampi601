using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port=5433;Database=CustomerDb;user Id=postgres;Password=12345";

        void GetAllCustomers()
        {
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Customers";
            var command=new NpgsqlCommand(query, connection);
            var adapter=new NpgsqlDataAdapter(command);
            DataTable dataTAble=new DataTable();
            adapter.Fill(dataTAble);
            dataGridView1.DataSource = dataTAble;
            connection.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string customerName=txtName.Text;
            string customerSurname = txtSurname.Text;
            string customerCity=txtCity.Text;
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Insert into Customers (CustomerName, CustomerSurname,CustomerCity)" +
                "values (@cutsomerName,@cutsomerSurname,@customerCity)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@cutsomerName", customerName);
            command.Parameters.AddWithValue("@cutsomerSurname", customerSurname);
            command.Parameters.AddWithValue("@customerCity", customerCity);
            command.ExecuteNonQuery();
            MessageBox.Show("Başarıyla Eklenmiştir.");
            connection.Close();
            GetAllCustomers();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id=int.Parse(txtId.Text);
            var connection=new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Delete from customers where customerId=@customerId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@customerId", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Başarıyla Silinmiştir.");
            connection.Close();
            GetAllCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string customerName = txtName.Text;
            string customerSurname = txtSurname.Text;
            string customerCity = txtCity.Text;
            int id= int.Parse(txtId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Update Customers Set CustomerName=@customerName," +
                "CustomerSurname=@customerSurname,CustomerCity=@customerCity" +
                " where CustomerId=@customerId ";
            var command= new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("customerName", customerName);
            command.Parameters.AddWithValue("customerSurname", customerSurname);
            command.Parameters.AddWithValue("customerCity", customerCity);
            command.Parameters.AddWithValue("customerId", id);
            command.ExecuteNonQuery ();
            MessageBox.Show("Başarıyla Güncellenmiştir.");
            connection.Close();
            GetAllCustomers();
        }
    }
}
