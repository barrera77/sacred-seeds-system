using Microsoft.AspNetCore.Components;
using SacredSeedsSystem;

namespace SacredSeedsWebApp.Components.Pages
{
    public partial class ClientPayment
    {
        private List<Client>? Clients { get; set; }
        private List<Client>? SearchClients { get; set; }
        private List<Session> Sessions { get; set; }
        private List<Session> ClientSessions { get; set; }
        private List<string> Errors { get; set; }

        public Client Client { get; set; } 
        public Session Session { get; set; }   
        
        private string SearchCriteria { get; set; } = "";
        private string SearchByName { get; set; } = "";
        private DateTime? SearchByDate { get; set; } = null;
        private bool PurchasePackage { get; set; } = false;
        private bool HasReferrals { get; set; } = false;


        private string feedback { get; set; } = "";
        private string AlertClass { get; set; }


        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

        protected override Task OnInitializedAsync()
        {
            Errors = new();
            ReadClientFile();
            ReadSessionFile();

            return base.OnInitializedAsync();
        }

        public void ReadSessionFile()
        {
            Sessions = new();

            string csvFilePath = $@"{WebHostEnvironment.ContentRootPath}\Data\Session.csv";

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                try
                {
                    if (reader.Peek() == -1)
                    {
                        throw new IOException("There are no records in the file to display");
                    }
                    //reader.ReadLine();
                    string? currentLine;

                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        try
                        {
                            Sessions.Add(Session.Parse(currentLine));
                        }
                        catch (FormatException e)
                        {
                            
                        }
                    }
                }
                catch (IOException e)
                {
                    
                }
                reader.Close();
            }
        }

        public void ReadClientFile()
        {
            Clients = new();

            string csvFilePath = $@"{WebHostEnvironment.ContentRootPath}\Data\ClientsList.csv";

            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                try
                {
                    if (reader.Peek() == -1)
                    {
                        throw new IOException("There are no records in the file to display");
                    }
                    //reader.ReadLine();
                    string? currentLine;

                    while ((currentLine = reader.ReadLine()) != null)
                    {
                        try
                        {
                            Clients.Add(Client.Parse(currentLine));
                        }
                        catch (FormatException e)
                        {

                        }
                    }
                }
                catch (IOException e)
                {

                }
                reader.Close();
            }
        }
        private void HandleSelectedClient(string clientId)
        {
            ClientSessions = new();

            if (!string.IsNullOrWhiteSpace(clientId))
            {
                Client = Clients.FirstOrDefault(c => c.ClientId == clientId);
                
                // Initialize the ClientSessions list if it's null
                if (ClientSessions == null)
                {
                    ClientSessions = new List<Session>();
                }
                else
                {
                    // Clear the list if it's not null
                    ClientSessions.Clear();
                }

                foreach (var session in Sessions)
                {
                    if (session.ClientId == clientId)
                    {
                        ClientSessions.Add(session);
                    }
                }               
            }
        }

        private void OnHandleSearchCriteria()
        {
            Errors.Clear();
            feedback = "";
            if (string.IsNullOrWhiteSpace(SearchCriteria))
            {
                feedback = "Error: Please select a search Criteria and provide a value";
                Errors.Add(feedback);
            }
            else if(SearchCriteria == "date" && !DateTime.TryParse(SearchByDate.ToString(), out _))
            {
                feedback = "Error: Please provide a session date to search";
                Errors.Add(feedback);
            }
            else if (SearchCriteria == "name" && string.IsNullOrWhiteSpace(SearchByName))
            {
                feedback = "Error: Please provide a client name to search";
                Errors.Add(feedback);
            }
            else
            {
                OnHandleSearch();
            }
        }

        private void OnHandleSearch()
        {
            ReadClientFile();

            if (SearchCriteria == "name")
            {
                if(!string.IsNullOrWhiteSpace(SearchByName))
                {
                    // Initialize the ClientSessions list if it's null
                    if (SearchClients == null)
                    {
                        SearchClients = new List<Client>();
                    }
                    else
                    {
                        // Clear the list if it's not null
                        SearchClients.Clear();
                    }
                    foreach (var client in Clients)
                    {
                        if (client.Name.Contains(SearchByName, StringComparison.OrdinalIgnoreCase))
                        {
                            SearchClients.Add(client);
                        }
                    }

                    if (SearchClients.Count() == 0)
                    {
                        AlertClass = "alert alert-danger";
                    }                   
                }

            }
        }
    }
}
