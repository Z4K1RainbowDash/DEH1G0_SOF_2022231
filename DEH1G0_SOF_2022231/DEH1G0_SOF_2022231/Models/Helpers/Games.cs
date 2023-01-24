using System.ComponentModel;

namespace DEH1G0_SOF_2022231.Models.Helpers
{
    /// <summary>
    /// This class contains the checkboxes for the <see cref ="Games"/>.
    /// </summary>
    public class Games
    {
        /// <summary>
        /// Gets or sets value indicating whether <see cref ="Games"/> category is currently selected or not
        /// </summary>
        [DisplayName("Games")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Iso" <see cref ="Games"/> Checkbox is active.
        /// </summary>
        [DisplayName("PC/ISO")]
        public bool Iso { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Rip" <see cref ="Games"/> Checkbox is active.
        /// </summary>
        [DisplayName("PC/RIP")]
        public bool Rip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Console" <see cref ="Games"/> Checkbox is active.
        /// </summary>
        [DisplayName("Console")]
        public bool Console { get; set; }
    }
}
