using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeSystem
{
    public partial class Admin : Form
    {
        string connectionString = "Server=LAPTOP-63JN59UH;Database=cafesystem;Trusted_Connection=True;";

        public Admin()
        {
            InitializeComponent();

        }



        private void Admin_Load(object sender, EventArgs e)
        {
            LoadData();


        }
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT ProductCategory, ProductName, Size, Price FROM Products";
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


        private void Add_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string category = comboBox1.Text;
            decimal Price = Convert.ToDecimal(textBox2.Text);
            string Size = comboBox2.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert into Orders table and get the new OrderID
                        string insertOrderQuery = "INSERT INTO Products (ProductName , ProductCategory , Price , Size)  VALUES (@name ,@category , @Price,@Size )";
                        using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction))
                        {
                            // Execute the command and retrieve the new OrderID
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@category", category);
                            cmd.Parameters.AddWithValue("@Price", Price);
                            cmd.Parameters.AddWithValue("@Size", Size);
                            cmd.ExecuteNonQuery();



                        }

                        transaction.Commit();

                        LoadData();

                        MessageBox.Show("Product added successfully!");

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                            {
                                string name = row.Cells["ProductName"].Value.ToString();
                                string category = row.Cells["ProductCategory"].Value.ToString();
                                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                                string size = row.Cells["Size"].Value.ToString();

                                string deleteQuery = "DELETE FROM Products WHERE ProductName = @name AND ProductCategory = @category AND Price = @price AND Size = @size";

                                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Parameters.AddWithValue("@category", category);
                                    cmd.Parameters.AddWithValue("@price", price);
                                    cmd.Parameters.AddWithValue("@size", size);

                                    cmd.ExecuteNonQuery();
                                }

                                dataGridView1.Rows.Remove(row);
                            }

                            transaction.Commit();

                            LoadData();

                            MessageBox.Show("Selected rows deleted successfully!");
                        }
                        catch (SqlException ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"SQL Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to remove from the cart.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            EditAdmins edmns = new EditAdmins();
            edmns.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Discounts discounts = new Discounts();
            discounts.Show();
        }
    }
}



        
    
    
    

