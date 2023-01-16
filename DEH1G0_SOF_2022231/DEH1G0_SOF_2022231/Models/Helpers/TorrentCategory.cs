namespace DEH1G0_SOF_2022231.Models.Helpers
{
    /// <summary>
    /// The class represents a category of a torrent
    /// </summary>
    public class TorrentCategory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TorrentCategory"/> class.
        /// </summary>
        /// <param name="name">The name of the category</param>
        /// <param name="isSelected">A boolean value indicating whether this category is currently selected or not</param>
        /// <param name="subcategories">A list of subcategories associated with this category</param>
        /// <exception cref="ArgumentNullException">Thrown if the name or subcategories parameter is null</exception>
        public TorrentCategory(string name, bool isSelected, List<TorrentSubcategory> subcategories)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsSelected = isSelected;
            Subcategories = subcategories ?? throw new ArgumentNullException(nameof(subcategories));
        }

        /// <summary>
        /// The name of the category
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// A boolean value indicating whether this subcategory is currently selected or not
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// A list of subcategories associated with this category
        /// </summary>
        public List<TorrentSubcategory> Subcategories { get; init; } 

    }
}
