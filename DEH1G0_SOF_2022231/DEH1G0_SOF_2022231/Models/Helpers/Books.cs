namespace DEH1G0_SOF_2022231.Models.Helpers;

namespace DEH1G0_SOF_2022231.Models.Helpers
{
    /// <summary>
    /// This class contains the checkboxes for the <see cref ="Books"/>.
    /// </summary>
    public class Books
    {
        /// <summary>
        /// Gets or sets value indicating whether <see cref ="Books"/> category is currently selected or not
        /// </summary>
        [DisplayName("Books")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "EBookHu" <see cref ="Books"/> Checkbox is active.
        /// </summary>
        [DisplayName("eBook/HU")]
        public bool EBookHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "EBookEn" <see cref ="Books"/> Checkbox is active.
        /// </summary>
        [DisplayName("eBook/HU")]
        public bool EBookEn { get; set; }

       
    }
}
