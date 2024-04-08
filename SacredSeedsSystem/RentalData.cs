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
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RenterEmail { get; set; }
        public double? Rate { get; set; }
        public bool? IsOwnerVerified { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DaysRemaining { get; set; }
        public string? ContractStatus { get; set; }
        public bool? IsRenterVerified { get; set; }
        public string? Notes { get; set; }

        //Constructor with all parameters
        public RentalData(int contractId, string contractNumber, string renterName, string renterAddress, string city, string province, string postalCode, string phoneNumber, string renterEmail, double rate, bool isOwnerVerified, DateTime startDate, DateTime endDate, int daysRemaining, string contractStatus, bool isRenterVerified, string notes)
        {
            ContractId = contractId;
            ContractNumber = contractNumber;
            RenterName = renterName;
            RenterAddress = renterAddress;
            City = city;
            Province = province;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            RenterEmail = renterEmail;
            Rate = rate;
            IsOwnerVerified = isOwnerVerified;
            StartDate = startDate;
            EndDate = endDate;
            DaysRemaining = daysRemaining;
            ContractStatus = contractStatus;
            IsRenterVerified = isRenterVerified;
            Notes = notes;
        }

        public RentalData(int contractId, string contractNumber, string renterName, string renterAddress, string city, string province, string postalCode, string phoneNumber, string email, double rate, bool isOwnerVerified, int daysRemaining, string contractStatus, bool isRenterVerified, string notes)
        {
            ContractId = contractId;
            ContractNumber = contractNumber;
            RenterName = renterName;
            RenterAddress = renterAddress;
            City = city;
            Province = province;
            PostalCode= postalCode;
            PhoneNumber = phoneNumber;
            RenterEmail= email;
            Rate = rate;
            IsOwnerVerified = isOwnerVerified;            
            DaysRemaining = daysRemaining;
            ContractStatus = contractStatus;
            IsRenterVerified = isRenterVerified;
            Notes = notes;
        }



        public override string ToString()
        {
            return $"{ContractId}, {ContractNumber}, {RenterName}, {RenterAddress}, {City}, {Province}, {PostalCode}, {PhoneNumber}, {RenterEmail}, {Rate?.ToString("0.00")}, {IsOwnerVerified}, {StartDate?.ToString("MMM dd yyyy")}, {EndDate?.ToString("MMM dd yyyy")}, {DaysRemaining}, {ContractStatus}, {IsRenterVerified}, {Notes}";
        }

        public static RentalData Parse(String text)
        {
            string[] parts = text.Split(",");
            RentalData newRentalData = null;

            if (parts.Length == 17)
            {
                newRentalData = new RentalData(Int32.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], double.Parse(parts[9]), bool.Parse(parts[10]), DateTime.Parse(parts[11]), DateTime.Parse(parts[12]), Int32.Parse(parts[13]), parts[14], bool.Parse(parts[15]), parts[16]);
            }

            else if (parts.Length < 17)
            {
                newRentalData = new RentalData(Int32.Parse(parts[0]), parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7], parts[8], double.Parse(parts[9]), bool.Parse(parts[10]), Int32.Parse(parts[11]), parts[12], bool.Parse(parts[13]), parts[14]);
            }

            return newRentalData;
        }



    }
}
