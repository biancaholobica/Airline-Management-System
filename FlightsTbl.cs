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

namespace AirlineManagement
{
    public partial class FlightsTbl : Form
    {
        public FlightsTbl()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void btnRecord_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtFlightCode.Text) &&
                    cmbSource.SelectedItem != null &&
                    cmbDestination.SelectedItem != null &&
                    int.TryParse(txtNumberSeats.Text, out int seats))
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        con.Open();
                        string query = "INSERT INTO Flights (FlightCode, Source, Destination, TakeOffDate, Seats) VALUES (@FlightCode, @Source, @Destination, @TakeOffDate, @Seats)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@FlightCode", txtFlightCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@Source", cmbSource.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Destination", cmbDestination.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@TakeOffDate", dtpDate.Value);
                            cmd.Parameters.AddWithValue("@Seats", seats);

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Flight recorded successfully!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid flight code, source, destination, and number of seats.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFlightCode.Text = "";
            txtNumberSeats.Text = "";
            cmbDestination.Text = "";
            cmbSource.Text = "";
        }

        private void btnFlights_Click(object sender, EventArgs e)
        {
            ViewFlights viewFlightsForm = new ViewFlights();

            viewFlightsForm.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

            Home homeForm = new Home();
            homeForm.Show();
        }
    }
}
