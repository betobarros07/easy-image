using System;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace ImageCrop.Tests.Net45
{
    using ImageCrop.Net45;

    /// <summary>
    /// Summary description for ResizeImageTest
    /// </summary>
    [TestClass]
    public class ResizeImageTest
    {
        public ResizeImageTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void DoResize()
        {
            var absolutePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("bin\\Debug", "");
            var path = Path.Combine(absolutePath, "Images\\Koala.jpg");
            var image = Image.FromFile(path);
            var teste = image.Resize(50, 10, 500, 200);
            var pathTo = Path.Combine(absolutePath, "Images\\Koala.jpg");
            teste.Save("c:\\testes.jpg");
        }
    }
}
