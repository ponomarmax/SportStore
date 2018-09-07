﻿using SportStore.Domain.Abstract;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;
        public ProductController(IProductsRepository productRepository)
        {
            this.repository = productRepository;
        }
        public int PageSize = 4;
        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.OrderBy(a => a.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };
            return View(model);
        }
    }
}