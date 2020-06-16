using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

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
            String range = "Типовой этаж!A2:AA10";
            SpreadsheetsResource.GetRequest request = service.Spreadsheets.Get(spreadsheetId);
            request.Ranges = range;
            request.IncludeGridData = true;

            var response = request.Execute();
            var sheetData = response.Sheets[0].Data[0];

            SaveHblockFlats(sheetData.RowData, Environment.CurrentDirectory);
        }

        private static void SaveHblockFlats(IList<RowData> rowData, string currentDirectory)
        {
            throw new NotImplementedException();
        }
    }
}