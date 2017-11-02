using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCommerce.RazorStore.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace DemoStore_uCommerce.Controller
{
    public class HomeController : RenderMvcController
    {
        // GET: HomepageCatalog
        public override ActionResult Index(RenderModel model)
        {
            model = (ProductsViewModel)model;
            return base.View();
        }
    }
}