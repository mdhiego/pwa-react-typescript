using System.ComponentModel.DataAnnotations;

namespace BabySounds.Contracts.Requests;

public sealed record LoginRequest
{
    /// <summary>
    /// The username of the user.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string UserName { get; set; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
