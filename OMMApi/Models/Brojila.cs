using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMMApi.Models
{
    public class Brojila
    {
        public int Id { get; set; }
        public string BrojBrojila { get; set; }
        public string NazivProizv { get; set; }
        public double KorekcijaTemp { get; set; }
        public int GodinaBazdarenja { get; set; }
    }
}
