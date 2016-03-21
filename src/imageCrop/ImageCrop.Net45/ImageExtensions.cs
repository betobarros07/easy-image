using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCrop.Net45
{
    public static class ImageExtensions
    {
        private const int CROP_WIDTH = 500;
        private const int CROP_HEIGHT = 200;

        public static Bitmap Resize(this Image image, int x, int y, int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            var horizontalResolution = image.HorizontalResolution;
            var verticalResolution = image.VerticalResolution;
            bitmap.SetResolution(horizontalResolution, verticalResolution);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                using (var imageAttributes = new ImageAttributes())
                {
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    imageAttributes.SetWrapMode(WrapMode.TileFlipXY);

                    var widthStart = 0;
                    var heightStart = 0;
                    var rectangle = new Rectangle(widthStart, heightStart, width, height);

                    graphics.DrawImage(image, rectangle, widthStart, heightStart, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
                }
            }
            return bitmap;
        }


        public static Image Crop(this Image imagem)
        {
            try
            {
                var width = imagem.Width;
                var height = imagem.Height;

                double proporcaoLargura = (double)width / (double)CROP_WIDTH;
                double proporcaoAltura = (double)height / (double)CROP_HEIGHT;

                if (proporcaoAltura > proporcaoLargura)
                {
                    width = Convert.ToInt32(proporcaoLargura * CROP_WIDTH);
                    height = Convert.ToInt32(proporcaoLargura * CROP_HEIGHT);
                }
                else
                {
                    width = Convert.ToInt32(proporcaoAltura * CROP_WIDTH);
                    height = Convert.ToInt32(proporcaoAltura * CROP_HEIGHT);
                }

                if (width < CROP_WIDTH)
                {
                    width = CROP_WIDTH;
                }
                if (height < CROP_HEIGHT)
                {
                    height = CROP_HEIGHT;
                }


                var bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bitmap.SetResolution(72, 72);

                var graphics = Graphics.FromImage(bitmap);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var heightStart = (imagem.Height - height) / 2;
                var widthStart = (imagem.Width - width) / 2;
                if (widthStart < 0)
                {
                    widthStart = 0;
                }
                if (heightStart < 0)
                {
                    heightStart = 0;
                }

                var rectangle = new Rectangle(0, 0, width, height);
                graphics.DrawImage(imagem, rectangle, widthStart, heightStart, width, height, GraphicsUnit.Pixel);

                var memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, ImageFormat.Jpeg);
                imagem.Dispose();
                bitmap.Dispose();
                graphics.Dispose();

                imagem = Image.FromStream(memoryStream);


                return imagem.GetThumbnailImage(CROP_WIDTH, CROP_HEIGHT, null, IntPtr.Zero);
            }
            catch
            {
                return null;
            }
        }
    }
}
