using LINQSamples.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQSamples
{
    internal class sqlquerys
    {
        class CustomerModel
        {
            public CustomerModel()
            {
                Orders = new List<OrderModel>();
            }
            public string CustomerId { get; set; }
            public string CustomerName { get; set; }
            public int OrderCount { get; set; }
            public List<OrderModel> Orders { get; set; }
        }
        class OrderModel
        {
            public int OrderId { get; set; }
            public decimal Total { get; set; }
            public List<ProductModel> Products { get; set; }
        }
        class ProductModel
        {
            public int ProductId { get; set; }
            public string Name { get; set; }
            public decimal? Price { get; set; }
            public int Quantity { get; set; }
            static void Main(string[] args)
            {
                using (var db = new NorthWindContext())
                {
                    //müşterilerin verdiği toplam sipariş

                    //var customers = db.Customers.Where(Cus => Cus.Orders.Count() == 0).Select(Cus => new
                    //var customers = db.Customers
                    //    .Where(Cus => Cus.CustomerId == "PERIC")
                    //    .Select(Cus => new
                    //    {
                    //        CustomerId = Cus.CustomerId,
                    //        OrderCount = Cus.Orders.Count(),
                    //        CustomerName = Cus.ContactName,
                    //        Orders = Cus.Orders.Select(order => new OrderModel
                    //        {
                    //            OrderId = order.OrderId,
                    //            Total = order.OrderDetails.Sum(od => od.Quantity * od.UnitPrice),

                    //            Products = order.OrderDetails.Select(od => new ProductModel
                    //            {
                    //                ProductId = od.ProductId,
                    //                Name = od.Product.ProductName,
                    //                Price = od.Product.UnitPrice,
                    //                Quantity = od.Quantity
                    //            }).ToList()
                    //        }).ToList()
                    //    }).OrderBy(i => i.OrderCount).ToList();

                    //foreach (var customer in customers)
                    //{
                    //    Console.WriteLine(customer.CustomerId + "-" +
                    //        customer.CustomerName + " " + customer.OrderCount);
                    //    foreach (var order in customer.Orders)
                    //    {
                    //        Console.WriteLine("Siparişler");
                    //        Console.WriteLine(order.OrderId + "=>" + order.Total);
                    //        foreach (var product in order.Products)
                    //        {
                    //            Console.WriteLine("**********************");
                    //            Console.WriteLine(product.ProductId + "Urun Ismi=>" + product.Name + "Urun ucreti =>" + product.Price + "adet =>" + product.Quantity);
                    //        }
                    //    }
                    //}

                    int id = 3;

                    var entity = db.Categories.Where(s => s.CategoryId == id)
                                .Select(p => new AdminCategoryEditViewModel
                                {
                                    CategoryName = p.CategoryName,
                                    CategoryId = p.CategoryId,
                                    Products = db.Products.Where(s => s.CategoryId == id).Select(p => new ProductView
                                    {
                                        ProductId = p.ProductId,
                                        ProductName = p.ProductName

                                    })

                                }).ToList();
                    if (entity == null)
                    {
                        return NotFound();
                    }
                    return View(entity);


                }
            }

            private static void coklutabloilecalisma(NorthWindContext db)
            {

                //var categories = db.Categories.Where(c=>c.Products.Count() > 0 ).ToList();
                //en az 1 ürünü olan kategorileri getirir
                //var categories = db.Categories.Where(c=>c.Products.Any()).ToList();
                //en az 1 ürünü olan kategorileri getirir
                //var products = db.Products.Select(p =>
                //new 
                //{
                //    supplierName = p.Supplier.CompanyName,
                //    contactName = p.Supplier.ContactName,
                //    p.ProductName
                //}).ToList();

                //EXTENSİON METHODS
                //QUERY EXPRESSİONS

                //}

                //var products = (from p in db.Products
                //                where p.UnitPrice>10
                //                select p).ToList();//db.Products.where(p=>p.UnitPrice>10).ToList();

                var products = (from p in db.Products
                                join s in db.Suppliers on p.SupplierId
                                equals s.SupplierId
                                select new
                                {
                                    p.ProductName,
                                    ContactName = s.ContactName,
                                    companyName = s.CompanyName

                                }).ToList();


                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + " " + product.ContactName + " "
                        + product.companyName);



                }
            }

            private static void aynisorgudaikitablodanvericekme(NorthWindContext db)
            {
                //var products = db.Products.Where(p => p.CategoryId == 1).ToList();//categoryId si 1 olanları çık

                //var products = db.Products.Include(p => p.Category).Where(p =>
                //p.Category.CategoryName == "Beverages").ToList();//product tablosuyla ilişkili category tablosundaki bir kayıdın
                //categoryName ile filtrelemesi yapılıp id si çekiliyor.ve include işlemi işe category tablosu
                //tümüyle sorguya import edilebiliyor
                //------------*-*-*-*-**-*-*-*-*-*-*-*-*-*-
                var products = db.Products.Where(p => p.Category.CategoryName == "Beverages")
                    .Select(p => new
                    {
                        name = p.ProductName,
                        Id = p.CategoryId,
                        categoryName = p.Category.CategoryName
                    })

                    .ToList();



                foreach (var product in products)
                {

                    Console.WriteLine(product.name + " " + product.Id + " " + product.categoryName);

                }
                //iki ayrı tablodan aynı sorguda veri çekilmektedir.
                //- *-*-*-*-*-*-*-*-*-*-**-*-*-*-*-*-*-*-*
            }

            private static void veriguncelleme(NorthWindContext db)
            {
                //--*-----------*---------*-----------*----------*-------
                var p = new Product() { ProductId = 1 };//arka planda select sorgusu yapmadan güncelleme yapar
                db.Products.Attach(p);
                p.UnitPrice = 50;
                db.SaveChanges();
                //----*---------*---------*-----------*--------*----------*-------
                //----------*-----------*-------------*-----------*----------*---------
                //var products = db.Products.Find(1);

                //if (products != null)
                //{
                //    products.UnitPrice = 28;
                //    db.Update(products);
                //    db.SaveChanges();
                //}
            }

            private static void ilkbestensonrakibes(NorthWindContext db)
            {
                var products = db.Products.Skip(5).Take(5).Select(p => new { p.ProductName, p.UnitPrice }).ToList();
                //ilk 5 kaydı atla sonraki 5 kaydı çek

                //var products = db.Products.Take(5).Select(p => new { p.ProductName, p.UnitPrice }).ToList();
                //ilk 5 kaydı çek
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + "-" + product.UnitPrice);
                }
                Console.ReadLine();
            }

            private static void kosullufiltreleme(NorthWindContext db)
            {
                var products = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).Where(p => p.UnitPrice > 18).ToList();
                //unitprice sütunundaki değerlerin 18 den yüksek olanları çektik

                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductName + "-" + product.UnitPrice);
                }
                Console.ReadLine();

                //var products = db.Products.Select(p => new { p.ProductName, p.UnitPrice }).Where(p => p.UnitPrice > 18

                //&& p.UnitPrice<30).ToList();

                //var products = db.Products.Where(p => p.CategoryId >= 1 && p.CategoryId <= 5).ToList();//kategori id si 1 ile 5 arasındakiler seçildi

                //var products = db.Products.Where(p => p.CategoryId == 1).Select(p => new { p.ProductName, p.UnitPrice }).ToList();


            }

            private static void Verisilme(NorthWindContext db)
            {
                var p = db.OrderDetails.Find(10248, 11);
                if (p != null)
                {
                    db.OrderDetails.Remove(p);
                    db.SaveChanges();
                    //product tablosundaki verilerin silinmemesi orderdetails taki ilişkili olduğu
                    //sütunlarla alakalı. orderdetailstaki kompozit primaty olarak tanımlanmış iki sütunu
                    //aynı anda belirtmediğin sürece orderdetailsta silinmiyor. orda silinmeyince
                    //product tablosundada silinmiyor.

                    //var p = new Product() { ProductId = 34 };

                    //db.Entry(p).State = EntityState.Deleted;

                    //db.Products.Remove(p);//entitystate(alternatif) ile başlattığımız trackingi arka planda otomatik çalıştırır

                    //db.SaveChanges();//select sorgusu yapılmadan tracking başlatarak direkt deleted yapabliyoruz

                    //----BİRDEN FAZLA VERİYİ AYNI ANDA SİLME
                    //var p1 = new Product() { ProductId = 34 };
                    //var p2 = new Product() { ProductId = 33 };
                    //var products = new List<Product> {  p1, p2 };

                    //db.RemoveRange(products);
                    //db.SaveChanges();



                }
            }

            private static void karisik(NorthWindContext db)
            {





                //var products = db.Customers.ToList();//İLGİLİ TABLODAKİ TÜM BİLGİLERİ ÇEK


                //var products = db.Employees.Select(p=> new {Fullname = p.FirstName+" "+p.LastName}).ToList();
                //çalışanların ismini ve soyismini tek kolon olarak çek

                //var result = db.Products.Count();// products tablosundaki elemanların sayısını çek

                //var result = db.Products.Count(i=> i.UnitPrice > 10 && i.UnitPrice < 30);//fiyatı 10 ile 30 arasındaki ürünleri çek

                // var result = db.Products.Count(i=> i.Discontinued); // satışta olan ürünleri çek

                //var result = db.Products.Min(i=> i.UnitPrice);// minimum fiyatlı kaydı çek

                //var result = db.Products.Max(i=> i.UnitPrice); // maksimum fiyatlı ürünü çek 

                //var result = db.Products.Where(i=> i.CategoryId==1).Max(i=> i.UnitPrice); // 1 numaralı kategorinin en yüksek fiyatını çek

                //var result = db.Products.Where(i=> i.Discontinued == false).Average(i=> i.UnitPrice); // satışta olan ürünlerin ortalama fiyatını çek

                //var result = db.Products.Average(i=> i.UnitPrice); // ürünlerin ortalama fiyatını çek

                //var result = db.Products.Where(i=> i.Discontinued == false).Sum(i=> i.UnitPrice); //satışta olan ürünlerin fiyatlarının toplamını çek


                //var result = db.Products.OrderBy(p=>p.UnitPrice).ToList();// fiyata göre artan bir sıralama ile çek

                //var result = db.Products.OrderByDescending(p=>p.UnitPrice).ToList();// fiyata göre azalan bir sıralama ile çek

                //var result = db.Products.OrderByDescending(p=>p.UnitPrice).FirstOrDefault();//en pahalı olan ilk ürünü çek

                //var result = db.Products.OrderByDescending(p=>p.UnitPrice).LastOrDefault();//en pahalı olan son ürünü çek

                //Console.WriteLine(result.ProductName + " " + result.UnitPrice);


                //foreach (var product in result)
                //{
                //    Console.WriteLine(product.ProductName + " " + product.UnitPrice);
                //}
                //Console.WriteLine(result);


                //change tracking ** ekleme güncelleme gibi işlemlerde changetracking rapor tutar ve veri tabanına bu işlemi gönderir

                //var products = db.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == 1);
                // select gibi sorgularında kullanabilir. güncelleme ekleme gibi sorguları kapatır. TRACKİNGİ KAPATIR.


                //--------*-**------------------*-**-----------*-*-----------*-*-
                //var products = db.Products.FirstOrDefault(p => p.ProductId == 1);

                //products.UnitPrice = 10;//arka planda select sorgusu yaparak güncelleme işlemi yapar
                //db.SaveChanges();
                //-----*------*----------*--------*------*------*-------*----



                //foreach (var product in products)
                //{
                //    Console.WriteLine(product.Name);
                //    Console.WriteLine(product.Price);
                //}
            }
        }

    }
}
