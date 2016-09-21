using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class AnotherProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public AnotherProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult Index(AnotherProductListViewModel model)
        {
            var products = repository.Products;

            if (model.searchString != null)
            {
                model.page = 1;
            }
            else
            {
                model.searchString = model.currentSearchString;
            }

            model.currentSearchString = model.searchString;

            if (!String.IsNullOrEmpty(model.searchString))
            {
                products = products.Where(s => s.Name.Contains(model.searchString) 
                    || s.Price.ToString().Contains(model.searchString) 
                    || s.Category.Contains(model.searchString)
                    || s.Description.Contains(model.searchString)
                );
            }

            int pageNumber = model.page ?? 1;
            model.Products = products.ToPagedList(pageNumber, PageSize);
            return View(model);
        }

    }
}
