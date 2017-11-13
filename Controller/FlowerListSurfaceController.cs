using DemoStore_uCommerce.Models;
using ImageProcessor.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using UCommerce.Api;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
using UCommerce.RazorStore.Controllers;
using UCommerce.RazorStore.Models;
using UCommerce.Runtime;
using UCommerce.Search.Facets;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace DemoStore_uCommerce.Controller
{
    public class FlowerListSurfaceController : SurfaceController
    {
        public ActionResult Index()
        {
            //  var products = SiteContext.Current.CatalogContext.CurrentCatalog.Categories.SelectMany(c => c.Products.Where(p => p.ProductProperties.Any(pp => pp.ProductDefinitionField.Name == "ShowOnHomepage" && Convert.ToBoolean(pp.Value))));
            // var products = SiteContext.Current.CatalogContext.CurrentCatalog.Categories.SelectMany(c => c.Products.Where(p => p.ProductProperties.Any(pp => pp.Product.ProductDefinition.Name == "Jul" && Convert.ToBoolean(pp.Value))));

            var products = SiteContext.Current.CatalogContext.CurrentCatalog.Categories.SelectMany(c => c.Products.Where(p => p.ProductDefinition.Name == "Jul"));

            ProductsViewModel productsViewModel = new ProductsViewModel();

            foreach (var p in products)
            {
                var id = p.ThumbnailImageMediaId;
                var content = Umbraco.Media(id);
                var imagerUrl = content.Url;

                productsViewModel.Products.Add(new ProductViewModel()
                {
                    Name = p.Name,
                    PriceCalculation = CatalogLibrary.CalculatePrice(p),
                    Url = CatalogLibrary.GetNiceUrlForProduct(p),
                    Sku = p.Sku,
                    IsVariant = p.IsVariant,
                    VariantSku = p.VariantSku,
                    ThumbnailImageUrl = imagerUrl
                });
            }
            return PartialView("/Views/PartialView/FlowerList.cshtml", productsViewModel);
        }
        public List<FlowerListViewModel> GetFlowers()
        {
            var currentCategory = SiteContext.Current.CatalogContext.CurrentCatalog.Categories.Where(c => c.Name == "Jul").First();
            var products = MapProductsInCategories(currentCategory);

            var flowers = new List<FlowerListViewModel>();
            flowers.Add(new FlowerListViewModel
            {
                Id = "2510",
                Categories = new[] { "fd", "n", "u" },
                Description = "Valfri hög bukett Blandade färger",
                MaxPrice = 180,
                Url = "https://www.ucarecdn.com/c6e02ff2-25a9-4292-9816-e835114a9a7b/-/resize/400x400/"
            });
            flowers.Add(new FlowerListViewModel
            {
                Id = "2511",
                Categories = new[] { "l" },
                Description = "Valfri hög dekoration Blandade färger",
                MaxPrice = 800,
                Url = "https://www.ucarecdn.com/8834107d-874c-4350-9a92-844036cb9636/-/resize/400x400/"
            });
            flowers.Add(new FlowerListViewModel
            {
                Id = "2512",
                Categories = new[] { "br", "f", "fd", "j", "jo", "n", "t" },
                Description = "Valfri låg bukett Blandade färger",
                MaxPrice = 500,
                Url = "https://www.ucarecdn.com/b7305e4e-3767-4c7c-8e7b-96f3134cd8a3/-/resize/400x400/"
            });
            flowers.Add(new FlowerListViewModel
            {
                Id = "2520",
                Categories = new[] { "fd", "j", "k", "n", "u" },
                Description = "Röda rosor Mellan 3 st",
                MaxPrice = 250,
                Url = "https://www.ucarecdn.com/cbaf69da-a331-4413-ad9e-0f769001afd7/-/resize/400x400/"
            });
            flowers.Add(new FlowerListViewModel
            {
                Id = "2525",
                Categories = new[] { "fd", "h", "k", "m", "u" },
                Description = "Extra grönt",
                MaxPrice = 115,
                Url = "https://www.ucarecdn.com/42799bd9-ef78-402d-aed4-3ece9cb4b604/-/resize/400x400/"
            });
            flowers.Add(new FlowerListViewModel
            {
                Id = "2526",
                Categories = new[] { "h", "k" },
                Description = "Extra brudslöja",
                MaxPrice = 115,
                Url = "https://www.ucarecdn.com/4146f607-4459-48d5-af00-f20848041df7/-/resize/400x400/"
            });
            flowers.Add(new FlowerListViewModel
            {
                Id = "2527",
                Categories = new[] { "n", "k" },
                Description = "Röda rosor större 3st",
                MaxPrice = 265,
                Url = "https://www.ucarecdn.com/0a4dbb86-8efa-4f4d-955a-2c299500f8f8/-/resize/400x400/"
            });

            return flowers;
        }

        private IList<ProductViewModel> MapProductsInCategories(Category category)
        {
            IList<Facet> facetsForQuerying = System.Web.HttpContext.Current.Request.QueryString.ToFacets();
            var productsInCategory = new List<ProductViewModel>();

            foreach (var subcategory in category.Categories)
            {
                productsInCategory.AddRange(MapProductsInCategories(subcategory));
            }

            productsInCategory.AddRange(MapProducts(category.Products));

            return productsInCategory;
        }

        private IList<ProductViewModel> MapProducts(IEnumerable<Product> productsInCategory)
        {
            IList<ProductViewModel> productViews = new List<ProductViewModel>();

            foreach (var product in productsInCategory)
            {
                var id = product.ThumbnailImageMediaId;
                var content = Umbraco.Media(id);
                var imagerUrl = content.Url;
                var productViewModel = new ProductViewModel
                {
                    Sku = product.Sku,
                    Name = product.Name,
                    ThumbnailImageUrl = imagerUrl
                };
                
                productViews.Add(productViewModel);
            }

            return productViews;
        }
    }
}