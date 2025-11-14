using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagement
{
    public partial class ViewFlights : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        public ViewFlights()
        {
            InitializeComponent();
            PopulateDataGridView();
        }

        private void PopulateDataGridView()
        {
            try
            {
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

                string query = "SELECT * FROM Flights";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dgvFlights.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while populating DataGridView: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            FlightsTbl flightsTblForm = new FlightsTbl();
            flightsTblForm.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFlightCode.Text = "";
            txtNumberSeats.Text = "";
            cmbDestination.Text = "";
            cmbSource1.Text = "";
            dtpdate.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string flightCodeToDelete = txtFlightCode.Text.Trim();

            if (!string.IsNullOrEmpty(flightCodeToDelete))
            {
                if (MessageBox.Show("Are you sure you want to delete this flight?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        if (Con.State == ConnectionState.Closed)
                        {
                            Con.Open();
                        }

                        string query = "DELETE FROM Flights WHERE FlightCode = @FlightCode";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.Parameters.AddWithValue("@FlightCode", flightCodeToDelete);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Flight deleted successfully!");
                            PopulateDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Flight not found or could not be deleted.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        if (Con.State == ConnectionState.Open)
                        {
                            Con.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a flight code to delete.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string flightCodeToUpdate = txtFlightCode.Text.Trim();

            if (!string.IsNullOrEmpty(flightCodeToUpdate))
            {
                try
                {
                    if (Con.State == ConnectionState.Closed)
                    {
                        Con.Open();
                    }

                    string query = "UPDATE Flights SET Source = @Source, Destination = @Destination, TakeOffDate = @TakeOffDate, Seats = @Seats WHERE FlightCode = @FlightCode";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@Source", cmbSource1.Text);
                    cmd.Parameters.AddWithValue("@Destination", cmbDestination.Text);
                    cmd.Parameters.AddWithValue("@TakeOffDate", dtpdate.Value);
                    cmd.Parameters.AddWithValue("@Seats", txtNumberSeats.Text);
                    cmd.Parameters.AddWithValue("@FlightCode", flightCodeToUpdate);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Flight updated successfully!");
                        PopulateDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Flight not found or could not be updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a flight code to update.");
            }
        } 
    }
}
