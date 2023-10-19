using FunctionApp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GiftCardFunction
{
    public class ImageMaker
    {
        
        public static void DrawText(int fontSize, string TrailName, string CityName, string NumberOfPlayer, string Date, string lan, string TicketName)
        {
           
            Bitmap img = new Bitmap(ReadFileAsync(lan));
            Graphics Gimg = Graphics.FromImage(img);


            Font MainimgFont = new Font("Exo2.0-Black", fontSize, FontStyle.Bold);
            Font LowerimgFont = new Font("Exo2.0-Black", 10);
            Font CityFont = new Font("Exo2.0-Black", 15, FontStyle.Bold);
            Color CustomColor = Color.FromArgb(75, 168, 175);
            SolidBrush ColorMainWhite = new SolidBrush(Color.White);
            SolidBrush ColorCityNameBlu = new SolidBrush(CustomColor);
            StringFormat centerAlignment = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            PointF PositionTrailName = new PointF(2050, 540);
            PointF PositionTrailCity = new PointF(2050, 680);
            PointF PostitionPlayer = new PointF(2050, 1420);
            PointF PositionDate = new PointF(2050, 1600);


            Gimg.DrawString(TrailName, MainimgFont, ColorMainWhite, PositionTrailName, centerAlignment);
            Gimg.DrawString(CityName, CityFont, ColorCityNameBlu, PositionTrailCity, centerAlignment);
            Gimg.DrawString(NumberOfPlayer, MainimgFont, ColorMainWhite, PostitionPlayer, centerAlignment);
            Gimg.DrawString(Date, LowerimgFont, ColorMainWhite, PositionDate, centerAlignment);

            try
            {
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "GiftCardFolder");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string imagePath = Path.Combine(directoryPath, TicketName+".jpg");
                img.Save(imagePath, ImageFormat.Jpeg);

                Console.WriteLine("Immagine salvata con successo.");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Si è verificato un errore durante il salvataggio dell'immagine: " + ex.Message);
            }
        }
        private static string SelectFile(string language)
        {
            switch (language)
            {
                case "en":
                    return "EN.png";

                case "de":
                    return "DE.png";

                case "it":
                    return "ITA.png";
            }

            var exceptionMsg = $"There is not Ticket Template for Language {language}";
            throw new Exception(exceptionMsg);
        }

        private static string ReadFileAsync(string lan)
        {
            string path = Directory.GetCurrentDirectory();
            path += @"\GcBase\" + SelectFile(lan);
            return path;
        }
    }
}
