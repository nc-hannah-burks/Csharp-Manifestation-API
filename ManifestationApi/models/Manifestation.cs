using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;  //sets further requirements/rules for inputs 
namespace ManifestationApi.Models;

public class Manifestation
{

    public Guid Id { get; set; }
    [Required]
    public string? Affirmation { get; set; }
    public string? ManifestationImg { get; set; }

    [Required]
    public Guid? UserId { get; set; } // Required foreign key property
    [JsonIgnore]
    public ManifestationUser ManifestationUser { get; set; } = null!; // Required reference navigation to principal


}

public class CreateManifestation
{
    public string? Affirmation { get; set; }
    public string? ManifestationImg { get; set; }

    [Required]
    public string? UserId { get; set; } // Required foreign key property

}


public class ManifestationImgUpdate
{
    [Required]
    public string? ManifestationImg { get; set; }
}
