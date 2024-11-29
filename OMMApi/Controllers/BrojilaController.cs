using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMMApi.Models;
using System.Xml.Linq;

namespace OMMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrojilaController : ControllerBase
    {
        private static List<Brojila> listabrojila = new List<Brojila>()
        {
            new Brojila { Id = 1, BrojBrojila = "123", NazivProizv = "Elster", KorekcijaTemp = 1.25, GodinaBazdarenja = 2024 },
            new Brojila { Id = 2, BrojBrojila = "1234", NazivProizv = "IKOMA", KorekcijaTemp = 1.25, GodinaBazdarenja = 2024 },
            new Brojila { Id = 3, BrojBrojila = "12345",NazivProizv = "Itron", KorekcijaTemp = 1.25, GodinaBazdarenja = 2024 },
            new Brojila { Id = 4, BrojBrojila = "123456",NazivProizv = "IKOMA", KorekcijaTemp = 1.25, GodinaBazdarenja = 2024 },


        };

        private static int nextId = listabrojila.Count > 0 ? listabrojila.Max(i => i.Id) + 1 : 1;

        [HttpGet(Name = "GetBrojila")]
        public ActionResult<List<Brojila>> GetBrojila()
        {
            return Ok(listabrojila);
        }

        [HttpGet("{id}")]
        public ActionResult GetOneBrojila(int id)
        {
            var brojila = listabrojila.FirstOrDefault(i => i.Id == id);
            if (brojila == null)
                return NotFound(new { message = $"Element s ID-jem {id} nije pronađen."});

            return Ok(brojila);
        }

        [HttpPost(Name = "CreateBrojila")]
        public ActionResult<Brojila> CreateOMM(Brojila brojila)
        {
            if (listabrojila.Any(i => i.Id == brojila.Id))
            {
                return Conflict(new { message = "Element s istim Id-jem već postoji" });
            }

            listabrojila.Add(brojila);
            return CreatedAtAction(nameof(GetBrojila), new { Id = brojila.Id }, brojila.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Brojila brojila)
        {
            var existingBrojila= listabrojila.FirstOrDefault(i => i.Id == id);

            if (existingBrojila == null)
            {
                return NotFound(new { message = $"Element s ID-jem {id} nije pronađen." });
            }

            if (listabrojila.Any(i => i.BrojBrojila == brojila.BrojBrojila && i.Id != id))
            {
                return Conflict(new { message = "Element s istim brojem brojila već postoji." });
            }

            existingBrojila.BrojBrojila = brojila.BrojBrojila;
            existingBrojila.NazivProizv = brojila.NazivProizv;
            existingBrojila.KorekcijaTemp = brojila.KorekcijaTemp;
            existingBrojila.GodinaBazdarenja = brojila.GodinaBazdarenja;
                    
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var brojila = listabrojila.FirstOrDefault(i => i.Id == id);

            if (brojila == null)
            {
                return NotFound(new { message = $"Element s ID-jem {id} nije pronađen." });
            }

            listabrojila.Remove(brojila);

            return NoContent();
        }


    }
}
