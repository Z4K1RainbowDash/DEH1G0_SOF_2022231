
namespace DEH1G0_SOF_2022231.Models.Helpers;

/// <summary>
///Represents the "Music" category, which includes various subcategories.
/// </summary>
public class Music
{
    /// <summary>
    /// Gets or sets value indicating whether <see cref ="Music"/> category is currently selected or not
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "Mp3Hu" subcategory is currently selected.
    /// </summary>
    public bool Mp3Hu { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "Mp3En" subcategory is currently selected.
    /// </summary>
    public bool Mp3En { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "LosslessHu" subcategory is currently selected.
    /// </summary>
    public bool LosslessHu { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "LosslessEn" subcategory is currently selected.
    /// </summary>
    public bool LosslessEn { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "Clip" subcategory is currently selected.
    /// </summary>
    public bool Clip { get; set; }
}

