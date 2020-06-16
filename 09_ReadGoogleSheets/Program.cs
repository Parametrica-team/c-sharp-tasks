using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;

namespace SheetsQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Google Sheets API .NET Quickstart";

        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream(@"secrets\credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1kL0cEVkh0dTVY6CfCCJij30Sh_D9XhylaOVFQxsZJXk";
            String range = "Типовой этаж!2:10";
            SpreadsheetsResource.GetRequest request = service.Spreadsheets.Get(spreadsheetId);
            request.Ranges = range;
            request.IncludeGridData = true;

            var response = request.Execute();
            var sheetData = response.Sheets[0].Data[0];

            SaveHblockFlats(sheetData.RowData, Environment.CurrentDirectory);
        }

        private static void SaveHblockFlats(IList<RowData> rows, string currentDirectory)
        {
            var headers = rows[0].Values;
            foreach (var row in rows.Skip(1))
            {
                var flats = new List<string>();
                var hblockcode = row.Values[1].FormattedValue+".txt";
                for (int i = 5; i < row.Values.Count; i++)
                {
                    var cell = row.Values[i];
                    var bgColor = cell.EffectiveFormat.BackgroundColor;
                    var bgColorNumber = (bgColor.Blue + bgColor.Red + bgColor.Green) / 3.0;
                    if (bgColorNumber != null && bgColorNumber != 1) //не белый
                        flats.Add(headers[i].FormattedValue);

                }
                var path = Path.Combine(currentDirectory, hblockcode);
                File.WriteAllLines(path, flats);
            }

            
        }
    }
}