using System.ComponentModel;

namespace DEH1G0_SOF_2022231.Models.Helpers
{
    /// <summary>
    /// This class contains the checkboxes for the <see cref ="Musics"/>.
    /// </summary>
    public class Musics
    {
        /// <summary>
        /// Gets or sets value indicating whether <see cref ="Musics"/> category is currently selected or not
        /// </summary>
        [DisplayName("Musics")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Mp3Hu" <see cref ="Musics"/> Checkbox is active.
        /// </summary>
        [DisplayName("MP3/HU")]
        public bool Mp3Hu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Mp3En" <see cref ="Musics"/> Checkbox is active.
        /// </summary>
        [DisplayName("MP3/EN")]
        public bool Mp3En { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "LosslessHu" <see cref ="Musics"/> Checkbox is active.
        /// </summary>
        [DisplayName("Lossless/HU")]
        public bool LosslessHu { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "LosslessEn" <see cref ="Musics"/> Checkbox is active.
        /// </summary>
        [DisplayName("Lossless/EN")]
        public bool LosslessEn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the "Clip" <see cref ="Musics"/> Checkbox is active.
        /// </summary>
        public bool Clip { get; set; }
    }
}
