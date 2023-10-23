using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;

namespace GiftCardFunction
{
    public static class GiftcardManager
    {
        private static ILogger _log;

        [FunctionName("GiftcardManager")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)

        {
            _log = log;
            _log.LogInformation("C# HTTP trigger function processed a request.");

            ManageFolder();

         
            string ticket = GetParameter(req, "ticket");
            string numberOfPlayer = GetParameter(req, "player");
            string lan = GetParameter(req, "lan");
            int trail = CastString(req.Query["trail"]);

            var trailData = TrailRepo.GetTrail(trail);

            ImageMaker.DrawText(22, $"{trailData.NameCity.ToUpper()}", $"{trailData.NameTrail.ToUpper()}",
                ticket.ToUpper(), SwitchMessage(lan, numberOfPlayer), lan, ticket);

            TicketOnFileTXT.SaveTicket(FindPath(@"\Ticket.txt"), ticket);

            try
            {
                Downloder Dwn = new Downloder();
                return Dwn.DownloadImg(FindPath(@"\GiftCardFolder\" + ticket + ".jpg"));

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("file not Downloded" + ex.Message);
            }

        }

        private static void ManageFolder()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "GiftCardFolder");
            Directory.CreateDirectory(path);
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                // is file older than one hour
                if (DateTime.UtcNow.Subtract(file.CreationTime.ToUniversalTime()).TotalMinutes > 60)
                {
                    _log.LogInformation($"Trying to delete file {file.Name} from {DateTime.UtcNow}");
                    file.Delete();
                }
            }
        }

        private static int CastString(string stringParameter)
        {
            var result = int.TryParse(stringParameter, out int intResult);

            if (result)
            {
                return intResult;
            }

            var erroMsg = $"Cannot parse string {stringParameter} to an int";


            throw new Exception(erroMsg);
        }
        private static string GetParameter(HttpRequest req, string parameterName)
        {
            var result = req.Query[parameterName];

            if (string.IsNullOrEmpty(result))
            {

                throw new Exception($"Cannot find parameter: {parameterName}");
            }

            return result;
        }

        private static string SwitchMessage(string lan, string numberOfPlayer )
        {
            string vailidityDate;

            if(lan== "de" ) 
            {
                vailidityDate = DateTime.Now.Date.AddYears(1).ToString("dd.MM.yyyy");
            }
            else
            {
                vailidityDate = DateTime.Now.Date.AddYears(1).ToString("dd/MM/yyyy");
            }

            switch (lan)
            {
                case "it":
                    return $"valido per {numberOfPlayer} persone \n fino al {vailidityDate}";
                case "de":
                    return $"Gültig für {numberOfPlayer} Personen\n bis zum {vailidityDate}";
                default:
                    return $"Valid for {numberOfPlayer} players\n until {vailidityDate}";
            }

        

        }

        private static string FindPath(string LastPartPath)

        {
            string path = Directory.GetCurrentDirectory();

            return path += LastPartPath;

        }


    }
}
