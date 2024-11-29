using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMMApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace OMMApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OMMController : ControllerBase
    {
        private static List<OMM> omms = new List<OMM>() 
        {
            new OMM { Id = 1, Adresa = "Prva Ulica 1", NazivKupca = "Pero Peric", BrojBrojila = 123456, DatumOcitanja = new DateTime(2023, 10, 23), Stanje = 125 },
            new OMM { Id = 2, Adresa = "Prva Ulica 2", NazivKupca = "Ivo Ivic", BrojBrojila = 1111111, DatumOcitanja = new DateTime(2023, 10, 23), Stanje = 1250 },
            new OMM { Id = 3, Adresa = "Druga Ulica 1", NazivKupca = "Mato Matic", BrojBrojila = 222222, DatumOcitanja = new DateTime(2023, 10, 23), Stanje = 0 },
        };

        private static int nextId = omms.Count > 0 ? omms.Max(i => i.Id) + 1 : 1;

        public int[] niz = [1, 2, 3, 4];

        [HttpGet("niz")]
        public ActionResult<int> GetNiz()
        {
            return Ok(niz[0]);
        }

        [HttpGet(Name = "GetOMMs")]
        public IActionResult GetOMMs()
        {
            return Ok(omms);
        }   

        [HttpGet("{id}")]
        public ActionResult GetOneOMM(int id)
        {
            var omm = omms.FirstOrDefault(i => i.Id == id);
            if (omm == null)
                return NotFound(new { message = $"Element s ID-jem {id} nije pronaðen." }); 

            return Ok(omm);
        }
    
        [HttpPost(Name = "CreateOMM")]
        public ActionResult<OMM> CreateOMM(OMM omm)
        {
            if (omms.Any(i => i.Id == omm.Id))
            {
                return Conflict(new { message = "Element s istim imenom veæ postoji" });
            }

            omms.Add(omm);
            return CreatedAtAction(nameof(GetOMMs), new { Id = omm.Id }, omm.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OMM omm)
        {
            var existingOMM = omms.FirstOrDefault(i => i.Id == id);

            if (existingOMM == null)
            {
                return NotFound(new { message = $"Element s ID-jem {id} nije pronaðen." });
            }
            
            if (omms.Any(i => i.Adresa == omm.Adresa && i.Id != id))
            {
                return Conflict(new { message = "Element s istom adresom veæ postoji." });
            }

            existingOMM.Adresa = omm.Adresa;
            existingOMM.NazivKupca = omm.NazivKupca;
            existingOMM.BrojBrojila = omm.BrojBrojila;
            existingOMM.DatumOcitanja = omm.DatumOcitanja;
            existingOMM.Stanje = omm.Stanje;
       
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var omm = omms.FirstOrDefault(i => i.Id == id);

            if (omm == null)
            {
                return NotFound(new { message = $"Element s ID-jem {id} nije pronaðen." });
            }

            omms.Remove(omm);

            return NoContent();
        }
    }
}