# Easy Image

[![Build](https://travis-ci.org/BetoBarros07/easy_image.svg)](https://travis-ci.org/BetoBarros07/easy_image)
[![Nuget Version](http://img.shields.io/nuget/v/EasyImage.svg)](http://www.nuget.org/packages/EasyImage)
[![Issues open](https://img.shields.io/github/issues/betobarros07/easyimage.svg)](https://github.com/BetoBarros07/easy_image/issues)
[![Unlicense](https://img.shields.io/badge/license-unlicense-orange.svg)](LICENSE)

Easy Image is a library that provides, basically, functions to resize and crop images.

You will have 4 functions, Crop, Resize, HeightResize and WidthResize.

All functions are a extension of Image class.

## Crop

The Crop function will cut a piece of an image and resize it. The crop is based in four coordinates(x1, x2 and y1, y2):

* `X` - Is the quantity of pixels that the image will be cutted from left and right, for example.
* `Y` - Is the quantity of pixels that the image will be cutted from top and bottom.

So, width is equal to width - (x1 + x2) and height is equal to height - (y1 + y2).

### Parameters

* `x1:` x1 coordinate(left) in pixels.
* `x2:` x2 coordinate(right) in pixels.
* `y1:` y1 coordinate(top) in pixels.
* `y2:` y2 coordinate(bottom) in pixels.

### Example

```c#
using EasyImage;
using System.Drawing;

namespace Foo
{
    public class Bar
    {
        public static void Main(string[] args)
        {
            var image = Image.FromFile("C:\\foo-bar.jpg");
            image.Crop(100, 150, 75, 50);
        }
    }
}
```

## Simple Crop

The Simple Crop function is a method overloading of Crop function, basically the x will be the x1 and x2 and the same thing to y, like the following code.

```c#
public static Bitmap Crop(this Image image, int x, int y)
{
    return image.Crop(x, x, y, y);
}
```

So, width is equal to width - x * 2 and height is equal to height - y * 2.

### Parameters

* `x:` x coordinate in pixels.
* `y:` y coordinate in pixels.

### Example

```c#
using EasyImage;
using System.Drawing;

namespace Foo
{
    public class Bar
    {
        public static void Main(string[] args)
        {
            var image = Image.FromFile("C:\\foo-bar.jpg");
            image.Crop(100, 75);
        }
    }
}
```

## Resize

The resize function is very easy and simple to understand, you just set two parameters, width and height, and the image size will be setted to the new size.

### Parameters

* `width:` New width to image.
* `height:` New height to image.

### Example

```c#
using EasyImage;
using System.Drawing;

namespace Foo
{
    public class Bar
    {
        public static void Main(string[] args)
        {
            var image = Image.FromFile("C:\\foo-bar.jpg");
            image.Resize(750, 350);
        }
    }
}
```

## HeightResize

The height resize function is similar to resize function, the difference is that you set just the new height of an image, and the width will be resized proportionally to a new value.
So, if image have 1024x768 and you set the height to 350px, your width will be setted to 466px.
The formula to check the proportional width is:

* newWidth = (newHeight * width) / height

### Parameters

* `height:` New height to image.

### Example

```c#
using EasyImage;
using System.Drawing;

namespace Foo
{
    public class Bar
    {
        public static void Main(string[] args)
        {
            var image = Image.FromFile("C:\\foo-bar.jpg");
            image.HeightResize(350);
        }
    }
}
```

## WidthResize

The width resize function have the same logic of height resize function, the difference is that you will set the width, and the height will be resized proportionally to a new value.
So, if image have 1024x768 and you set the width to 750px, your height will be setted to 562px.
The formula to check the proportional height is:

* newHeight = (newWidth * height) / width

### Parameters

* `width:` New width to image.

### Example

```c#
using EasyImage;
using System.Drawing;

namespace Foo
{
    public class Bar
    {
        public static void Main(string[] args)
        {
            var image = Image.FromFile("C:\\foo-bar.jpg");
            image.WidthResize(350);
        }
    }
}
```

## Considerations

If you want to use the easy image library you need to know some things.

1. You don't need have the image saved on server or physically.
2. You can get images from stream or whatever you want, you just need an instance of Image class.
3. All the functions do is creating a graphical draw in memory, so if you use some function, **you haven't saved image yet**.
4. If you want to save the image, just call this:

```c#
using EasyImage;
using System.Drawing;

namespace Foo
{
    public class Bar
    {
        public static void Main(string[] args)
        {
            var image = Image.FromFile("C:\\foo-bar.jpg");
            image.Resize(350, 200);
            image.Save("C:\\foo-bar_thumb.jpg");
        }
    }
}
```

## License

[UNLICENSE](LICENSE) © [Beto Barros](https://github.com/betobarros07)
