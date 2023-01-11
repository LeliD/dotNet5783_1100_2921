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
        readonly string s_Products = "Products";
        public int Add(Product product)
        {
            List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_Products);
            if (listProducts.Exists(x => x?.ID == product.ID))
                throw new DalAlreadyExistIdException(product.ID, "Product");
            listProducts.Add(product);
            XMLTools.SaveListToXMLSerializer(listProducts, s_Products);
            return product.ID;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }
    }
}
