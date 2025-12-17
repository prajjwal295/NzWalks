using System.ComponentModel.DataAnnotations;

namespace NzWalks.API.Model.DTO
{
    public class CreateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Code has to be a minimum of 3 charector")]
        [MaxLength(3 , ErrorMessage ="Code has to be a maximum of 3 charector")]
        public string Code { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Code has to be a maximum of 30 charector")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
