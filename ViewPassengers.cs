using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AirlineManagement
{
    public partial class ViewPassengers : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        public ViewPassengers()
        {
            InitializeComponent();
            // Stilizare pentru DataGridView dgvPassengers
            dgvPassengers.DefaultCellStyle.BackColor = Color.LightBlue;
            dgvPassengers.DefaultCellStyle.ForeColor = Color.Black;
            dgvPassengers.DefaultCellStyle.Font = new Font("Arial", 10);

            dgvPassengers.ColumnHeadersDefaultCellStyle.BackColor = Color.LightCoral; 
            dgvPassengers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPassengers.ColumnHeadersDefaultCellStyle.Font = new Font(dgvPassengers.Font, FontStyle.Bold);

            dgvPassengers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvPassengers.ScrollBars = ScrollBars.Horizontal;


        }

        private void populate()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM PassengerTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                var ds = new DataSet();
                sda.Fill(ds);
                dgvPassengers.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void ViewPassengers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AddPassenger addpass = new AddPassenger();
            addpass.Show();
            this.Hide();
        }

        private void dgvPassengers_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                populate(); 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Please enter the Passenger ID to delete.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "DELETE FROM PassengerTbl WHERE PassengerId = @PassengerId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@PassengerId", txtID.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger deleted successfully!");
                    Con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtPass.Text = "";
            txtAdress.Text = "";
            txtName.Text = "";
            txtPhoneNumber.Text = "";
            cmbGender.SelectedItem = "";
            cmbNationality.SelectedItem = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Please enter the Passenger ID to update.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "UPDATE PassengerTbl SET PassengerName = @PassengerName, PassengerAdress = @PassengerAdress, Passport = @Passport, PassengerNationality = @PassengerNationality, PassengerGender = @PassengerGender, PassengerPhoneNumber = @PassengerPhoneNumber WHERE PassengerId = @PassengerId";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@PassengerName", txtName.Text);
                    cmd.Parameters.AddWithValue("@PassengerAdress", txtAdress.Text);
                    cmd.Parameters.AddWithValue("@Passport", txtPass.Text); 
                    cmd.Parameters.AddWithValue("@PassengerNationality", cmbNationality.SelectedItem);
                    cmd.Parameters.AddWithValue("@PassengerGender", cmbGender.SelectedItem);
                    cmd.Parameters.AddWithValue("@PassengerPhoneNumber", txtPhoneNumber.Text); 
                    cmd.Parameters.AddWithValue("@PassengerId", txtID.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Passenger updated successfully!");
                        populate();
                    }
                    else
                    {
                        MessageBox.Show("Passenger not found or could not be updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
    }
}
