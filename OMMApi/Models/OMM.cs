using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMMApi.Models
{
    public class OMM
    {
        public int Id {get; set; }
        public string Adresa { get; set; }
        public string NazivKupca { get; set; } 
        public int BrojBrojila { get; set; }  
        public DateTime DatumOcitanja { get; set; } 
        public int Stanje { get; set; } 
    }
}