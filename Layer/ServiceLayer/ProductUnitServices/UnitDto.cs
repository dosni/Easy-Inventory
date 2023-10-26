using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ProductUnitServices
{
    public class UnitDto
    {
        public int UnitId { get; set; }
        [Required(ErrorMessage = "Unit is required")]
        public string Unit { get; set; }

    }
}
