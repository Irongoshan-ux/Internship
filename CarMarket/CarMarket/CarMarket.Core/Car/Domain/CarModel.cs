using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Core.Car.Domain
{
    public class CarModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public CarType CarType { get; set; }
        public ICollection<ImageModel> CarImages { get; set; }
        [Required]
        public UserModel Owner { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(25, 300000, ErrorMessage = "Unreliable price")]
        public int Price { get; set; }
    }
}