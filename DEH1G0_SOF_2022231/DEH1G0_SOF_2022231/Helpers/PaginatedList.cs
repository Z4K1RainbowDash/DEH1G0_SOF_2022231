namespace DEH1G0_SOF_2022231.Helpers;


/// <summary>
/// Represent a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of the items in the list.</typeparam>
public class PaginatedList<T> : List<T>, IPaginatedList<T> where T : class
{
    /// <summary>
    /// Gets the index of the current page.
    /// </summary>
    public int PageIndex { get; init; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/>
    /// </summary>
    /// <param name="items">The items to be included in the list.</param>
    /// <param name="count">The total number of items.</param>
    /// <param name="pageQueryParameters"></param>
    public PaginatedList(IEnumerable<T> items, int count, PageQueryParameters pageQueryParameters)
    {
        PageIndex = pageQueryParameters.PageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageQueryParameters.PageSize);

        this.AddRange(items);
    }

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => (PageIndex > 1);

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage => (PageIndex < TotalPages);
}
