namespace DEH1G0_SOF_2022231.Models.Helpers;

/// <summary>
/// Represents the "Books" category, which includes various subcategories.
/// </summary>
public class Books
{
    /// <summary>
    /// Gets or sets value indicating whether <see cref ="Books"/> category is currently selected or not
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "EBookHu" subcategory is currently selected.
    /// </summary>
    public bool EBookHu { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "EBookEn" subcategory is currently selected.
    /// </summary>
    public bool EBookEn { get; set; }
}

