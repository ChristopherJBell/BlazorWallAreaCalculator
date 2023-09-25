using System.ComponentModel.DataAnnotations;

namespace BlazorWallAreaCalculator.Data
{
    public class Room
    {
        [Required]
        public int RoomID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        [StringLength(50)]
        public string RoomName { get; set; } = String.Empty;
    }
}
