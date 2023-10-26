namespace DEH1G0_SOF_2022231.Helpers;

/// <summary>
/// Interface for a paginated list builder.
/// </summary>
public interface IPaginatedListBuilder
{
    /// <summary>
    /// Build a paginated list.
    /// </summary>
    /// <param name="source">The source data to be paginated.</param>
    /// <param name="pageQueryParameters">The parameters for the page query.</param>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <returns>A paginated list of type T.</returns>
    Task<PaginatedList<T>> BuildAsync<T>(IQueryable<T> source, PageQueryParameters pageQueryParameters) where T : class;
}