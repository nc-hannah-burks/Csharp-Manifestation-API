using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;  //sets further requirements/rules for inputs 
namespace ManifestationApi.Models;

public class ManifestationReminder
{

    public Guid Id { get; set; }
    [Required]

    [RegularExpression(@"^([01]?[0-9]|2[0-3]):([0-5][0-9])$
")]
    public string? ReminderTime { get; set; }

    [Required]
    public Guid? UserId { get; set; } // Required foreign key property
    [JsonIgnore]
    public ManifestationUser ManifestationUser { get; set; } = null!; // Required reference navigation to principal


}

public class CreateManifestationReminder
{

    [RegularExpression(@"^([01]?[0-9]|2[0-3]):([0-5][0-9])$
")]
    public string? ReminderTime { get; set; }

    [Required]
    public string? UserId { get; set; } // Required foreign key property

}
// public class ManifestationUserEmailUpdate
// {

//     [Required]
//     [EmailAddress(ErrorMessage = "E-mail is not valid")] //validates email is in valid email format
//     public string? Email { get; set; }
// }
