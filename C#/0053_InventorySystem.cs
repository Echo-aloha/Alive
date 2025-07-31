// 一个简易的库存管理系统，包含商品类、库存类以及基本的添加、删除和查询功能
using System;
using System.Collections.Generic;

namespace InventoryApp
{
    // 商品类
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Name} - Qty: {Quantity}";
        }
    }

    // 库存管理类
    public class Inventory
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            var existing = products.Find(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Quantity += product.Quantity;
            }
            else
            {
                products.Add(product);
            }
        }

        public bool RemoveProduct(int productId, int quantity)
        {
            var product = products.Find(p => p.Id == productId);
            if (product != null && product.Quantity >= quantity)
            {
                product.Quantity -= quantity;
                if (product.Quantity == 0)
                    products.Remove(product);
                return true;
            }
            return false;
        }

        public Product FindProduct(int productId)
        {
            return products.Find(p => p.Id == productId);
        }

        public void PrintInventory()
        {
            Console.WriteLine("\n=== Inventory ===");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
    }

    // 主程序类
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            inventory.AddProduct(new Product(1, "Apple", 100));
            inventory.AddProduct(new Product(2, "Banana", 50));
            inventory.AddProduct(new Product(1, "Apple", 50)); // 增加现有商品数量

            inventory.PrintInventory();

            inventory.RemoveProduct(1, 30); // 减少 Apple 数量
            inventory.PrintInventory();

            var product = inventory.FindProduct(2);
            if (product != null)
                Console.WriteLine($"\nFound product: {product}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
