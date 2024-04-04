using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacredSeedsSystem
{
    internal class RentalData
    {


        public string RenterName { get; set; }
        public string RenterAddress { get; set; }
        public string PhoneNumber { get; set; }
        public double Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }

        public RentalData (string renterName, string renterAddress, string phoneNumber, double rate, DateTime startDate, DateTime dateDate, bool isVerified, bool isActive)
        {
            RenterName = renterName;
            RenterAddress = renterAddress;
            PhoneNumber = phoneNumber;
            Rate = rate;
            StartDate = startDate;
            EndDate = dateDate;
            IsVerified = isVerified;
            IsActive = isActive;
        }

        public override string ToString()
        {
            return $"{RenterName}, {RenterAddress}, {PhoneNumber}, {Rate.ToString("0.00")}, {StartDate.ToString("MMM dd yyyy")}, {EndDate.ToString("MMM dd yyyy")}, {IsVerified}, {IsActive}";
        }

        public static RentalData Parse(String text)
        {
            string[] parts = text.Split(",");
            RentalData newRentalData = null;

            if (parts.Length == 8)
            {
                newRentalData = new RentalData(parts[0], parts[1], parts[2], double.Parse(parts[3]), DateTime.Parse(parts[4]), DateTime.Parse(parts[5]), bool.Parse(parts[6]), bool.Parse(parts[7]));
            }

            return newRentalData;
        }

    }
}
