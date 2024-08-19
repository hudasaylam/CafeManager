using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CafeSystem
{
    public partial class AddOrder : Form
    {
        string connectionString = "Server=LAPTOP-63JN59UH;Database=cafesystem;Trusted_Connection=True;";

        // List of categories to populate the ComboBox
        private List<string> categories = new List<string>
        {
            "Coffee",
            "Tea",
            "Water",
            "Milkshake",
            "Juice",
            "Pastry",
            "HotChocolate"
        };

        public AddOrder()
        {
            InitializeComponent();
            // Ensure that the event handler is only subscribed once
            button1.Click -= button1_Click; // Unsubscribe first to avoid multiple subscriptions
            button1.Click += new EventHandler(button1_Click); // Then subscribe
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            ConfigureDataGridViews();
            ConfigureNumericUpDown();
        }

        private void ConfigureDataGridViews()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            cartview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cartview.MultiSelect = false;
        }

        private void LoadComboBoxData()
        {
            comboBox1.DataSource = categories;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCategory = comboBox1.SelectedItem.ToString();
            FilterDataGridView(selectedCategory);
        }

        private void FilterDataGridView(string category)
        {
            string query2 = "SELECT ProductID , ProductName, size FROM Products WHERE ProductCategory = @category";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter d = new SqlDataAdapter(query2, conn);
                d.SelectCommand.Parameters.AddWithValue("@category", category);

                DataSet ds = new DataSet();
                d.Fill(ds, "Products");

                dataGridView1.DataSource = ds.Tables["Products"];
            }
        }

        private void ConfigureNumericUpDown()
        {
            numericUpDownQuantity.Minimum = 1;
            numericUpDownQuantity.Maximum = 10;
            numericUpDownQuantity.Value = 1; // Default to 1
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Ensure the cart DataGridView has the same columns as dataGridView1
                if (cartview.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        cartview.Columns.Add((DataGridViewColumn)col.Clone());
                    }
                }

                // Get the selected quantity from NumericUpDown
                int quantity = (int)numericUpDownQuantity.Value;

                // Add the selected item to the cart multiple times based on the quantity
                for (int q = 0; q < quantity; q++)
                {
                    // Create a new row for the cart DataGridView
                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(cartview);

                    // Copy the values from the selected row to the new row
                    for (int i = 0; i < selectedRow.Cells.Count; i++)
                    {
                        newRow.Cells[i].Value = selectedRow.Cells[i].Value;
                    }

                    // Add the new row to the cart DataGridView
                    cartview.Rows.Add(newRow);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to add to the cart.");
            }
        }
        public void DisplayCartProducts()
        {
            if (cartview.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in cartview.Rows)
                {
                    // Assuming the DataGridView has columns "ProductName" and "Size"
                    string productName = row.Cells["ProductName"].Value.ToString();
                    string size = row.Cells["Size"].Value.ToString();


                }
            }
            else
            {
                MessageBox.Show("The cart is empty.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            
            decimal x;
                
            if (cartview.Rows.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                           

                            string insertOrderQuery = "INSERT INTO Orders (Name) OUTPUT INSERTED.OrderID VALUES (@name)";
                            int orderID;
                            using (SqlCommand cmd = new SqlCommand(insertOrderQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@name", name);

                                orderID = (int)cmd.ExecuteScalar();

                            }

                            // Insert each product into OrderList table
                            string insertOrderListQuery = "INSERT INTO OrderList (OrderID, ProductID) VALUES (@OrderID, @ProductID)";

                            foreach (DataGridViewRow row in cartview.Rows)
                            {
                                using (SqlCommand cmd = new SqlCommand(insertOrderListQuery, conn, transaction))
                                {
                                    // Add parameters for OrderID and ProductID
                                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                                    cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(row.Cells["ProductID"].Value));

                                    // Execute the command to insert into OrderList
                                    cmd.ExecuteNonQuery();
                                }
                              
                            }
                            string pricecalculate = "SELECT Total from Orders where OrderID = @orderID";
                            using (SqlCommand cmd = new SqlCommand(pricecalculate, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@OrderID", orderID);
                                cmd.ExecuteNonQuery();
                                x = (decimal)cmd.ExecuteScalar();

                                totalLabel.Text = x.ToString();

                            }

                            // Commit the transaction if all inserts are successful
                            transaction.Commit();
                            MessageBox.Show("Order placed successfully!");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if an error occurs
                            transaction.Rollback();
                            MessageBox.Show("An error occurred: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No products in the cart to place an order.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Check if any row is selected in the cartview
            if (cartview.SelectedRows.Count > 0)
            {
                // Loop through each selected row (in case multiple rows are selected)
                foreach (DataGridViewRow row in cartview.SelectedRows)
                {
                    // Remove the selected row from the cartview
                    cartview.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to remove from the cart.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

     
    }
}
