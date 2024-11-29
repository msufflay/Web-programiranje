using ItemsAPI.Models;

using Microsoft.AspNetCore.Mvc;
using System;

namespace ItemsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static List<Item> Items = new List<Item>() {
            new Item { Id = Guid.NewGuid().ToString(), Name = "Laptop", Description = "Za skolu" },
            new Item { Id = Guid.NewGuid().ToString(), Name = "Stol", Description = "Zeleni" }
            };

        [HttpGet(Name = "GetItems")]
        public ActionResult <List<Item>> GetItems()
        {
            var authHeader = Request.Headers.Authorization;
            if (authHeader.ToString() != "Bearer Mirjana")
            {
                return Unauthorized("Niste autorizirani");
            }
            return Ok(Items);
        }
        
        [HttpGet("{id}")]
        public ActionResult GetOneItems(string id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }
        
        [HttpPost(Name = "CreateItem")]
        public ActionResult<Item> CreateItem(ItemCreateRequest itemcreaterequest)
        {
            Item item = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Name = itemcreaterequest.Name,
                Description = itemcreaterequest.Description
            };

            Items.Add(item);

            return CreatedAtAction(nameof(GetOneItems), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, ItemUpdateRequest itemupdaterequest)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == id);

            if (existingItem == null)
            {
                return NotFound(new { message = $"Element s istim Id-jem {id} veæ postoji." });
            }

            if (Items.Any(i => i.Name == itemupdaterequest.Name && i.Id != id))
            {
                return Conflict(new { message = "Element s istim imenom veæ postoji." });
            }

            existingItem.Name = itemupdaterequest.Name;
            existingItem.Description = itemupdaterequest.Description;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            Items.Remove(item);

            return NoContent();
        }
    }
}