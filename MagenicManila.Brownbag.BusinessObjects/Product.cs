using Csla;
using Csla.Data;
using MagenicManila.Brownbag.BusinessObjects.Contracts;
using MagenicManila.Brownbag.BusinessObjects.Core;
using MagenicManila.Brownbag.Core;
using System;
using System.Linq;

namespace MagenicManila.Brownbag.BusinessObjects
{
    [Serializable]
    internal sealed class Product
        : BusinessBaseCore<Product>, IProduct
    {
        private void DataPortal_Fetch(int id)
        {
            var product = this.Context.Products.Single(_ => _.Id == id);
            using (this.BypassPropertyChecks)
            {
                DataMapper.Map(product, this);
            }
        }

        public static readonly PropertyInfo<int?> IdProperty =
            PropertyInfoRegistration.Register<Product, int?>(_ => _.Id);
        public int? Id
        {
            get { return this.ReadProperty(Product.IdProperty); }
            private set { this.LoadProperty(Product.IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty =
            PropertyInfoRegistration.Register<Product, string>(_ => _.Name);
        public string Name
        {
            get { return this.GetProperty(Product.NameProperty); }
            set { this.SetProperty(Product.NameProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty =
            PropertyInfoRegistration.Register<Product, string>(_ => _.Description);
        public string Description
        {
            get { return this.GetProperty(Product.DescriptionProperty); }
            set { this.SetProperty(Product.DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<string> BrandProperty =
            PropertyInfoRegistration.Register<Product, string>(_ => _.Brand);
        public string Brand
        {
            get { return this.GetProperty(Product.BrandProperty); }
            set { this.SetProperty(Product.BrandProperty, value); }
        }

        public static readonly PropertyInfo<decimal> PriceProperty =
            PropertyInfoRegistration.Register<Product, decimal>(_ => _.Price);
        public decimal Price
        {
            get { return this.GetProperty(Product.PriceProperty); }
            set { this.SetProperty(Product.PriceProperty, value); }
        }

        public static readonly PropertyInfo<string> SKUProperty =
            PropertyInfoRegistration.Register<Product, string>(_ => _.SKU);
        public string SKU
        {
            get { return this.GetProperty(Product.SKUProperty); }
            set { this.SetProperty(Product.SKUProperty, value); }
        }
    }
}
