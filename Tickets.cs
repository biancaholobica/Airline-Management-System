using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagement
{
    public partial class Tickets : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        public Tickets()
        {
            InitializeComponent();
            PopulateFlightCodes(); 
            PopulatePassengerIds(); 
            PopulateDataGridView(); 
        }

        private void PopulateFlightCodes()
        {
            try
            {
                Con.Open();
                string query = "SELECT FlightCode FROM Flights";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbFlightCode.Items.Add(rdr["FlightCode"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while populating flight codes: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void PopulatePassengerIds()
        {
            try
            {
                Con.Open();
                string query = "SELECT PassengerId FROM PassengerTbl";
                SqlCommand cmd = new SqlCommand(query, Con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbPassId.Items.Add(rdr["PassengerId"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while populating passenger IDs: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void PopulateDataGridView()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM Tickets";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgvTickets.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while populating DataGridView: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtAge.Text = "";
            txtAmount.Text = "";
            txtName.Text = "";
            txtPassport.Text = "";
            //txtTicketId.Text = "";
            cmbFlightCode.SelectedItem = "";
            cmbNationality.SelectedItem = "";
            cmbPassId.SelectedItem = "";
        }

        private void btnBook_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    con.Open();
                    string query = "INSERT INTO Tickets (FlightCode, PassId, PassName, PassPassport, PassNationality, PassAge, Amount) VALUES (@FlightCode, @PassId, @PassName, @PassPassport, @PassNationality, @PassAge, @Amount)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FlightCode", cmbFlightCode.Text);
                    cmd.Parameters.AddWithValue("@PassId", cmbPassId.Text);
                    cmd.Parameters.AddWithValue("@PassName", txtName.Text);
                    cmd.Parameters.AddWithValue("@PassPassport", txtPassport.Text);
                    cmd.Parameters.AddWithValue("@PassNationality", cmbNationality.Text);
                    cmd.Parameters.AddWithValue("@PassAge", txtAge.Text);
                    cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket booked successfully!");
                    PopulateDataGridView(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while booking ticket: " + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

            Home homeForm = new Home();
            homeForm.Show();
        }
    }
}
