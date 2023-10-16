namespace DEH1G0_SOF_2022231.Models.Helpers;

/// <summary>
/// Represents the "Games" category, which includes various subcategories.
/// </summary>
public class Games
{
    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="Games"/> category is currently selected.
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
    /// Gets or sets a value indicating whether the "Console" subcategory is currently selected.
    /// </summary>
    public bool Console { get; set; }
}