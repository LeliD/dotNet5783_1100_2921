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
        /// <summary>
        /// name of products xml file
        /// </summary>
        readonly string s_products = "products";
        /// <summary>
        /// create DO.Product from XElement
        /// </summary>
        /// <param name="s">XElement</param>
        /// <returns>DO.Product?</returns>
        /// <exception cref="DO.DalNullPropertyException">Throw exception if property is null</exception>
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

        /// <summary>
        /// Adds product to products xml file
        /// </summary>
        /// <param name="product">product is the object which being added</param>
        /// <returns>returns Id of profuct</returns>
        /// <exception cref="DalAlreadyExistIdException">Throw exception if id of product already exists</exception>
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
        /// <summary>
        /// Deletes product from products xml file
        /// </summary>
        /// <param name="id ">id of product to delete</param>
        /// <exception cref="DalMissingIdException">Throw exception if product doesn't exist</exception>
        public void Delete(int id)
        {
            XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
            XElement xmlProduct = productsRootElem.Elements().Where(s => s.ToIntNullable("ID") == id).Select(s => s).FirstOrDefault()?? throw new DO.DalMissingIdException(id, "Product");
            xmlProduct.Remove(); //<==>   Remove stud from studentsRootElem

            XMLTools.SaveListToXMLElement(productsRootElem, s_products);
        }
        /// <summary>
        /// Gets all the products in products xml file in case no function was transfered. Otherwize, returns the filter products xml file. 
        /// </summary>
        /// <returns>return IEnumerable<Product?></returns>
        public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
        {
            XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
            if (filter != null)
            {
                return productsRootElem.Elements().Select(s => createProductfromXElement(s)).Where(filter);
            }
            return productsRootElem.Elements().Select(s => createProductfromXElement(s));
        }
        /// <summary>
        /// Gets product by its Id
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>returns the product</returns>
        /// <exception cref="DalMissingIdException">Throw exception if id of product doesn't exists</exception>
        public Product GetById(int id)
        {
            XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);
            return (from s in productsRootElem?.Elements()
                    where s.ToIntNullable("ID") == id
                    select createProductfromXElement(s)).FirstOrDefault()
                    ?? throw new DO.DalMissingIdException(id, "Product");
        }
        /// <summary>
        /// Update product that exsist in products xml file
        /// </summary>
        /// <param name="product">product is the object which being updated</param>
        /// <exception cref="DalMissingIdException">Throw exception if product doesn't exist</exception>
        public void Update(Product product)
        {
            Delete(product.ID);
            Add(product);
        }
    }
}
