using System.ComponentModel.DataAnnotations;

namespace NIMAP.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }


    }

    public class DropDownSource
    {
        public string Value { get; set; }
        public string Text { get; set; }

    }
}
