using System.Globalization;
using System.Xml.Linq;



namespace SacredSeedsSystem
{
    public class Client
    {
      
        public string? ClientId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactName { get; set; } 
        public string? PhoneNumber { get; set; }
        public DateTime AppointmentDate {get; set; }

        public Client(string clientId, string name, string address, string contactName, string phoneNumber, DateTime appointmentDate)
        {
            ClientId = clientId;
            Name = name;
            Address = address;
            ContactName = contactName;
            PhoneNumber = phoneNumber;
            AppointmentDate = appointmentDate;
        }    
        
        public override string ToString()
        {

            return $"{ClientId},{Name}, {Address},{ContactName},{PhoneNumber}, {AppointmentDate.ToString("MMM dd yyyy")}";
        }

        public static Client Parse(String text)
        {
            string[] parts = text.Split(",");
            Client NewClient = null;

            if (parts.Length == 6)
            {
                NewClient = new Client(parts[0], parts[1], parts[2], parts[3], parts[4], DateTime.Parse(parts[5]));
            }

            return NewClient;
        }
    }
}
