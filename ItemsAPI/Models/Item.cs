using Microsoft.OpenApi.MicrosoftExtensions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ItemsAPI.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}