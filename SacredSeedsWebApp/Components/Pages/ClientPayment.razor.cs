using Microsoft.AspNetCore.Components;
using SacredSeedsSystem;

namespace SacredSeedsWebApp.Components.Pages
{
    public partial class ClientPayment
    {
        private List<Client>? Clients { get; set; }

        public Client Client { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

        protected override Task OnInitializedAsync()
        {
            ReadFile();

            return base.OnInitializedAsync();
        }

        public void ReadFile()
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

     
    }
}
