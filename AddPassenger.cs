using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineManagement
{
    public partial class AddPassenger : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        public AddPassenger()
        {
            InitializeComponent();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassID.Text) || string.IsNullOrEmpty(txtPassName.Text) || string.IsNullOrEmpty(txtPassAdress.Text) || string.IsNullOrEmpty(txtPassNumber.Text) || string.IsNullOrEmpty(txtPhoneNumber.Text) || cmbNationality.SelectedItem == null || cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields.");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "INSERT INTO PassengerTbl VALUES (@PassID, @PassName, @PassAdress, @PassNumber, @Nationality, @Gender, @PhoneNumber)";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@PassID", txtPassID.Text);
                    cmd.Parameters.AddWithValue("@PassName", txtPassName.Text);
                    cmd.Parameters.AddWithValue("@PassAdress", txtPassAdress.Text);
                    cmd.Parameters.AddWithValue("@PassNumber", txtPassNumber.Text);
                    cmd.Parameters.AddWithValue("@Nationality", cmbNationality.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passenger recorded successfully!");
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
        }

        private void btnViewPassengers_Click(object sender, EventArgs e)
        {
            ViewPassengers viewpass = new ViewPassengers();
            viewpass.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassID.Text = "";
            txtPassName.Text = "";
            txtPassAdress.Text = "";
            txtPassNumber.Text = "";
            cmbNationality.SelectedItem = null;
            cmbGender.SelectedItem = null;
            txtPhoneNumber.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

            Home homeForm = new Home();
            homeForm.Show();
        }

        private void AddPassenger_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home homeForm = new Home();
            homeForm.Show();
            this.Dispose();
        }
    }
}
