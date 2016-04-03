using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadImageFromFIle.Entities
{
    /// <summary>The image data file.</summary>
    /// <remarks>Represents the image metadata after the image was processed.</remarks>
    public class Image
    {
        /// <summary>
        /// Get or Sets the number of pixels in the the row of the image
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// Gets or Sets the number of columns in the image
        /// </summary>
        public int Columns { get; set; }
        /// <summary>
        /// Gets or sets the name of the image file
        /// </summary>
        public string Name;
        /// <summary>
        /// Gets or sets the image bitmap
        /// </summary>
        public byte[,] Data;

    }
}
