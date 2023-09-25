using System.ComponentModel.DataAnnotations;

namespace BlazorWallAreaCalculator.Data
{
    public class Wall
    {
        [Required]
        public int WallID { get; set; }
        [Required]
        public int RoomID { get; set; }
        [Required]
        [StringLength(50)]
        public string WallName { get; set; } = String.Empty;
        public int WallTypeID { get; set; } = 0;
        [Required]
        public string WallTypeName { get; set; } = string.Empty;
        [Required]
        public int WallLengthMax { get; set; } = 0;
        [Required]
        public int WallLengthMin { get; set; } = 0;
        [Required]
        public int WallHeightMax { get; set; } = 0;
        [Required]
        public int WallHeightMin { get; set; } = 0;
        [StringLength(50)]
        public decimal WallSqM { get; set; } = decimal.Zero;
    }
}
