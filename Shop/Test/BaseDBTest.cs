using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Shop.DataBase;

namespace Shop.Test
{
    [TestFixture]
    class BaseDBTest
    {
        static protected string category1 = "Category1";
        static protected string category2 = "Category2";
        static protected string category3 = "Category3";
        static protected string category4 = "Category4";

        [Test]
        public static void AddCategory()
        {
            using (var dbContext = new DataContext("DBShop1"))
            {
                Category cat1 = new Category() { CategoryId = 1, Name = category1, Parent = null };
                Category cat2 = new Category() { CategoryId = 2, Name = category1, Parent = cat1 };
                Category cat3 = new Category() { CategoryId = 3, Name = category1, Parent = cat1 };
                dbContext.Categories.Add(cat1);
                dbContext.Categories.Add(cat2);
                dbContext.Categories.Add(cat3);
                dbContext.SaveChanges();
                Assert.AreEqual(3, dbContext.Categories.Count());
                
                dbContext.Categories.Remove(cat2);               
                dbContext.SaveChanges();
                Assert.AreEqual(2, dbContext.Categories.Count());

                dbContext.Categories.Remove(cat3);
                dbContext.SaveChanges();
                Assert.AreEqual(1, dbContext.Categories.Count());

                dbContext.Categories.Remove(cat1);
                dbContext.SaveChanges();
                Assert.AreEqual(0, dbContext.Categories.Count());
            }            
        }

        [Test]
        public static void AddCurrency()
        {
            using (var dbContext = new DataContext("DBShop1"))
            {
                string name = "RUR";
                if (dbContext.Currencies.FirstOrDefault(x => x.Name == name) == null)
                {
                    dbContext.Currencies.Add(new Currency() { Name = name, Rate = 1 });
                    dbContext.SaveChanges();
                }
                Assert.AreEqual(1, dbContext.Currencies.Count());
            }
        }

        [Test]
        public static void AddProduct()
        {
            using (var dbContext = new DataContext("DBShop1"))
            {
                Category cat1 = new Category() { CategoryId = 1, Name = category1, Parent = null };
                dbContext.Categories.Add(cat1);               
                dbContext.SaveChanges();

                int productId = 123123;
                int categoryId = 1;
                string vendorName = "Sumsung";
                VendorName vendorN = dbContext.VendorNames.FirstOrDefault(x=>x.Name == vendorName);
                if (vendorN == null)
                {
                    VendorName vendor = new VendorName() { Name = vendorName };
                    dbContext.VendorNames.Add(vendor);
                }

                var vendorCode = "GT-5000";
                Vendor vendorV = dbContext.Vendors.FirstOrDefault(x=>x.VendorCode == vendorCode);
                if (vendorV == null)
                {
                    vendorV = new Vendor() { VendorCode = vendorCode, VendorName = vendorN };
                    dbContext.Vendors.Add(vendorV);
                }

                var listPicture = new List<Picture>();
                for(int i = 0; i<3; i++)
                {
                    Picture picture = new Picture { PictureUrl = "pic" + i, ProductId = productId };
                    dbContext.Pictures.Add(picture);
                    listPicture.Add(picture);
                }

                var product = new Product()
                {
                    ProductId = productId,
                    Barcode = "barcode",
                    CategoryId = categoryId,
                    Currency = dbContext.Currencies.Find(1),
                    Description = "description",
                    Discount = 0.0,
                    Name = "name",
                    Price = 123.6,
                    Url = "url\\",
                    Vendor = vendorV,
                    Picture = listPicture
                };
                dbContext.Products.Add(product);
                dbContext.SaveChanges();

                for (int i = 0; i < 4; i++ )
                {
                    var paramName = "name" + i;
                    ParamName paramN = dbContext.ParamNames.FirstOrDefault(x => x.Name == paramName);
                    if (paramN == null)
                    {
                        paramN = new ParamName() { Name = paramName };
                        dbContext.ParamNames.Add(paramN);
                        dbContext.SaveChanges();
                    }
                    var paramValue = "Value" + i;
                    ParamValue paramV = dbContext.ParamValues.FirstOrDefault(x => x.Name == paramValue);
                    if (paramV == null)
                    {
                        paramV= new ParamValue() { Name = paramValue };
                        dbContext.ParamValues.Add(paramV);
                        dbContext.SaveChanges();
                    }

                    dbContext.Parameters.Add(new Parameter()
                    {
                        ParamNameId = paramV.ParamValueId,
                        ParamValueId = paramN.ParamNameId,
                        ProductId = productId
                    });
                }
                dbContext.SaveChanges();

                Assert.AreEqual(1, dbContext.Products.Count());
                Assert.AreEqual(3, dbContext.Pictures.Count());
                Assert.AreEqual(4, dbContext.Parameters.Count());
                Assert.AreEqual(1, dbContext.Vendors.Count());
                Assert.AreEqual(1, dbContext.VendorNames.Count());
                Assert.AreEqual(1, dbContext.Categories.Count());

                dbContext.Products.Remove(product);
                dbContext.SaveChanges();

                Assert.AreEqual(0, dbContext.Products.Count());
                Assert.AreEqual(0, dbContext.Pictures.Count());
                Assert.AreEqual(0, dbContext.Parameters.Count());
                Assert.AreEqual(1, dbContext.Categories.Count());

                dbContext.Categories.Remove(cat1);
                dbContext.SaveChanges();
                Assert.AreEqual(0, dbContext.Categories.Count());
            }
        }
    }
}
