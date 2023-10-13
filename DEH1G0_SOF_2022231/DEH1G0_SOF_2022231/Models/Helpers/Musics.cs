
namespace DEH1G0_SOF_2022231.Models.Helpers;

    /// <summary>
    /// This class contains the checkboxes for the <see cref ="Music"/>.
    /// </summary>
    public class Music
    {
        /// <summary>
        /// Gets or sets value indicating whether <see cref ="Music"/> category is currently selected or not
        /// </summary>
        [DisplayName("Music")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Mp3Hu" <see cref ="Music"/> Checkbox is active.
        /// </summary>
        [DisplayName("MP3/HU")]
        public bool Mp3Hu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Mp3En" <see cref ="Music"/> Checkbox is active.
        /// </summary>
        [DisplayName("MP3/EN")]
        public bool Mp3En { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "LosslessHu" <see cref ="Music"/> Checkbox is active.
        /// </summary>
        [DisplayName("Lossless/HU")]
        public bool LosslessHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "LosslessEn" <see cref ="Music"/> Checkbox is active.
        /// </summary>
        [DisplayName("Lossless/EN")]
        public bool LosslessEn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Clip" <see cref ="Music"/> Checkbox is active.
        /// </summary>
        public bool Clip { get; set; }
}
