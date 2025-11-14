using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineManagement
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();

            LoginForm login = new LoginForm();
            login.Show();
        }

        private void btnFlights_Click(object sender, EventArgs e)
        {
            FlightsTbl flightsTblForm = new FlightsTbl();
            flightsTblForm.Show();

        }

        private void btnPassengers_Click(object sender, EventArgs e)
        {
            AddPassenger addPassengerForm = new AddPassenger();
            addPassengerForm.Show();
        }

        private void btnTickets_Click(object sender, EventArgs e)
        {
            Tickets ticketsForm = new Tickets();
            ticketsForm.Show();
        }

        private void btnCancellation_Click(object sender, EventArgs e)
        {
            CancellationTbl cancellationTblForm = new CancellationTbl();
            cancellationTblForm.Show();
        }
    }
}
