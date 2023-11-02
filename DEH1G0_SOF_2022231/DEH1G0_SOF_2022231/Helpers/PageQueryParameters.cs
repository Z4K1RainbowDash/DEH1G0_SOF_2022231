namespace DEH1G0_SOF_2022231.Helpers;

/// <summary>
/// Represents parameters for page query.
/// </summary>
public class PageQueryParameters
{
    /// <summary>
    /// Gets the index of the current page.
    /// </summary>
    public int PageIndex { get; init;}
    
    /// <summary>
    /// Gets the size of the page.
    /// </summary>
    public int PageSize { get; init; }
}