using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using SacredSeedsSystem;
using System.Diagnostics.Contracts;
using System.Net.Mail;

namespace SacredSeedsWebApp.Components.Pages
{
    public partial class RentalContractManagement
    {        
        private bool SectionTwoIsHidden { get; set; }
        private bool SectionThreeIsHidden { get; set; }
        public List<RentalData> RentalContracts { get; set; }
        private List<RentalData> SearchContracts { get; set; }
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
        private string SearchCriteria { get; set; } = "";
        private string RenterEmail { get; set; } = "";
        private string EmailBody { get; set; } = "";



        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

        protected override Task OnInitializedAsync()
        {

            SectionTwoIsHidden = true;
            SectionThreeIsHidden = true;
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

        private async Task HandleSearch(ChangeEventArgs e)
        {
            ReadContractsFile();

            SearchCriteria = Convert.ToString(e.Value);

            switch (SearchCriteria)
            {
                case "all":
                    if (SearchContracts == null)
                    {
                        SearchContracts = new();
                    }
                    else
                    {
                        SearchContracts.Clear();
                    }

                    foreach (var contract in RentalContracts)
                    {
                        SearchContracts.Add(contract);                        
                    }
                    await InvokeAsync(StateHasChanged);
                    break;

                case "active":
                    if (SearchContracts == null)
                    {
                        SearchContracts = new();
                    }
                    else
                    {
                        SearchContracts.Clear();
                    }
                    foreach (var contract in RentalContracts)
                    {
                        if (contract.IsActive.ToLower() == "active")
                        {
                            SearchContracts.Add(contract);
                        }
                    }
                    await InvokeAsync(StateHasChanged);
                    break;
                case "pending":
                    if (SearchContracts == null)
                    {
                        SearchContracts = new();
                    }
                    else
                    {
                        SearchContracts.Clear();
                    }
                    foreach (var contract in RentalContracts)
                    {
                        if (contract.IsActive.ToLower() == "pending")
                        {
                            SearchContracts.Add(contract);
                        }
                    }
                    await InvokeAsync(StateHasChanged);
                    break;
                case "expired":
                        if (SearchContracts == null)
                    {
                        SearchContracts = new();
                    }
                    else
                    {
                        SearchContracts.Clear();
                    }
                    foreach (var contract in RentalContracts)
                    {
                        if (contract.IsActive.ToLower() == "expired")
                        {
                            SearchContracts.Add(contract);
                        }
                    }
                    await InvokeAsync(StateHasChanged);
                    break;
                default:
                    SearchContracts.Clear();
                    break;
            }
        }

        private void HandleSelectedContract(string? contractNumber)
        {
            SectionThreeIsHidden = false;           

            if (!string.IsNullOrWhiteSpace(contractNumber))
            {
                RentalContract = SearchContracts.FirstOrDefault(c => c.ContractNumber == contractNumber);
            }
        }


        private void HandleEditContract()
        {

        }

        private void HandleContactRenter()
        {

        }

        private void HandleCreateReport()
        {

        }

        private void HandleAddNewContract()
        {
        }

        private bool HandleCancelAction()
        {
            ClearFields();
            return true;
        }

        private void ClearFields()
        {
            RenterName = "";
            RenterAddress = "";
            PhoneNumber = "";
            Rate = 0;   
        }

        private async Task SendEmail(string renterEmail, string emailBody)
        {
            try
            {
                using (MailMessage emailMessage = new MailMessage())
                {
                    emailMessage.From = new MailAddress("sacred.seeds@outlook.com");
                    emailMessage.To.Add(renterEmail);
                    emailMessage.Subject = "Contract Expiration Reminder";
                    emailMessage.Body = $"<p>{emailBody}<p>";
                    emailMessage.IsBodyHtml = true;

                    using (SmtpClient smpt = new SmtpClient("smtp.office365.com"))
                    {
                        smpt.Credentials = new System.Net.NetworkCredential("sacred.seeds@outlook.com", "anap1525");
                        smpt.EnableSsl = true;
                        smpt.Send(emailMessage);
                        feedback = "Mail Sent";
                    }
                }
            }
            catch (Exception e)
            {
                feedback = e.Message;
            }
        }


    }
}
