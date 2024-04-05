using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacredSeedsSystem
{
    public class Session
    {
        public int? SessionId { get; set; }
        public string? SessionNumber { get; set; }
        public int? SessionType { get; set; }
        public string? ExtraServices { get; set; }
        public string? ClientId { get; set; }
        public double? Price { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? AppointmentTime { get; set;}
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? PaymentStatus { get; set; }
        public string? UsageStatus { get; set; }
        public string? Notes { get; set; }

        public Session(int sessionId, string sessionNumber, int sessionType, string extraServices, string clientId, double price, DateTime appointmentDate, DateTime appointmentTime, DateTime startTime, DateTime endTime, string paymentStatus, string usageStatus, string notes)
        {
            SessionId = sessionId;
            SessionNumber = sessionNumber;
            SessionType = sessionType;
            ExtraServices = extraServices;
            ClientId = clientId;
            Price = price;
            AppointmentDate = appointmentDate;
            AppointmentTime = appointmentTime;
            StartTime = startTime;
            EndTime = endTime;
            PaymentStatus = paymentStatus;
            UsageStatus = usageStatus;
            Notes = notes;
        }

        // Constructor with missing parameters (set to null)
        public Session(int sessionId, string sessionNumber, int sessionType, string extraServices, string clientId, double price, string paymentStatus, string usageStatus, string notes)
        {
            SessionId = sessionId;
            SessionNumber = sessionNumber;
            SessionType = sessionType;
            ExtraServices = extraServices;
            ClientId = clientId;
            Price = price;
            PaymentStatus = paymentStatus;
            UsageStatus = usageStatus;
            Notes = notes;
        }

        public override string ToString()
        {
            return $"{SessionId}, {SessionNumber}, {SessionType}, {ExtraServices}, {ClientId}, {Price}, {AppointmentDate?.ToString("yyyy-MMM-dd")}, {AppointmentTime?.ToString("HH:mm")}, {StartTime?.ToString("HH:mm")}, {EndTime?.ToString("HH:mm")}, {PaymentStatus}, {UsageStatus}, {Notes}";
        }

        public static Session Parse(String text)
        {
            string[] parts = text.Split(",");
            Session NewSession = null;

            if (parts.Length == 13)
            {
                NewSession = new Session(Int32.Parse(parts[0]), parts[1], Int32.Parse(parts[2]), parts[3], parts[4], Double.Parse(parts[5]), DateTime.Parse(parts[6]), DateTime.Parse(parts[7]), DateTime.Parse(parts[8]), DateTime.Parse(parts[9]), parts[10], parts[11], parts[12]);
            }
            else if (parts.Length < 13) // If some parameters are missing
            {
                NewSession = new Session(Int32.Parse(parts[0]), parts[1], Int32.Parse(parts[2]), parts[3], parts[4], Double.Parse(parts[5]), parts[6], parts[7], parts[8]);
            }

            return NewSession;
        }
    }
}
