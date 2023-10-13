using System.ComponentModel;

namespace DEH1G0_SOF_2022231.Models.Helpers;

    /// <summary>
    /// This class contains the checkboxes for the <see cref ="Programs"/>.
    /// </summary>
    public class Programs
    {
        /// <summary>
        /// Gets or sets value indicating whether <see cref ="Programs"/> category is currently selected or not
        /// </summary>
        [DisplayName("Programs")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Iso" <see cref ="Programs"/> Checkbox is active.
        /// </summary>
        [DisplayName("PROG/ISO")]
        public bool Iso { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Rip" <see cref ="Programs"/> Checkbox is active.
        /// </summary>
        [DisplayName("PROG/RIP")]
        public bool Rip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Mobile" <see cref ="Programs"/> Checkbox is active.
        /// </summary>
        [DisplayName("Mobile")]
        public bool Mobile { get; set; }
}
