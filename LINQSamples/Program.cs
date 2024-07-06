using LINQSamples.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQSamples
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }


    }
    internal class Program
    {
        static void Main(string[] args)
        {

            using (var db = new CustomNorthwindContext())
            {
                //var sonuc = db.Database.ExecuteSqlRaw("update products set unitprice=unitprice*1.2 where categoryId=4");
                //var query = "4";
                //var product = db.Products.FromSqlRaw($"select ProductName from products where categoryId={query}").ToList();
                //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-

                var products = db.ProductModels.FromSqlRaw("select ProductId, ProductName, UnitPrice from Products ").ToList();

                
                
                foreach (var item in products)
                {
                    Console.WriteLine(item.Name + "-" + item.Price);
                }
            }
            Console.ReadLine();
        }
    }
}