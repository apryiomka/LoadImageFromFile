using System;
using System.IO;
using LoadImageFromFIle.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoadImageFromFIle.Test
{
    [TestClass]
    public class ImageTests
    {
        private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Invalid_Path_Format()
        {
            Assert.IsNull("some garbage path &**$&&(__@#@$|}][".LoadImageFromFile());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Path_Empty_String_Provided()
        {
            Assert.IsNull("".LoadImageFromFile());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Path_White_Spaces_Provided()
        {
            Assert.IsNull("      ".LoadImageFromFile());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Path_Is_Null_Provided()
        {
            Assert.IsNull(default(string).LoadImageFromFile());
        }

        [TestMethod]
        [ExpectedException(typeof(ImageColorDepthException))]
        public void Invalid_16bit_image_Provided()
        {
            Assert.IsNull(Path.Combine(BasePath, @"TestImages\16BitImage.bmp").LoadImageFromFile());
        }

        [TestMethod]
        public void Load_Valid_8bit_image()
        {
            //3 x 2 image: 3 pixels width, two pixels height
            var bitMap = Path.Combine(BasePath, @"TestImages\8BitImage.bmp").LoadImageFromFile();
            var expectedBitmap = new byte[,]
            {
                //black, yellow, black
                {0, 248, 0}, 
                //blue, red, green
                {1, 224, 28}
            };

            Assert.IsNotNull(bitMap);
            Assert.AreEqual(bitMap.Columns, 3);
            Assert.AreEqual(bitMap.Rows, 2);
            Assert.AreEqual(bitMap.Name, "8BitImage.bmp");

            for (var h = 0; h < 2; h++)
            {
                for (var w = 0; w < 3; w++)
                {
                    Assert.AreEqual(bitMap.Data[h,w], expectedBitmap[h,w]);
                }
            }
                
        }


    }
}
