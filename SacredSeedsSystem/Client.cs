using System.Globalization;
using System.Xml.Linq;



namespace SacredSeedsSystem
{
    public class Client
    {
      
        public string? ClientId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ContactName { get; set; } 
        public string? PhoneNumber { get; set; }
        public int? Referrals { get; set; }
        public DateTime AppointmentDate {get; set; }

        public Client(string clientId, string name, string email, string contactName, string phoneNumber, int referrals, DateTime appointmentDate)
        {
            ClientId = clientId;
            Name = name;
            Email = email;
            ContactName = contactName;
            PhoneNumber = phoneNumber;
            Referrals = referrals;
            AppointmentDate = appointmentDate;
        }    
        
        public override string ToString()
        {

            return $"{ClientId},{Name}, {Email},{ContactName},{PhoneNumber}, {Referrals}, {AppointmentDate.ToString("MMM dd yyyy")}";
        }

        public static Client Parse(String text)
        {
            string[] parts = text.Split(",");
            Client NewClient = null;

            if (parts.Length == 7)
            {
                NewClient = new Client(parts[0], parts[1], parts[2], parts[3], parts[4], Int32.Parse(parts[5]), DateTime.Parse(parts[6]));
            }

            return NewClient;
        }
    }
}
