using System.ComponentModel.DataAnnotations;

namespace ManifestationApi.Models;

public class ManifestationUser
{
    [Key] //make UserId the key 
    public int UserId { get; set; }
    [Required]
    public string? Forename { get; set; }

    [Required]
    public string? Surname { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "E-mail is not valid")] //validates email is in valid email format
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$",
        ErrorMessage = "Password must be 8-20 characters long, include at least one number, and one special character.")]
    public string? Password { get; set; }
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
}


public class ManifestationUserNameUpdate
{
    [Required]
    public string? Forename { get; set; }

    [Required]
    public string? Surname { get; set; }
}
