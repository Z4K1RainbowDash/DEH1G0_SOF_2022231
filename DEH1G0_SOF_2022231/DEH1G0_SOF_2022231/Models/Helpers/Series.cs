using System.ComponentModel;

namespace DEH1G0_SOF_2022231.Models.Helpers
{
    /// <summary>
    /// This class contains the checkboxes for the <see cref ="Series"/>.
    /// </summary>
    public class Series
    {
        /// <summary>
        /// Gets or sets value indicating whether <see cref ="Series"/> category is currently selected or not
        /// </summary>
        [DisplayName("Series")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "SdHu" <see cref ="Series"/> Checkbox is active.
        /// </summary>
        [DisplayName("SD/HU")]
        public bool SdHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "SdEn" <see cref ="Series"/> Checkbox is active.
        /// </summary>
        [DisplayName("SD/EN")]
        public bool SdEn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "DvdrHu" <see cref ="Series"/> Checkbox is active.
        /// </summary>
        [DisplayName("DVDR/HU")]
        public bool DvdrHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "DvdrEn" <see cref ="Series"/> Checkbox is active.
        /// </summary>
        [DisplayName("DVDR/EN")]
        public bool DvdrEn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "HdHu" <see cref ="Series"/> Checkbox is active.
        /// </summary>
        [DisplayName("HD/HU")]
        public bool HdHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "HdEn" <see cref ="Series"/> Checkbox is active.
        /// </summary>
        [DisplayName("HD/EN")]
        public bool HdEn { get; set; }

    }
}
