using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageCrop.Net45
{
    /// <summary>
    /// Image extensions class for .Net Framework 4.5.
    /// <para/>Here you have all the resize and crop functions for Image class, Have fun!.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Resize the image based on height.
        /// <para/>Keeps width proportional.
        /// </summary>
        /// <example>
        /// How to use HeightResize function:
        /// <code>
        /// var image = Image.FromFile("C:\\images\\bar.jpg");
        /// image.HeightResize(500);
        /// </code>
        /// </example>
        /// <see cref="Image.FromFile(string)"/>
        /// <seealso cref="Resize(Image, int, int)"/>
        /// <param name="image">The image to be resized.</param>
        /// <param name="height">The new height of image.</param>
        /// <returns>The new resized image.</returns>
        public static Bitmap HeightResize(this Image image, int height)
        {
            var width = (height * image.Width) / image.Height;
            return image.Resize(width, height);
        }

        /// <summary>
        /// Resize the image.
        /// </summary>
        /// <example>
        /// How to use Resize function:
        /// <code>
        /// var image = Image.FromFile("C:\\images\\foo-bar.jpg");
        /// image.HeightResize(400, 500);
        /// </code>
        /// </example>
        /// <see cref="Image.FromFile(string)"/>
        /// <param name="image">The image to be resized.</param>
        /// <param name="width">The new width of image.</param>
        /// <param name="height">The new height of image.</param>
        /// <returns>The new resized image.</returns>
        public static Bitmap Resize(this Image image, int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            var horizontalResolution = image.HorizontalResolution;
            var verticalResolution = image.VerticalResolution;
            bitmap.SetResolution(horizontalResolution, verticalResolution);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                using (var imageAttributes = new ImageAttributes())
                {
                    var widthStart = 0;
                    var heightStart = 0;
                    imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
                    var rectangle = new Rectangle(widthStart, heightStart, width, height);
                    graphics.DrawImage(image, rectangle, widthStart, heightStart, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }
            return bitmap;
        }

        /// <summary>
        /// Resize the image based on width.
        /// <para/>Keeps height proportional.
        /// </summary>
        /// <example>
        /// How to use WidthResize function:
        /// <code>
        /// var image = Image.FromFile("C:\\images\\foo.jpg");
        /// image.WidthResize(400);
        /// </code>
        /// </example>
        /// <see cref="Image.FromFile(string)"/>
        /// <seealso cref="Resize(Image, int, int)"/>
        /// <param name="image">The image to be resized.</param>
        /// <param name="width">The new width of image.</param>
        /// <returns>The new resized image.</returns>
        public static Bitmap WidthResize(this Image image, int width)
        {
            var height = (width * image.Height) / image.Width;
            return image.Resize(width, height);
        }
    }
}
