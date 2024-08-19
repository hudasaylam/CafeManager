using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CafeSystem
{
    public partial class Discounts : Form
    {
        string connectionString = "Server=LAPTOP-63JN59UH;Database=cafesystem;Trusted_Connection=True;";

        public Discounts()
        {
            InitializeComponent();
        }

        private void Discounts_Load(object sender, EventArgs e)
        {
            LoadData();
            PopulateComboBox();


        }
        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT DiscountName ,DiscountType , DiscountValue ,StartDate, EndDate,ProductID FROM Discounts";
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

        private void PopulateComboBox()
        {

            string query = "SELECT ProductID, ProductName FROM Products";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                // Set the DataSource, DisplayMember, and ValueMember properties
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "ProductName";
                comboBox2.ValueMember = "ProductID";
            }
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            string DisName = textBox1.Text;
            string DisType = comboBox1.Text;

            DateTime SrtDate = dateTimePicker1.Value.Date;
            DateTime EndDate = dateTimePicker2.Value.Date;
            string PrdName = comboBox2.Text;
            int prdID = (int)comboBox2.SelectedValue;
            string Disinput = textBox1.Text;
            double DisValue = 0;

            try
            {
                DisValue = Convert.ToDouble(textBox3.Text);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Insert into Discounts table
                            string insertOrderQuery = "INSERT INTO Discounts (DiscountName, DiscountType, DiscountValue, StartDate, EndDate, ProductID) " +
                                                      "VALUES (@DisName, @DisType, @DisValue, @SrtDate, @EndDate, @prdID)";
                            using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@DisName", DisName);
                                cmd.Parameters.AddWithValue("@DisType", DisType);
                                cmd.Parameters.AddWithValue("@DisValue", DisValue);  // Fixed parameter name
                                cmd.Parameters.AddWithValue("@SrtDate", SrtDate);    // Fixed parameter name
                                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                                cmd.Parameters.AddWithValue("@prdID", prdID);

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            LoadData();

                            MessageBox.Show("Discount added successfully!");
                        }
                        catch (SqlException ex)
                        {
                            transaction.Rollback();  // Rollback in case of an error
                            MessageBox.Show($"SQL Error: {ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();  // Rollback in case of an error
                            MessageBox.Show($"Error: {ex.Message}");
                        }
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid decimal number.");
            }
            catch (OverflowException)
            {
                MessageBox.Show("The number entered is too large or too small.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }



        }

        private void deletebtn_Click(object sender, EventArgs e)
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
                                if (row.Cells["DiscountName"].Value != null &&
                                    row.Cells["DiscountType"].Value != null &&
                                    row.Cells["DiscountValue"].Value != null)
                                {
                                    string DisName = row.Cells["DiscountName"].Value.ToString();
                                    string DisType = row.Cells["DiscountType"].Value.ToString();
                                    decimal DisValue = Convert.ToDecimal(row.Cells["DiscountValue"].Value);

                                    string deleteQuery = "DELETE FROM Discounts WHERE DiscountName = @DisName AND DiscountType = @DisType AND DiscountValue = @DisValue";

                                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@DisName", DisName);
                                        cmd.Parameters.AddWithValue("@DisType", DisType);
                                        cmd.Parameters.AddWithValue("@DisValue", DisValue);

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            transaction.Commit();

                            // Remove rows from DataGridView after committing the transaction
                            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                            {
                                dataGridView1.Rows.Remove(row);
                            }

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
