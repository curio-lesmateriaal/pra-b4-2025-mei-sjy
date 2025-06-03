using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        public static Home Window { get; set; }

        // Gebruik nu OrderedProduct i.p.v. een tuple
        public List<OrderedProduct> receiptItems = new List<OrderedProduct>();

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant
            ShopManager.SetShopPriceList("Prijzen: \n");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            ShopManager.Products.Add(new KioskProduct { Name = "Foto 10x15 -", Price = 2.55M, Description = "Een foto van 10x15 cm." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto 15x20 -", Price = 4.00M, Description = "Een foto van 15x20 cm." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto sleutelhanger -", Price = 7.00M, Description = "Een sleutelhanger met foto." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto Mok -", Price = 9.33M, Description = "Een mok met foto." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto T-Shirt -", Price = 12.69M, Description = "Een T-Shirt met foto." });

            foreach (KioskProduct item in ShopManager.Products)
            {
                ShopManager.AddShopPriceList(item.Name + ": €" + item.Price + "\n" + item.Description + "\n");
            }

            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
        }

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            var product = ShopManager.GetSelectedProduct();
            var fotoID = ShopManager.GetFotoId();
            var amount = ShopManager.GetAmount();

            if (product != null && fotoID != null && amount != null)
            {
                double total = (double)(amount.Value * product.Price);
                receiptItems.Add(new OrderedProduct
                {
                    FotoId = fotoID.Value,
                    ProductName = product.Name,
                    Amount = amount.Value,
                    TotalPrice = total
                });

                UpdateReceipt();
            }
            else
            {
                MessageBox.Show("Controleer of alle velden correct zijn ingevuld.");
            }
        }

        public void UpdateReceipt()
        {
            StringBuilder receiptBuilder = new StringBuilder();
            double totalAmount = 0;

            foreach (var item in receiptItems)
            {
                receiptBuilder.AppendLine($"{item.Amount} x {item.ProductName} (Foto ID: {item.FotoId}) - €{item.TotalPrice:F2}");
                totalAmount += item.TotalPrice;
            }

            receiptBuilder.AppendLine($"\nEindbedrag\n€{totalAmount:F2}");
            ShopManager.SetShopReceipt(receiptBuilder.ToString());
        }

        // Reset de bon
        public void ResetButtonClick()
        {
            receiptItems.Clear();
            ShopManager.SetShopReceipt("\n---\n");
        }

        // Sla bon op
        public void SaveButtonClick()
        {
            string receipt = ShopManager.GetShopReceipt();
            string filePath = "receipt.txt";
            File.WriteAllText(filePath, receipt);
            MessageBox.Show($"Bon opgeslagen naar {filePath}");
        }
    }
}
