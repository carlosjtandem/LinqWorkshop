using LINQ_Course.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Course
{
    public class Program
    {
        private static List<Product> GetProductList() => Products.ProductList;
        public List<Customer> GetCustomerList() => Customers.CustomerList;
        static void Main(string[] args)
        {
            Console.WriteLine("join operations!");
            Console.WriteLine("************ CrossJoinQuery! ***************");
            CrossJoinQuery();
            Console.WriteLine("************ GroupJoinQquery! ***************");
            GroupJoinQquery();
            Console.WriteLine("************ CrossGroupJoin! ***************");
            CrossGroupJoin();
            Console.WriteLine("************ LeftOuterJoin! ***************");
            LeftOuterJoin();
        }

        private static int CrossJoinQuery()
        {
            #region cross-join
            string[] categories = {
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category
                    select (Category: c, p.ProductName);

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
            #endregion
            return 0;
        }
        private static int GroupJoinQquery()
        {
            #region group-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    select (Category: c, Products: ps);

            foreach (var v in q)
            {
                Console.WriteLine(v.Category + ":");
                foreach (var p in v.Products)
                {
                    Console.WriteLine("   " + p.ProductName);
                }
            }
            #endregion
            return 0;
        }

        private static int CrossGroupJoin()
        {
            #region cross-group-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    from p in ps
                    select (Category: c, p.ProductName);

            foreach (var v in q)
            {
                Console.WriteLine(v.ProductName + ": " + v.Category);
            }
            #endregion
            return 0;
        }

        private static int LeftOuterJoin()
        {
            #region left-outer-join
            string[] categories = {
                "Beverages",
                "Condiments",
                "Vegetables",
                "Dairy Products",
                "Seafood"
            };

            List<Product> products = GetProductList();

            var q = from c in categories
                    join p in products on c equals p.Category into ps
                    from p in ps.DefaultIfEmpty()
                    select (Category: c, ProductName: p == null ? "(No products)" : p.ProductName);

            foreach (var v in q)
            {
                Console.WriteLine($"{v.ProductName}: {v.Category}");
            }
            #endregion
            return 0;
        }
    }
}
