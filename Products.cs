using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace CafeSystem
{
    public partial class Products : Form
    {



        public Products()
        {
            InitializeComponent();
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dataGridView1.AutoSize = true;
            string connectionString = "Server=LAPTOP-63JN59UH;Database=cafesystem;Trusted_Connection=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT ProductCategory ,ProductName , Size ,Price  FROM Products ";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                          

                            dataGridView1.DataSource = dataTable;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

      

        private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder adrd = new AddOrder();
            adrd.Show();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Orders ord = new Orders();
            ord.Show();
                
        }
    }
}

