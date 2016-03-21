using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace EasyImage.Tests
{
    // Reference of Library EasyImage.Net45
    using Net45;

    /// <summary>
    /// Test class for ImageExtensions used in .Net Framework 4.5
    /// </summary>
    [TestClass]
    public class Net45Test
    {
        /// <summary>
        /// Absolute path to get the images.
        /// </summary>
        private readonly string _absolutePath;

        /// <summary>
        /// Initialize _absolutePath.
        /// </summary>
        public Net45Test()
        {
            _absolutePath= Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug", "");
        }

        /// <summary>
        /// Get a image from Images directory.
        /// </summary>
        /// <param name="imageName">Name of image from Images directory.</param>
        /// <returns>Image from Images directory</returns>
        private Image GetImage(string imageName)
        {
            var path = Path.Combine(_absolutePath, "Images\\" + imageName);
            return Image.FromFile(path);
        }

        /// <summary>
        /// Save image to results folder.
        /// </summary>
        /// <param name="image">Image to be saved.</param>
        /// <returns>Path to the saved image.</returns>
        private string Save(Image image)
        {
            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var pathTo = Path.Combine(_absolutePath, "Images\\Results\\", fileName);
            image.Save(pathTo);
            return pathTo;
        }

        /// <summary>
        /// Do a simple Crop.
        /// </summary>
        [TestMethod]
        public void DoCrop()
        {
            var image = GetImage("hydrangeas.jpg");
            var crop = image.Crop(120, 50);
            var path = Save(crop);
            var croppedImage = Image.FromFile(path);
            // New size is:
            // 768 - 50 * 2 = 668 height
            // 1024 - 120 * 2 = 784 width
            croppedImage.Width.Should().Be(784, "Is the width for the cropped image.");
            croppedImage.Height.Should().Be(668, "Is the height for the cropped image.");
        }

        /// <summary>
        /// Do a simple Height resize.
        /// </summary>
        [TestMethod]
        public void DoHeightResize()
        {
            var image = GetImage("desert.jpg");
            var resize = image.HeightResize(499);
            var path = Save(resize);
            var resizedImage = Image.FromFile(path);
            // New width is:
            // (1024 * 499) / 768 = 665
            resizedImage.Width.Should().Be(665, "Is the width for the resized image.");
            resizedImage.Height.Should().Be(499, "Is the height for the resized image.");
        }

        /// <summary>
        /// Do a simple resize.
        /// </summary>
        [TestMethod]
        public void DoResize()
        {
            var image = GetImage("tulips.jpg");
            var resize = image.Resize(800, 450);
            var path = Save(resize);
            var resizedImage = Image.FromFile(path);
            resizedImage.Width.Should().Be(800, "Is the width for the resized image.");
            resizedImage.Height.Should().Be(450, "Is the height for the resized image.");
        }

        /// <summary>
        /// Do a simple width resize.
        /// </summary>
        [TestMethod]
        public void DoWidthResize()
        {
            var image = GetImage("koala.jpg");
            var resize = image.WidthResize(665);
            var path = Save(resize);
            var resizedImage = Image.FromFile(path);
            // New heigth is:
            // (768 * 665) / 1024 = 498
            resizedImage.Width.Should().Be(665, "Is the width for the resized image.");
            resizedImage.Height.Should().Be(498, "Is the height for the resized image.");
        }
    }
}
