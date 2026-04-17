using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager
{
    public class Appointment
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string GetDisplayText()
        {
            return $"[{Date:MM/dd/yyyy HH:mm}] {Title}\n{Description}"; 
        }
    }
}
