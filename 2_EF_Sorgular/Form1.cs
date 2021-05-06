using _2_EF_Sorgular.Data;
using System;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Windows.Forms;

namespace _2_EF_Sorgular
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Entities db = new Entities();
        private void btnSorgu1_Click(object sender, EventArgs e)
        {
            #region Soru
            //Fiyatı 20 ile 50 arasında olan ürünlerin Id ,adı,Fiyatı Miktarı ve Kategori getiren sorgu yaz....
            //Write a query that returns the Id, Name, Price, Amount and Category of the products whose price is between 20 and 50 ....
            #endregion
            #region LING To Entity
            dgvSonuclar.DataSource = db.Products
                .Where(p => p.UnitPrice > 20 && p.UnitPrice < 50)
                .OrderByDescending(p => p.UnitPrice)
                .Select(p => new
                {
                    UrunId = p.ProductID,
                    UrunAdi = p.ProductName,
                    p.UnitPrice,
                    p.UnitsInStock,
                    p.Category.CategoryName
                }).ToList();
            #endregion

            #region Ling To Sql
            var result = from p in db.Products
                         where p.UnitPrice > 20 && p.UnitPrice < 50
                         orderby p.UnitPrice descending
                         select (new
                         {
                             UrunId = p.ProductID,
                             UrunAdi = p.ProductName,
                             p.UnitPrice,
                             p.UnitsInStock,
                             p.Category.CategoryName
                         });
            dgvSonuclar.DataSource = result.ToList();

            #endregion
        }

        private void btnSorgu2_Click(object sender, EventArgs e)
        {
            #region Ling To Entity
            dgvSonuclar.DataSource = db.Orders.Select(x => new
            {
                Musteri = x.Customer.CompanyName,
                Calisan = x.Employee.FirstName + " " + x.Employee.LastName,
                SiparişId = x.OrderID,
                Tarih = x.OrderDate,
                Kargo = x.Shipper.CompanyName
            }).ToList();
            #endregion

            #region LİNG TO Sql
            var result = from x in db.Orders
                         select new
                         {
                             Musteri = x.Customer.CompanyName,
                             Calisan = x.Employee.FirstName + " " + x.Employee.LastName,
                             SiparişId = x.OrderID,
                             Tarih = x.OrderDate,
                             Kargo = x.Shipper.CompanyName
                         };
            dgvSonuclar.DataSource = result.ToList();
            #endregion

        }

        private void btnSorgu3_Click(object sender, EventArgs e)
        {
            #region Soru
            //CompanyName icerisinde "restorant" gecen muşterileri listeleyiniz
            //List the customers who have "restaurant" in Company Name
            #endregion

            #region LİNG To Entity
            //dgvSonuclar.DataSource = db.Customers
            //    .Where(x => x.CompanyName.Contains("restaurant"))
            //    .ToList();
            #endregion

            #region LİNG To Sql
            //var result = from x in db.Customers
            //             where x.CompanyName.Contains("restaurant")
            //             select x;
            //dgvSonuclar.DataSource = result.ToList();
            #endregion

            #region JOIN
            var result = from p in db.Products
                         join c in db.Categories on p.CategoryID equals c.CategoryID
                         select new
                         {
                             p.ProductName,
                             c.CategoryName
                         };
            dgvSonuclar.DataSource = result.ToList();
            #endregion


        }

        private void btnSorgu4_Click(object sender, EventArgs e)
        {
            #region Soru
            //Kategorisi Beverages olan ve ürün adı kola,fiyatı 5.50,Stok Adteti 500 olan ürünü ekleyiniz
            //Add the product whose category is Beverages and the product name is cola, the price is 5.50, the Stock Quantity is 500.
            #endregion

            #region 1.Yol
            int kategoriId = db.Categories
                .Where(x => x.CategoryName == "Beverages")
                .FirstOrDefault().CategoryID;

            Product p = new Product();
            p.ProductName = "Kola 1";
            p.UnitPrice = 5.5M;
            p.UnitsInStock = 500;
            p.CategoryID = kategoriId;
            db.Products.Add(p);
            db.SaveChanges();

            //dgvSonuclar.DataSource = db.Products.ToList().OrderByDescending(x => x.ProductID).ToList();
            #endregion

            #region 2.Yol
            //db.Products.Add(new Product
            //{
            //    ProductName = "Kola 4",
            //    UnitPrice = 5.75M,
            //    UnitsInStock = 150,
            //    CategoryID = db.Categories.FirstOrDefault(x=> x.CategoryName == "Beverages").CategoryID
            //});
            //db.SaveChanges();

            //dgvSonuclar.DataSource = db.Products.ToList().OrderByDescending(x => x.ProductID).ToList();
            #endregion

            #region 3.Yol
            //db.Categories
            //    .FirstOrDefault(x => x.CategoryName == "Beverages")
            //    .Products.Add(new Product
            //    {
            //        ProductName = "Kola 5",
            //        UnitPrice = 6.5M,
            //        UnitsInStock = 250
            //    });
            //db.SaveChanges();
            //dgvSonuclar.DataSource = db.Products.ToList().OrderByDescending(x => x.ProductID).ToList();
            #endregion

            dgvSonuclar.DataSource = db.Products.ToList().OrderByDescending(x => x.ProductID).ToList();
        }

        private void btnSorgu5_Click(object sender, EventArgs e)
        {
            #region Soru
            //Calışanlarık Adını Soyadını Dogum Tarihini ve yaşını getiren sorgu yazınız..
            //Write a query that brings the Employees' Name, Surname, Date of Birth and Age ..
            #endregion

            #region LİNG To Entity
            //dgvSonuclar.DataSource = db.Employees
            //    .Select(x => new
            //    {
            //        x.FirstName,
            //        x.LastName,
            //        x.BirthDate,
            //        Yas = SqlFunctions.DateDiff("YEAR", x.BirthDate, DateTime.Now)
            //    }).ToList();
            #endregion

            #region Ling to Sql
            var result = from x in db.Employees
                         select new
                         {
                             x.FirstName,
                             x.LastName,
                             x.BirthDate,
                             Yas = SqlFunctions.DateDiff("YEAR", x.BirthDate, DateTime.Now)
                         };
            dgvSonuclar.DataSource = result.ToList();
            #endregion
        }

        private void btnSorgu6_Click(object sender, EventArgs e)
        {
            #region LING TO Entity
            dgvSonuclar.DataSource = db.Products
                .GroupBy(x => x.Category.CategoryName)
                .Select(g => new
                {
                    KateggoriAdı = g.Key,
                    ToplamStok = g.Sum(p=> p.UnitsInStock)
                }).ToList();
            #endregion
        }
    }
}
