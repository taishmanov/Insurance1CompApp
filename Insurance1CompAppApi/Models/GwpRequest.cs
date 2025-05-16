using System.Collections.Generic;

namespace Insurance1CompApp.Models
{
    public class GwpRequest
    {
        /// <summary>
        /// Country name
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// List of lines of business (LOB)
        /// </summary>
        public List<string> Lob { get; set; }
    }
}
