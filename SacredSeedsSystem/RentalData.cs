using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacredSeedsSystem
{
    public class RentalData
    {      
        public int? ContractId { get; set; }
        public string? ContractNumber { get; set; }
        public string RenterName { get; set; }
        public string? RenterAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public double? Rate { get; set; }
        public bool? IsOwnerVerified { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DaysRemaining { get; set; }
        public string? IsActive { get; set; }
        public bool? IsRenterVerified { get; set; }
        public string? Notes { get; set; }

        //Constructor with all parameters
        public RentalData(int contractId, string contractNumber, string renterName, string renterAddress, string phoneNumber, double rate, bool isOwnerVerified, DateTime startDate, DateTime endDate, int daysRemaining, string isActive, bool isRenterVerified, string notes)
        {
            ContractId = contractId;
            ContractNumber = contractNumber;
            RenterName = renterName;
            RenterAddress = renterAddress;
            PhoneNumber = phoneNumber;
            Rate = rate;
            IsOwnerVerified = isOwnerVerified;
            StartDate = startDate;
            EndDate = endDate;
            DaysRemaining = daysRemaining;
            IsActive = isActive;
            IsRenterVerified = isRenterVerified;
            Notes = notes;
        }

        public RentalData(int contractId, string contractNumber, string renterName, string renterAddress, string phoneNumber, double rate, bool isOwnerVerified, int daysRemaining, string isActive, bool isRenterVerified, string notes)
        {
            ContractId = contractId;
            ContractNumber = contractNumber;
            RenterName = renterName;
            RenterAddress = renterAddress;
            PhoneNumber = phoneNumber;
            Rate = rate;
            IsOwnerVerified = isOwnerVerified;            
            DaysRemaining = daysRemaining;
            IsActive = isActive;
            IsRenterVerified = isRenterVerified;
            Notes = notes;
        }



        public override string ToString()
        {
            return $"{ContractId}, {ContractNumber}, {RenterName}, {RenterAddress}, {PhoneNumber}, {Rate?.ToString("0.00")}, {IsOwnerVerified}, {StartDate?.ToString("MMM dd yyyy")}, {EndDate?.ToString("MMM dd yyyy")}, {DaysRemaining}, {IsRenterVerified}, {IsActive}, {Notes}";
        }

        public static RentalData Parse(String text)
        {
            string[] parts = text.Split(",");
            RentalData newRentalData = null;

            if (parts.Length == 13)
            {
                newRentalData = new RentalData(Int32.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], double.Parse(parts[5]), bool.Parse(parts[6]), DateTime.Parse(parts[7]), DateTime.Parse(parts[8]), Int32.Parse(parts[9]), parts[10], bool.Parse(parts[11]), parts[12]);
            }

            else if (parts.Length < 13)
            {
                newRentalData = new RentalData(Int32.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], double.Parse(parts[5]), bool.Parse(parts[6]), Int32.Parse(parts[9]), parts[10], bool.Parse(parts[11]), parts[12]);
            }

            return newRentalData;
        }



    }
}
