using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web.Models;

namespace DemoStore_uCommerce.Models
{
    public class FlowerListViewModel
    {
        public string[] Categories { get; set; }
        public string Description { get; set; }
        public int MaxPrice { get; set; }
        public int TopRows { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
    }
}