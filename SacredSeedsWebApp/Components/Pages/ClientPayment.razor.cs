using Microsoft.AspNetCore.Components;
using SacredSeedsSystem;

namespace SacredSeedsWebApp.Components.Pages
{
    public partial class ClientPayment
    {
        private List<Client>? Clients { get; set; }
        private List<Session> Sessions { get; set; }
        private List<Session> ClientSessions { get; set; }

        public Client Client { get; set; }
        public Session Session { get; set; }            

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

        protected override Task OnInitializedAsync()
        {
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

       
    }
}
