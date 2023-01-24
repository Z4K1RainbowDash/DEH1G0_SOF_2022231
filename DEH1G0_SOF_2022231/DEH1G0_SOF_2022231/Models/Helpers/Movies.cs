using System.ComponentModel;

namespace DEH1G0_SOF_2022231.Models.Helpers
{
    /// <summary>
    /// This class contains the checkboxes for the <see cref ="Movies"/>.
    /// </summary>
    public class Movies
    {

        /// <summary>
        /// Gets or sets value indicating whether <see cref ="Movies"/> category is currently selected or not
        /// </summary>
        [DisplayName("Movies")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "SdHu" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("SD/HU")]
        public bool SdHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "SdEn" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("SD/EN")]
        public bool SdEn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "DvdrHu" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("DVDR/HU")]
        public bool DvdrHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "DvdrEn" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("DVDR/EN")]
        public bool DvdrEn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Dvd9Hu" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("DVD9/HU")]
        public bool Dvd9Hu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Dvd9En" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("DVD9/EN")]
        public bool Dvd9En { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "HdHu" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("HD/HU")]
        public bool HdHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "HdEn" <see cref ="Movies"/> Checkbox is active.
        /// </summary>
        [DisplayName("HD/EN")]
        public bool HdEn { get; set; }

       
    }
}
