using Microsoft.AspNetCore.Components;
using SacredSeedsSystem;

namespace SacredSeedsWebApp.Components.Pages
{
    public partial class RentalContractManagement
    {
        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;
        private bool IsHidden { get; set; }
        public List<RentalData> RentalContracts { get; set; }
        public string RenterName { get; set; }
        public string RenterAddress {  get; set; }
        public string PhoneNumber {  get; set; }
        public double Rate {  get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }

        protected override Task OnInitializedAsync()
        {
            IsHidden = true;

            return base.OnInitializedAsync();
        }

        public void ReadFile()
        {
            RentalContracts = new();

            string csvFilePath = $@"{WebHostEnvironment.ContentRootPath}\Data\ClientsList.csv";

            using (StreamReader reader = new StreamReader(csvFilePath))
            {

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
