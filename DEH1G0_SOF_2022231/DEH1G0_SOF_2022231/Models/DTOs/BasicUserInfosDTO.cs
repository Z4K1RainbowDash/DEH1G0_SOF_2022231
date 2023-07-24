namespace DEH1G0_SOF_2022231.Models.DTOs
{
/// <summary>
/// Represent a basic information of user for API communications.
/// </summary>
public class BasicUserInfosDTO
{
    /// <summary>
    /// Gets or sets the user's id.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Gets or sets the user's email.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// Gets or sets the user's email confirmation.
    /// </summary>
    public bool EmailConfirmed { get; set; }
    
    /// <summary>
    /// Gets or sets the user's access failed count.
    /// </summary>
    public int AccessFailedCount { get; set; }
    
    /// <summary>
    /// Gets or sets the user's role.
    /// </summary>
    public string? Role { get; set; }
}
}