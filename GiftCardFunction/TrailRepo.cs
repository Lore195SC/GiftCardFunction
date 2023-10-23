using System;
using System.Collections.Generic;
using System.Linq;

namespace GiftCardFunction
{
    public static class TrailRepo
    {
        private static List<Trail> _trails = new()
        {
            new Trail(3,
                "Sato Code Zurich",
                "Albert's Door"),

           new Trail(6,
                "Sato Code Locarno",
                "The Sasso Society"),
           new Trail(7,
                "Sato Code Zurich",
                "Albert's Key"),
          new Trail(8,
                "Sato Code Lugano",
                "The Duke of Marengo"),

           new Trail(9,
                "Sato Code Bellinzona",
                "The Iguanas"),
           new Trail(10,
                "Sato Code Zug",
                "The Sasso Society"),
           new Trail(12,
                "Sato Code Como",
                "The Origin"),
           new Trail(13,
                "Sato Code Varese",
                "The Angelus Key"),
           new Trail(14,
                "Sato Code Bremgarten",
                "The Angelus Key"),
           new Trail(15,
                "Sato Code Lecco",
                "Marella's Job"),
           new Trail(16,
                "Sato Code Bergamo",
                "Colleoni's Shield"),
           new Trail(17,
                "Sato Code Brescia",
                "Arnaldo's Brothers"),
           new Trail(18,
                "Sato Code Pavia",
                "Martellus"),
           new Trail(19,
                "Sato Code Padova",
                "Deep Desire"),
           new Trail(20,
                "Sato Code Padova",
                "Memmo's Legacy'"),
           new Trail(21,
                "Sato Code Rapperswill",
                "Amnesia"),
           new Trail(22,
                "Sato Code Milano",
                "FR13ND"),
           new Trail(23,
                "Sato Code Iseo",
                "Emilia"),
           new Trail(24,
                "Sato Code Ascona",
                "Enzo's box"),
           new Trail(25,
               "Sato Code Monza",
               "FrogLover")



         };
        public static Trail GetTrail(int id)
        {
            var trail = _trails.FirstOrDefault(t => t.Id == id);

            if (trail == null)
            {
                throw new Exception($"Trail with Id {id} is not in TrailRepository");
            }

            return trail;
        }
    }
}
