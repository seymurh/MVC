using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testApp;
using testApp.Controllers;

namespace testApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void Index()
        {
            string b = "BAKI";
            System.Globalization.CultureInfo info = System.Globalization.CultureInfo.CreateSpecificCulture("az");
            string l = b.ToLower(info);
            Assert.AreEqual<string>(l, "bakı");
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Index() as ViewResult;

            //// Assert
            //Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.About() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            //HomeController controller = new HomeController();

            //// Act
            //ViewResult result = controller.Contact() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }
    }
}
