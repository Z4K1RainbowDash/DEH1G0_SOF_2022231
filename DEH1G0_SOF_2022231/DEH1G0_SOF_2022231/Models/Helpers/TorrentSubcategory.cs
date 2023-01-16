namespace DEH1G0_SOF_2022231.Models.Helpers
{
    /// <summary>
    /// The class represents a subcategory of a torrent
    /// </summary>
    public class TorrentSubcategory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TorrentSubcategory"/> class.
        /// </summary>
        /// <param name="name">The name of the subcategory.</param>
        /// <param name="isSelected">A boolean value indicating whether this subcategory is currently selected or not.</param>
        /// <exception cref="ArgumentNullException">Thrown if the name parameter is null.</exception>
        public TorrentSubcategory(string name, bool isSelected)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsSelected = isSelected;
        }

        /// <summary>
        /// The name of the subcategory
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// A boolean value indicating whether this subcategory is currently selected or not
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
