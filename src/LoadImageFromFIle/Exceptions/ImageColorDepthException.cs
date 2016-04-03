using System;

namespace LoadImageFromFIle.Exceptions
{
    /// <summary>
    /// Invalid image color depth.
    /// </summary>
    /// <remarks>Thrown if the image depth is greater than 8 bit</remarks>
    public class ImageColorDepthException : ApplicationException
    {
        /// <summary>
        /// Creates the instance of <see cref="ImageColorDepthException"/>
        /// </summary>
        public ImageColorDepthException() : base("Image must be less or equal 8 bit"){ }
    }
}
