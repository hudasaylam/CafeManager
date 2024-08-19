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
    public partial class EditAdmins : Form
    {
        string connectionString = "Server=LAPTOP-63JN59UH;Database=cafesystem;Trusted_Connection=True;";

        public EditAdmins()
        {
            InitializeComponent();
        }

        private void EditAdmins_Load(object sender, EventArgs e)
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
                    string query = "SELECT AdminID , UserName FROM Admins";
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

        private void button1_Click(object sender, EventArgs e)
        {
            string UserName = textBox1.Text;
            string password =textBox2.Text;
           

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert into Orders table and get the new OrderID
                        string insertOrderQuery = "INSERT INTO Admins (UserName , Password )  VALUES (@username,@password )";
                        using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction))
                        {
                            // Execute the command and retrieve the new OrderID
                            cmd.Parameters.AddWithValue("@username", UserName);
                            cmd.Parameters.AddWithValue("@password",password);
                            
                            cmd.ExecuteNonQuery();



                        }

                        transaction.Commit();

                        LoadData();

                        MessageBox.Show("Admin added successfully!");

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

        private void button2_Click(object sender, EventArgs e)
            
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
                                string username = row.Cells["UserName"].Value.ToString();
                                

                                string deleteQuery = "DELETE FROM Admins WHERE UserName = @username  ";

                                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn, transaction))
                                {
                                    // Add parameters to the command
                                    cmd.Parameters.AddWithValue("@userName", username);
                     
                                    // Execute the delete command
                                    cmd.ExecuteNonQuery();
                                }

                                // Remove the selected row from the DataGridView
                                dataGridView1.Rows.Remove(row);
                            }

                            // Commit the transaction
                            transaction.Commit();

                            // Reload the data to show the updated list
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
                MessageBox.Show("Please select an item to remove .");
            }
        }



    }
    }

