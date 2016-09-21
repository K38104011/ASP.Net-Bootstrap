using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class AnotherProductListViewModel
    {
        public PagedList.IPagedList<Product> Products { get; set; }
        public string searchString { get; set; }
        public string currentSearchString { get; set; }
        public int? page { get; set; }
    }
}