using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    internal class DalProduct : IProduct
    {
        readonly string s_products = "Products";
        public int Add(Product product)
        {
            List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            if (productsList.Exists(x => x?.ID == product.ID))
                throw new DalAlreadyExistIdException(product.ID, "Product");
            productsList.Add(product);
            XMLTools.SaveListToXMLSerializer(productsList, s_products);
            return product.ID;
        }

        public void Delete(int id)
        {
            List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            if (!productsList.Exists(x => x?.ID == id))
            {
                throw new DalMissingIdException(id, "Product");
            }
            productsList.RemoveAll(x => x?.ID == id);
            XMLTools.SaveListToXMLSerializer(productsList, s_products);
        }

        public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
        {
            List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            if (filter != null)
            {
                return from item in productsList
                       where filter(item)
                       select item;
            }
            return from item in productsList
                   select item;
        }

        public Product GetById(int id)
        {
            List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            Product product = listProducts.Find(x => x?.ID == id) ?? throw new DalMissingIdException(id, "Product");
            return product;
        }

        public void Update(Product product)
        {
            List<DO.Product?> productsList = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
            if (!productsList.Exists(x => x?.ID == product.ID))
            {
                throw new DalMissingIdException(product.ID, "Product");
            }
            productsList.RemoveAll(x => x?.ID == product.ID);
            productsList.Add(product);
            XMLTools.SaveListToXMLSerializer(productsList, s_products);
        }
    }
}
