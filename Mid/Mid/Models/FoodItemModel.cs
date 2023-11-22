using System.ComponentModel.DataAnnotations;

namespace Mid.Models
{
    public class FoodItemModel
    {

        public int Id { get; set; }


        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public string PreserveTime { get; set; }
    }

}