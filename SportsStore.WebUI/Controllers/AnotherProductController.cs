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
        private int PageSize = 4;
        private List<string> _searchFilters = new string[] {
            "All", "Price", "Description", "Name", "Category"
        }.ToList();

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
                model.selectedFilter = model.currentSelectedFilter;
            }

            model.currentSearchString = model.searchString;
            model.currentSelectedFilter = model.selectedFilter;

            if (!String.IsNullOrEmpty(model.searchString))
            {
                switch (model.selectedFilter)
                {
                    case "All" : products = products.Where(s => s.Name.Contains(model.searchString)
                       || s.Price.ToString().Contains(model.searchString)
                       || s.Category.Contains(model.searchString)
                       || s.Description.Contains(model.searchString)
                   ); break;
                    case "Price" : products = products.Where(s => s.Price.ToString().Contains(model.searchString)); break;
                    case "Description" : products = products.Where(s =>
                       s.Description.IndexOf(model.searchString, StringComparison.OrdinalIgnoreCase) >= 0); break;
                    case "Name": products = products.Where(s =>
                       s.Name.IndexOf(model.searchString, StringComparison.OrdinalIgnoreCase) >= 0); break;
                    case "Category": products = products.Where(s =>
                       s.Category.IndexOf(model.searchString, StringComparison.OrdinalIgnoreCase) >= 0); break;
                }
            }

            int pageNumber = model.page ?? 1;
            model.Products = products.ToPagedList(pageNumber, PageSize);
            
            return View(model);
        }

    }
}
