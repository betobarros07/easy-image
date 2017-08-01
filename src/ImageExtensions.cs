using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace EasyImage
{
    /// <summary>
    /// Image extensions class for .Net Framework 4.5.
    /// <para/>Here you have all the resize and crop functions for Image class, Have fun!.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Crop an image.
        /// <para/>The new width and height of image will be:
        /// Width = image.Width - (x1 + x2);
        /// Height = image.Height - (y1 + y2);
        /// </summary>
        /// <example>
        /// How to use Crop function:
        /// <code>
        /// var image = Image.FromFile("C:\\images\\bar.jpg");
        /// image.Crop(100, 90, 30, 20);
        /// </code>
        /// </example>
        /// <see cref="Image.FromFile(string)"/>
        /// <param name="image">The image to be cropped.</param>
        /// <param name="x1">The left coordinate x in pixels (width spacing).</param>
        /// <param name="x2">The right coordinate x in pixels (width spacing).</param>
        /// <param name="y1">The left coordinate y in pixels (height spacing).</param>
        /// <param name="y2">The right coordinate y in pixels (height spacing).</param>
        /// <returns>The new cropped image.</returns>
        public static Bitmap Crop(this Image image, int x1, int x2, int y1, int y2)
        {
            var width = image.Width - (x1 + x2);
            var height = image.Height - (y1 + y2);
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
                var rectangle = new Rectangle(0, 0, width, height);
                graphics.DrawImage(image, rectangle, x1, y1, width, height, GraphicsUnit.Pixel);
            }
            return bitmap;
        }

        /// <summary>
        /// Crop an image.
        /// <para/>The new width and height of image will be:
        /// Width = image.Width - x * 2;
        /// Height = image.Height - y * 2;
        /// </summary>
        /// <example>
        /// How to use Crop function:
        /// <code>
        /// var image = Image.FromFile("C:\\images\\bar.jpg");
        /// image.Crop(100, 30);
        /// </code>
        /// </example>
        /// <see cref="Image.FromFile(string)"/>
        /// <param name="image">The image to be cropped.</param>
        /// <param name="x">The coordinate x in pixels (width spacing).</param>
        /// <param name="y">The coordinate y in pixels (height spacing).</param>
        /// <returns>The new cropped image.</returns>
        public static Bitmap Crop(this Image image, int x, int y)
        {
            return image.Crop(x, x, y, y);
        }

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
        /// image.Resize(400, 500);
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
