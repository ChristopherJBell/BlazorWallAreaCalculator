using System.ComponentModel.DataAnnotations;

namespace BlazorWallAreaCalculator.Data
{
    public class Deduction
    {
        [Required]
        public int DeductionID { get; set; }
        [Required]
        public int WallID { get; set; }
        [Required]
        [StringLength(50)]
        public string DeductionName { get; set; } = String.Empty;
        [Required]
        public int DeductionWidth { get; set; } = 0;
        [Required]
        public int DeductionHeight { get; set; } = 0;
        public decimal SqM { get; set; } = decimal.Zero;
    }
}