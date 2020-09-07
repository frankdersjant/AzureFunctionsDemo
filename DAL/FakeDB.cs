using BusinessLayer;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class FakeDB : IDB
    {
        private List<Product> products;

        public FakeDB()
        {
            products = new List<Product>();
            products.Add(new Product { Id = 1, ProductName = "Ducati 916" });
            products.Add(new Product { Id = 2, ProductName = "Benelli TNT Cafe racer" });
        }

        public IEnumerable<Product> GetProducts()
        {
            return products.AsEnumerable();
        }

        public Product GetProductById(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public Product CreateProduct(string productname)
        {
            var newProduct = new Product() { Id = products.Count + 1, ProductName = productname };
            products.Add(newProduct);
            return newProduct;
        }
    }
}
