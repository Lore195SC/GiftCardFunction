using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Diagnostics;
using FunctionApp;

namespace GiftCardFunction
{
    public static class GiftcardManager
    {
        [FunctionName("GiftcardManager")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger _log)
        {
            _log.LogInformation("C# HTTP trigger function processed a request.");


            DateTime Today = DateTime.Now.Date.AddYears(1);
            string setDate = Today.ToString("dd-MM-yyyy");
            int trail;
            string email;
            try
            {

                string ticket = GetParameter(req, "ticket");
                string numberOfPlayer = GetParameter(req, "player");
                string lan = GetParameter(req, "lan");
                trail = CastString(req.Query["trail"]);
                email = GetParameter(req, "email");

                var trailData = TrailRepo.GetTrail(trail);

                ImageMaker.DrawText(20, $"{trailData.NameCity.ToUpper()}", $"{trailData.NameTrail.ToUpper()}",
                    ticket.ToUpper(), swtichMessage(lan, numberOfPlayer, setDate), lan);
                new OkObjectResult("Image edit");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Failed to read Parameters. " + ex.Message);
            }
            try
            {
                Sender imageMaker = new Sender();
                bool result = imageMaker.EmailSender(email, "C:\\Users\\loren\\Downloads\\NewTest2.jpg");

                return new OkObjectResult("Email Send");

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Failed to send email" + ex.Message);
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

        private static string swtichMessage(string lan, string numberOfPlayer, string setDate)
        {
            switch (lan)
            {
                case "it":
                    return $"valido per {numberOfPlayer} persone \n fino al {setDate}";
                case "de":
                    return $"Gültig für {numberOfPlayer} Personen\n bis zum {setDate}";
                case "en":
                    return $"Valid for {numberOfPlayer} players\n until {setDate}";
            }

            var exceptionMsg = $"There is not Ticket Template for Language {lan}";
            throw new Exception(exceptionMsg);
        }
    }
}
