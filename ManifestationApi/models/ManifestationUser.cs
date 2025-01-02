using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ManifestationApi.Models;

public class ManifestationUser
{

    public Guid Id { get; set; }
    [Required]
    public string? Forename { get; set; }

    [Required]
    public string? Surname { get; set; }

    [Required]
    public string? MemorableQuestion { get; set; }

    [Required]
    public string? MemorableAnswer { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "E-mail is not valid")] //validates email is in valid email format
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$",
        ErrorMessage = "Password must be 8-20 characters long, include at least one number, and one special character.")]
    public string? Password { get; set; }

    [JsonIgnore]
    public ICollection<ManifestationReminder> ManifestationReminders { get; } = new List<ManifestationReminder>();
}

public class CreateManifestationUser
{
    [Required]
    public string? Forename { get; set; }

    [Required]
    public string? Surname { get; set; }
    [Required]
    public string? MemorableQuestion { get; set; }

    [Required]
    public string? MemorableAnswer { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "E-mail is not valid")] //validates email is in valid email format
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$",
        ErrorMessage = "Password must be 8-20 characters long, include at least one number, and one special character.")]
    public string? Password { get; set; }

    [JsonIgnore]
    public ICollection<ManifestationReminder> ManifestationReminders { get; } = new List<ManifestationReminder>();
}
public class ManifestationUserEmailUpdate
{

    [Required]
    [EmailAddress(ErrorMessage = "E-mail is not valid")] //validates email is in valid email format
    public string? Email { get; set; }
}

public class ManifestationUserPasswordUpdate
{

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$",
        ErrorMessage = "Password must be 8-20 characters long, include at least one number, and one special character.")]
    public string? Password { get; set; }
    public string? MemorableAnswer { get; set; }
}


public class ManifestationUserNameUpdate
{
    [Required]
    public string? Forename { get; set; }

    [Required]
    public string? Surname { get; set; }
}
