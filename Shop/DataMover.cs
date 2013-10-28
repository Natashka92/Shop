using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Shop.DataBase;

namespace Shop
{
    public class DataMover
    {
        private IDictionary<string, Category> categories = new Dictionary<string, Category>(460);
        private IDictionary<string, Currency> currencies = new Dictionary<string, Currency>();
        private IDictionary<string, ParamName> paramNames = new Dictionary<string, ParamName>();
        private IDictionary<string, ParamValue> paramValues = new Dictionary<string, ParamValue>();
        private IDictionary<string, Vendor> vendors = new Dictionary<string, Vendor>();
        private IDictionary<string, VendorName> vendorNames = new Dictionary<string, VendorName>();

        private DataContext dataContext;

        public DataMover(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void LoadData(DataReader reader)
        {
            try
            {
                LoadCurrencies(reader.GetXmlItems("currency"));
                LoadCategories(reader.GetXmlItems("category"));
                LoadProducts(reader.GetXmlItems("offer"));
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName,
                                               validationError.ErrorMessage);
                    }
                }
            } 
        }

        public void LoadCurrencies(IEnumerable<XElement> items)
        {
            foreach (var item in items)
            {
                var name = item.Attribute("id").Value;
                var rate = (double)item.Attribute("rate");
                var currency = new Currency()
                {
                    Name = name,
                    Rate = rate
                };
                currencies[name] = currency;
                dataContext.Currencies.Add(currency);
            }
            dataContext.SaveChanges();
            Console.WriteLine("Commit currencies.");            
        }

        private void LoadCategories(IEnumerable<XElement> items)
        { 
            foreach(var item in items)
            {
                var id = item.Attribute("id").Value;
                string parentId = null;
                try
                {
                    parentId = item.Attribute("parentId").Value;
                }
                catch
                {
                    parentId = null;
                }
                var category = new Category()
                {
                    CategoryId = Convert.ToInt32(id),
                    Name = item.Value,
                    Parent = parentId == null ? null : categories[parentId]
                };
                categories[id] = category;
                dataContext.Categories.Add(category);
            }
            dataContext.SaveChanges();
            Console.WriteLine("Commit categories.");
        }

        private void LoadProducts(IEnumerable<XElement> items)
        {
            int i = 0;
            foreach (var item in items)
            {
                int id = Convert.ToInt32(item.Attribute("id").Value);
                int categoryId = Convert.ToInt32(item.Element("categoryId").Value);

                var vendorName = ReadAttribute(item, "vendor");
                if (!vendorNames.ContainsKey(vendorName))
                {
                    VendorName vendor = new VendorName() { Name = vendorName };
                    vendorNames[vendorName] = vendor;
                    dataContext.VendorNames.Add(vendor);
                }

                var vendorCode = ReadAttribute(item, "vendorCode");
                if (!vendors.ContainsKey(vendorCode))
                {
                    Vendor vendor = new Vendor() { VendorCode = vendorCode, VendorName = vendorNames[vendorName] };
                    vendors[vendorCode] = vendor;
                    dataContext.Vendors.Add(vendor);
                }

                var listPicture = new List<Picture>();
                foreach (XElement pic in item.Elements("picture"))
                {
                    Picture picture = new Picture
                    {
                        PictureUrl = pic.Value,
                        ProductId = id
                    };
                    dataContext.Pictures.Add(picture);
                    listPicture.Add(picture);
                }

                var product = new Product()
                {
                    ProductId = id,
                    Barcode = item.GetStringValue("barcode"),
                    CategoryId = categoryId,
                    Currency = item.GetEntity("currencyId", currencies),
                    Description = item.GetStringValue("description"),
                    Discount = 0.0,
                    Name = item.GetStringValue("name"),
                    Price = Convert.ToDouble(item.Attribute("price")),
                    Url = item.GetStringValue("url"),
                    Vendor = vendors[vendorCode],
                    Picture = listPicture
                };
                dataContext.Products.Add(product);
                dataContext.SaveChanges();

                foreach (XElement element in item.Elements("param"))
                {
                    var paramName = element.Attribute("name").Value;
                    if (!paramNames.ContainsKey(paramName))
                    {
                        ParamName param = new ParamName() { Name = paramName };
                        dataContext.ParamNames.Add(param);
                        dataContext.SaveChanges();
                        paramNames[paramName] = dataContext.ParamNames.First(x => x.Name == paramName);
                    }
                    var paramValue = element.Value;
                    if (!paramValues.ContainsKey(paramValue))
                    {
                        ParamValue param = new ParamValue() { Name = paramValue };
                        dataContext.ParamValues.Add(param);
                        dataContext.SaveChanges();
                        paramValues[paramValue] = dataContext.ParamValues.First(x => x.Name == paramValue);
                    }

                    dataContext.Parameters.Add(new Parameter()
                    {
                        ParamNameId = paramValues[paramValue].ParamValueId,
                        ParamValueId = paramNames[paramName].ParamNameId,
                        ProductId = id
                    });
                }
                dataContext.SaveChanges();
                if (++i % 100 != 0)
                {
                    continue;
                }                
                Console.WriteLine("Commit books: {0}", i);
            }
            dataContext.SaveChanges();
        }

        private string ReadAttribute(XElement el, String s)
        {
            String result;
            try
            {
                result = el.Element(s).Value;
            }
            catch
            {
                result = "-";
            }
            return result;
        }
    }
}
