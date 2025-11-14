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
    public partial class CancellationTbl : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bholo\OneDrive\Documente\AirlineDb.mdf;Integrated Security=True;Connect Timeout=30");

        public CancellationTbl()
        {
            InitializeComponent();
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                string selectQuery = "SELECT * FROM Tickets WHERE TicketId = @TicketId";
                SqlCommand selectCmd = new SqlCommand(selectQuery, Con);
                selectCmd.Parameters.AddWithValue("@TicketId", txtTicketId.Text);
                SqlDataReader reader = selectCmd.ExecuteReader();

                DataTable deletedRow = new DataTable();
                deletedRow.Load(reader);

                string deleteQuery = "DELETE FROM Tickets WHERE TicketId = @TicketId";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, Con);
                deleteCmd.Parameters.AddWithValue("@TicketId", txtTicketId.Text);
                deleteCmd.ExecuteNonQuery();

                MessageBox.Show("Ticket cancelled successfully!");


                dgvCancellations.DataSource = deletedRow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while cancelling ticket: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTicketId.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

            Home homeForm = new Home();
            homeForm.Show();
        }
    }
}
