using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Controllers;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.HtmlHelpers;
using SportStore.Models;

namespace SportStore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void List_Pagination_CorrectItemsReturns()
        {
            //Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(a => a.Products).Returns(new Product[]
            {
               new Product {ProductID = 1, Name = "P1"},
               new Product {ProductID = 2, Name = "P2"},
               new Product {ProductID = 3, Name = "P3"},
               new Product {ProductID = 4, Name = "P4"},
               new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");

        }
        [TestMethod]
        public void HtmlHelperPageLinks_GenerateLinks_CorrectHtml()
        {
            //Arrange
            HtmlHelper htmlHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;


            //Act
            MvcHtmlString result = htmlHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                            result.ToString());
        }
        [TestMethod]
        public void List_SendPagininInfo_Correct()
        {
            //Arrange
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(a => a.Products).Returns(new Product[]
            {
               new Product {ProductID = 1, Name = "P1"},
               new Product {ProductID = 2, Name = "P2"},
               new Product {ProductID = 3, Name = "P3"},
               new Product {ProductID = 4, Name = "P4"},
               new Product {ProductID = 5, Name = "P5"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //Act
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            //Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }
    }
}
