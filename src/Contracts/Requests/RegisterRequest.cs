using System.ComponentModel.DataAnnotations;

namespace BabySounds.Contracts.Requests;

public sealed record RegisterRequest
{
    /// <summary>
    /// The first name of the user.
    /// </summary>
    [Required]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
    [DataType(DataType.Text)]
    public required string FirstName { get; set; }

    /// <summary>
    /// The username of the user.
    /// </summary>
    [Required]
    [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
    [DataType(DataType.Text)]
    public required string UserName { get; set; }

    /// <summary>
    /// The email of the user.
    /// </summary>
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    /// <summary>
    /// The password of the user.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    /// <summary>
    /// The confirmation password of the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }
}
