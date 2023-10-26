using System.Collections.ObjectModel;

namespace DEH1G0_SOF_2022231.Helpers;

/// <summary>
/// interface for a paginated list.
/// </summary>
/// <typeparam name="T">The type of the items in the list.</typeparam>
public interface IPaginatedList<T> :IList<T> where T : class
{
    /// <summary>
    /// Gets the index of the current page.
    /// </summary>
    int PageIndex { get; init; }
    
    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    int TotalPages { get; init; }
    
    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    bool HasPreviousPage { get; }
    
    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    bool HasNextPage { get; }
}