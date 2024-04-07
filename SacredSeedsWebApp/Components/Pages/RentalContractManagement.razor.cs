using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using SacredSeedsSystem;

namespace SacredSeedsWebApp.Components.Pages
{
    public partial class RentalContractManagement
    {        
        private bool IsHidden { get; set; }
        public List<RentalData> RentalContracts { get; set; }
        public RentalData RentalContract { get; set; }
        public string RenterName { get; set; }
        public string RenterAddress {  get; set; }
        public string PhoneNumber {  get; set; }
        public double Rate {  get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }

        private string feedback {  get; set; }
        

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

        protected override Task OnInitializedAsync()
        {
            
            IsHidden = true;
            ReadContractsFile();
            return base.OnInitializedAsync();
        }

        private void ReadContractsFile()
        {
            RentalContracts = new List<RentalData>();

            string csvFilePath = $@"{WebHostEnvironment.ContentRootPath}\Data\RentalContracts.csv";

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                try
                {
                    if (reader.Peek() == -1)
                    {
                        feedback = "There are no records in the file to display";
                    }
                    string? currentLine;

                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        try
                        {
                            RentalContracts.Add(RentalData.Parse(currentLine));
                        }
                        catch (FormatException e)
                        {
                            feedback = e.Message;
                        }
                    }

                }
                catch (IOException e)
                {
                    feedback = e.Message;
                }
            }

        }

        private void OnHandleAddNewContract()
        {
            IsHidden = false;
        }

        private void OnHandleCancelNewContract()
        {
            ClearFields();
            IsHidden = true;
        }

        private void ClearFields()
        {
            RenterName = "";
            RenterAddress = "";
            PhoneNumber = "";
            Rate = 0;   
        }


    }
}
