using BusinessLayer;
using System.Collections.Generic;

namespace DAL
{
    public interface IDB
    {
        Product CreateProduct(string productname);
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts();
    }
}