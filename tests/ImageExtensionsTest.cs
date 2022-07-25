using System.Drawing;

namespace O7.EasyImage.Tests;

/// <summary>
/// Test class for ImageExtensions
/// </summary>
public class ImageExtensionsTest
{
    /// <summary>
    /// Absolute path to get the images.
    /// </summary>
    private readonly string _absolutePath;

    /// <summary>
    /// Initialize _absolutePath.
    /// </summary>
    public ImageExtensionsTest()
    {
        // Used to back to default folder
        _absolutePath = Path.GetFullPath("../../..");
    }

    /// <summary>
    /// Get a image from Images directory.
    /// </summary>
    /// <param name="imageName">Name of image from Images directory.</param>
    /// <returns>Image from Images directory</returns>
    private Image GetImage(string imageName)
    {
        var path = Path.Combine(_absolutePath, "Images", imageName);
        return Image.FromFile(path);
    }

    /// <summary>
    /// Do a complex Crop.
    /// </summary>
    [Fact]
    public void DoComplexCrop()
    {
        var image = GetImage("Penguins.jpg");
        var croppedImage = image.Crop(70, 130, 150, 50);
        // New size is:
        // 1024 - (70 + 30) = 824 width
        // 768 - (50 + 150) = 568 height
        croppedImage.Width.Should().Be(824, "Is the width for the cropped image.");
        croppedImage.Height.Should().Be(568, "Is the height for the cropped image.");
    }

    /// <summary>
    /// Do a simple Height resize.
    /// </summary>
    [Fact]
    public void DoHeightResize()
    {
        var image = GetImage("Desert.jpg");
        var resizedImage = image.HeightResize(499);
        // New width is:
        // (1024 * 499) / 768 = 665
        resizedImage.Width.Should().Be(665, "Is the width for the resized image.");
        resizedImage.Height.Should().Be(499, "Is the height for the resized image.");
    }

    /// <summary>
    /// Do a simple resize.
    /// </summary>
    [Fact]
    public void DoResize()
    {
        var image = GetImage("Tulips.jpg");
        var resizedImage = image.Resize(800, 450);
        resizedImage.Width.Should().Be(800, "Is the width for the resized image.");
        resizedImage.Height.Should().Be(450, "Is the height for the resized image.");
    }

    /// <summary>
    /// Do a simple Crop.
    /// </summary>
    [Fact]
    public void DoSimpleCrop()
    {
        var image = GetImage("Hydrangeas.jpg");
        var croppedImage = image.Crop(120, 50);
        // New size is:
        // 1024 - 120 * 2 = 784 width
        // 768 - 50 * 2 = 668 height
        croppedImage.Width.Should().Be(784, "Is the width for the cropped image.");
        croppedImage.Height.Should().Be(668, "Is the height for the cropped image.");
    }

    /// <summary>
    /// Do a simple width resize.
    /// </summary>
    [Fact]
    public void DoWidthResize()
    {
        var image = GetImage("Koala.jpg");
        var resizedImage = image.WidthResize(665);
        // New heigth is:
        // (768 * 665) / 1024 = 498
        resizedImage.Width.Should().Be(665, "Is the width for the resized image.");
        resizedImage.Height.Should().Be(498, "Is the height for the resized image.");
    }
}
