﻿using EASendMail;
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
        private bool SectionFourIsHidden { get; set; }
        private bool SectionFiveIsHidden { get; set; }
        public List<RentalData> RentalContracts { get; set; }
        private List<RentalData> SearchContracts { get; set; }
        public RentalData RentalContract { get; set; }
        public string ContractNumber { get; set; }
        public string RenterName { get; set; }
        public string RenterAddress {  get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber {  get; set; }
        public string RenterEmail { get; set; }
        public string Notes { get; set; }
        public double Rate {  get; set; }   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? IsOwnerVerified { get; set; }
        public bool? IsRenterVerified {  get; set; }
        public bool ContractStatus { get; set; }
        private string feedback {  get; set; }
        private string SearchCriteria { get; set; } = "";
        //private string RenterEmail { get; set; } = "";
        private string EmailBody { get; set; } = "";


        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

        protected override Task OnInitializedAsync()
        {

            SectionTwoIsHidden = true;
            SectionThreeIsHidden = true;
            SectionFourIsHidden = true;
            SectionFiveIsHidden = true;

            ContractNumber = "024-0034";
            RenterName = "Chi-Chi Peralta";
            RenterAddress = "6914 177 Street NW";
            PostalCode = "T5N 1N3";
            PhoneNumber = "(306) 225-4589";
            RenterEmail = "chi.chi.peralta@example.com";
            Notes = "Renter verification needed. Renter contacted 05-Apr-24";    

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
                        if (contract.ContractStatus.ToLower() == "active")
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
                        if (contract.ContractStatus.ToLower() == "pending")
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
                        if (contract.ContractStatus.ToLower() == "expired")
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
            SectionTwoIsHidden = true;
            SectionThreeIsHidden = false;
            SectionFourIsHidden = true;
            SectionFiveIsHidden = true;

            if (!string.IsNullOrWhiteSpace(contractNumber))
            {
                RentalContract = SearchContracts.FirstOrDefault(c => c.ContractNumber == contractNumber);
            }
        }
        private void HandleEditContract(int? contractId)
        {
            SectionTwoIsHidden = true;
            SectionThreeIsHidden = true;
            SectionFourIsHidden = false;

            SectionFiveIsHidden = true;

            if (contractId != 0)
            {
                RentalContract = SearchContracts.FirstOrDefault(c => c.ContractId == contractId);                
            }
        }

        private void HandleSaveChanges()
        {
            feedback = "Changes saved succesfully";
            RentalContract = null;
            SectionThreeIsHidden = false;
        }
      
        private void HandleCreateReport()
        {
            feedback = "Report Created Succesfully";

        }

        private void HandleAddNewContract()
        {
            SectionTwoIsHidden = false;
            SectionFiveIsHidden = true;
            SectionFourIsHidden = true;
            SectionThreeIsHidden = true;
        }
        private void HandleAddNewContractExistingRenter(int? contractId)
        {
            SectionFiveIsHidden = false;
            SectionFourIsHidden= true;
            SectionThreeIsHidden = true;
            SectionTwoIsHidden= true;

            if (contractId != 0)
            {
                RentalContract = SearchContracts.FirstOrDefault(c => c.ContractId == contractId);
            }
        }

        private void HandleSaveNewContract()
        {
            ClearFields();
            RentalContract = null;
            ContractNumber = "024-0035";
            feedback = "Contract Saved";
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
            PostalCode = "";
            RenterEmail = "";
            PhoneNumber = "";
            Notes = "";
            Rate = 0;   
        }
        private async Task SendEmail(string renterEmail, string emailBody)
        {

            feedback = "Message sent succesfully";
            //try
            //{
            //    SmtpMail eMail = new SmtpMail("TryIt");
            //    eMail.From = "barrera_ml@hotmail.com";
            //    eMail.To = renterEmail;
            //    eMail.Subject = "Contract Expiration Reminder";
            //    eMail.TextBody = emailBody;

            //    SmtpServer eServer = new SmtpServer("smtp.office365.com");
            //    eServer.User = "sacred.seeds@outlook.com:smtp.office365.com:587";
            //    eServer.Password = "anap1525";
            //    eServer.Port = 587;

            //    eServer.ConnectType = SmtpConnectType.ConnectTryTLS;

            //    EASendMail.SmtpClient eSmtp = new EASendMail.SmtpClient();
            //    eSmtp.SendMail(eServer, eMail);
            //    feedback = "Message Sent";
            //}
            //catch (Exception e)
            //{
            //    feedback = e.Message;
            //}
        }

        //private async Task SendEmail(string renterEmail, string emailBody)
        //{
        //    try
        //    {
        //        using (MailMessage emailMessage = new MailMessage())
        //        {
        //            emailMessage.From = new MailAddress("sacred.seeds@outlook.com");
        //            emailMessage.To.Add(renterEmail);
        //            emailMessage.Subject = "Contract Expiration Reminder";
        //            emailMessage.Body = $"<p>{emailBody}<p>";
        //            emailMessage.IsBodyHtml = true;

        //            using (SmtpClient smpt = new SmtpClient("smtp.live.com", 587))
        //            {
        //                smpt.Credentials = new System.Net.NetworkCredential("sacred.seeds@outlook.com", "anap1525");
        //                smpt.EnableSsl = true;
        //                smpt.Send(emailMessage);
        //                feedback = "Message Sent";
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        feedback = e.Message;
        //    }
        //}


    }
}
