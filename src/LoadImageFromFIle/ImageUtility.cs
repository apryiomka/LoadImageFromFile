using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using LoadImageFromFIle.Exceptions;
using Image = LoadImageFromFIle.Entities.Image;
using SystemImage = System.Drawing.Image;

namespace LoadImageFromFIle
{
    /// <summary>
    /// Image utility
    /// </summary>
    /// <remarks>used to load image metadata from a file</remarks>
    public static class ImageUtility
    {
        /// <summary>
        /// Returns the <see cref="Image"/> instance from the path
        /// </summary>
        /// <param name="fullFilePath">The file path</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">fullFilePath is required.</exception>
        /// <exception cref="FileNotFoundException">file doenst exist or not valid.</exception>
        public static Image LoadImageFromFile(this string fullFilePath)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath)) throw new ArgumentException("Image file path is required.", nameof(fullFilePath));
            if (!File.Exists(fullFilePath)) throw new FileNotFoundException();

            //declare deligate to convert RGB bytes to a 8 BIT HEX represantion
            //Bit       7 6 5 4 3 2 1 0
            //Data      R R R G G G B B
            //https://upload.wikimedia.org/wikipedia/commons/9/93/256colour.png
            Func<Color, byte> encode = c => (byte)((c.R & 0xE0) | ((c.G & 0xE0) >> 3) | (c.B >> 6)); //shift three bytes for GREEN and 6 bites for BLUE

            using (var bitMap = (Bitmap)SystemImage.FromFile(fullFilePath))
            {
                if (bitMap.PixelFormat != PixelFormat.Format8bppIndexed) throw new ImageColorDepthException(); //chekc the image depth for 8 bit image
                //create a pixel map
                var colors = new byte[bitMap.Height, bitMap.Width];

                //loop thru the pixels
                for (var h = 0; h < bitMap.Height; h++)
                {
                    for (var w = 0; w < bitMap.Width; w++)
                    {
                        var color = bitMap.GetPixel(w, h);
                        colors[h, w] = encode(color);
                    }
                }

                return new Image
                {
                    Name = Path.GetFileName(fullFilePath),
                    Columns = bitMap.Width,
                    Rows = bitMap.Height,
                    Data = colors
                };
            }
        }
    }
}
