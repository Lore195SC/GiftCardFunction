using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftCardFunction
{
    public class Trail
    {
        public Trail(int id, string nameTrail , string nameCity) 
        { 
            Id = id;
            NameCity = nameCity;
            NameTrail = nameTrail;
        }

        public int Id { get; set; }
        public string NameCity { get; set; }
        public string NameTrail { get; set; }

    }
}
