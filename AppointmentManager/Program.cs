using System;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentManager
{
    internal class Program
    {
        static List<Appointment> appointments = new List<Appointment>();
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("===== Appointment Manager =====");
                Console.WriteLine("1. Add Appointment");
                Console.WriteLine("2. View All Appointment");
                Console.WriteLine("3. View Upcoming (Next 7 Days)");
                Console.WriteLine("4. Search Appointments");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddAppointment();
                        break;
                    case "2":
                        ViewAllAppointments();
                        break;
                    case "3":
                        ViewUpcomingAppointments();
                        break;
                    case "4":
                        SearchAppointments();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
                Console.WriteLine();
            }

        }
        static void AddAppointment()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            DateTime date = DateTime.MinValue;
            bool validDate = false;

            while (!validDate)
            {
                Console.Write("Enter date (MM/dd/yyyy HH:mm): ");
                string inputDate = Console.ReadLine();

                if (DateTime.TryParse(inputDate, out date))
                {
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date. Please try again.");
                }
            }
            Appointment appt = new Appointment
            {
                Title = title,
                Description = description,
                Date = date
            };
            appointments.Add(appt);
            Console.WriteLine("Appointment added successfully");
        }
        static void ViewAllAppointments()
        {
            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }
            appointments.Sort((a, b) => a.Date.CompareTo(b.Date));
            Console.WriteLine("All Appointmeants:");
            foreach (Appointment appt in appointments)
            {
                Console.WriteLine(appt.GetDisplayText());
                Console.WriteLine();
            }
        }
        static void ViewUpcomingAppointments()
        {
            DateTime now = DateTime.Now;
            DateTime next7Days = now.AddDays(7);

            var upcomingAppointments = appointments
                .Where(a => a.Date >= now && a.Date <= next7Days)
                .OrderBy(a => a.Date)
                .ToList();
            if (upcomingAppointments.Count == 0)
            {
                Console.WriteLine("No upcoming appointments in the next 7 days.");
                return;
            }
            Console.WriteLine("Upcoming Appointments (Next 7 Days):");
            foreach (Appointment appt in upcomingAppointments)
            {
                Console.WriteLine(appt.GetDisplayText());
                Console.WriteLine();
            }
        }
        static void SearchAppointments()
        {
            Console.Write("Enter search term: ");
            string keyword = Console.ReadLine().ToLower();
            var results = appointments
                .Where(a => a.Title.ToLower().Contains(keyword) ||
                           a.Description.ToLower().Contains(keyword))
                .OrderBy(a => a.Date)
                .ToList();
            if (results.Count == 0)
            {
                Console.WriteLine("No matching appointments found.");
                return;
            }
            Console.WriteLine("Search Results:");
            foreach (Appointment appt in results)
            {
                Console.WriteLine(appt.GetDisplayText());
                Console.WriteLine();
            }
        }
    }
}

