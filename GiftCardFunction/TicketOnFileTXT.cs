using System;
using System.IO;

namespace GiftCardFunction
{
    public class TicketOnFileTXT
    {
        public static void SaveTicket(string Path, string Ticket)
        {
            using (StreamWriter sw = new StreamWriter(Path, true)) 
            {
                sw.WriteLine(Ticket);
                sw.WriteLine("----------");
                Console.WriteLine("Saved : " + Ticket);
            }
        }


    }
}
