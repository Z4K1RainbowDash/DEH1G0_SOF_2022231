namespace DEH1G0_SOF_2022231.Helpers;

    /// <summary>
    /// This class store the URL for Ncore.
    /// </summary>
    public class NcoreUrl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NcoreUrl"/> class.
        /// </summary>
        /// <param name="url">URL for Ncore.</param>
        public NcoreUrl(string url)
        {
            this.Url = url;
        }

        /// <summary>
        /// Gets Ncore URL.
        /// </summary>
        public string Url { get; init; }
    }

