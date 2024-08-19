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
    public partial class login : Form
    {
        string connectionString = "Server=LAPTOP-63JN59UH;Database=cafesystem;Trusted_Connection=True;";

        public login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string username = textBox1.Text;
                        string password = textBox2.Text;

                        string query = "SELECT COUNT(*) FROM admins WHERE UserName = @username AND Password = @password";

                        using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@password", password);

                            int result = Convert.ToInt32(cmd.ExecuteScalar());

                            if (result > 0)
                            {
                                MessageBox.Show("Login successful!");
                                Admin admin = new Admin();
                                admin.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password, please try again.");
                            }
                        }
                    }


                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

      
    }

}
