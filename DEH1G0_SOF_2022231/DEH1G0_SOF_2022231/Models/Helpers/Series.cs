
namespace DEH1G0_SOF_2022231.Models.Helpers;

/// <summary>
/// Represents the "Series" category, which includes various subcategories.
/// </summary>
public class Series
{
    /// <summary>
    /// Gets or sets value indicating whether <see cref ="Series"/> category is currently selected or not
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "SdHu" subcategory is currently selected.
    /// </summary>
    public bool SdHu { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "SdEn" subcategory is currently selected.
    /// </summary>
    public bool SdEn { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "DvdrHu" subcategory is currently selected.
    /// </summary>
    public bool DvdrHu { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "DvdrEn" subcategory is currently selected.
    /// </summary>
    public bool DvdrEn { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "HdHu" subcategory is currently selected.
    /// </summary>
    public bool HdHu { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "HdEn" subcategory is currently selected.
    /// </summary>
    public bool HdEn { get; set; }
}

