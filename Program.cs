using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new AddPassenger());
            //Application.Run(new ViewPassengers());
            //Application.Run(new FlightsTbl());
            // Application.Run(new ViewFlights());
            //Application.Run(new Tickets());
            //Application.Run(new Home());
            Application.Run(new LoginForm());
            //Application.Run(new LoadingForm());
        }
    }
}
