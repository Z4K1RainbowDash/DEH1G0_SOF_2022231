
namespace DEH1G0_SOF_2022231.Models.Helpers;

/// <summary>
/// Represents the "Programs" category, which includes various subcategories.
/// </summary>
public class Programs
{
    /// <summary>
    /// Gets or sets value indicating whether <see cref ="Programs"/> category is currently selected or not
    /// </summary>
    public bool IsSelected { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "Iso" subcategory is currently selected.
    /// </summary>
    public bool Iso { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "Rip" subcategory is currently selected.
    /// </summary>
    public bool Rip { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the "Mobile" subcategory is currently selected.
    /// </summary>
    public bool Mobile { get; set; }
}

