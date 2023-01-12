using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class DalProduct : IProduct
    {
        readonly string s_products = "Products";
        static DO.Product? createProductfromXElement(XElement s)
        {
            return new DO.Product()
            {
                ID = s.ToIntNullable("ID") ?? throw new DO.DalNullPropertyException("id"), 
                Name = (string?)s.Element("Name"),
                Price = s.ToDoubleNullable("Price")?? throw new DO.DalNullPropertyException("Price"),
                Category = s.ToEnumNullable<DO.Category>("Category")?? throw new DO.DalNullPropertyException("id"),
                InStock = s.ToIntNullable("InStock") ?? throw new DO.DalNullPropertyException("InStock"),
                ImageRelativeName = (string?)s.Element("ImageRelativeName")
             };
        }
        public int Add(Product product)
        {
            XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
            XElement? xmlProduct= productsRootElem.Elements().Where(s => s.ToIntNullable("ID") == product.ID).Select(s=>s).FirstOrDefault();
            if (xmlProduct != null)
                throw new DO.DalAlreadyExistIdException(product.ID, "product"); // fix to: throw new DalMissingIdException(id);

            XElement produuctElem = new XElement("Product",
                                       new XElement("ID", product.ID),
                                       new XElement("Name", product.Name),
                                       new XElement("Price", product.Price),
                                       new XElement("Category", product.Category),
                                       new XElement("InStock", product.InStock),
                                       new XElement("ImageRelativeName", product.ImageRelativeName)
                                       );

            productsRootElem.Add(produuctElem);
            XMLTools.SaveListToXMLElement(productsRootElem, s_products);
            return product.ID;
        }

        public void Delete(int id)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
            XElement xmlProduct = productsRootElem.Elements().Where(s => s.ToIntNullable("ID") == id).Select(s => s).FirstOrDefault()?? throw new DO.DalMissingIdException(id, "Product");
            xmlProduct.Remove(); //<==>   Remove stud from studentsRootElem

            XMLTools.SaveListToXMLElement(productsRootElem, s_products);
        }

        public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
        {
            XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
            if (filter != null)
            {
                return productsRootElem.Elements().Select(s => createProductfromXElement(s)).Where(filter);
            }
            return productsRootElem.Elements().Select(s => createProductfromXElement(s));
        }

        public Product GetById(int id)
        {
            XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
            return (from s in productsRootElem?.Elements()
                    where s.ToIntNullable("ID") == id
                    select createProductfromXElement(s)).FirstOrDefault()
                    ?? throw new DO.DalMissingIdException(id, "Product");
        }

        public void Update(Product product)
        {
            Delete(product.ID);
            Add(product);
        }
    }
}
