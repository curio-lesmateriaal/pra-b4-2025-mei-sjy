using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

//TODO C1 Bewerk de bon door de volgende methodes te gebruiken 
//ShopManager.SetShopReceipt(“string”); 
//ShopManager.AddShopReceipt(“string”); 
//ShopManager.GetShopReceipt(); Haal de formulierdata op via: 
//ShopManager.GetSelectedProduct();
//ShopManager.GetFotoId();
//ShopManager.GetAmount();

//TODO C2 
// a

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        public static Home Window { get; set; }
        public List<(string ProductName, decimal Price, int Amount)> receiptItems = new List<(string, decimal, int)>();





        // Gebruik nu OrderedProduct i.p.v. een tuple
        public List<OrderedProduct> receiptItems = new List<OrderedProduct>();

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen: \n");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15 - ", Price = 2.55M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 15x20 - ", Price = 4.00M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto sleutelhanger - ", Price = 7.00M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto Mok - ", Price = 9.33M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto T-Shirt - ", Price = 12.69M });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto 10x15 -", Price = 2.55M, Description = "Een foto van 10x15 cm." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto 15x20 -", Price = 4.00M, Description = "Een foto van 15x20 cm." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto sleutelhanger -", Price = 7.00M, Description = "Een sleutelhanger met foto." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto Mok -", Price = 9.33M, Description = "Een mok met foto." });
            ShopManager.Products.Add(new KioskProduct { Name = "Foto T-Shirt -", Price = 12.69M, Description = "Een T-Shirt met foto." });

            foreach (KioskProduct item in ShopManager.Products)
            {
                ShopManager.AddShopPriceList(item.Name + ": €" + item.Price + "\n");
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
                receiptItems.Add(((string ProductName, decimal Price, int Amount))(product.Name, product.Price, amount.Value));
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
            decimal total = 0;
            string receipt = "";

            foreach (var item in receiptItems)
            {
                decimal itemTotal = (decimal)(item.Price * item.Amount);
                // Add the line to the receipt string
                receipt += $"{item.Amount} x {item.ProductName}: €{itemTotal}\n";
                total += itemTotal;
            }

            // Add the total line
            receipt += $"Eindbedrag\n€{total}";

            // Set the receipt
            ShopManager.SetShopReceipt(receipt);
        }

        }





        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
            receiptItems.Clear();
            // Als reset dan laat dit zien:
            ShopManager.SetShopReceipt("---");
        }





        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
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
            //Slaat bon op als receipt.txt
            string filePath = "receipt.txt";
            //Schrijft ingevulde informatie naar txt bestand
            File.WriteAllText(filePath, receipt);
            MessageBox.Show($"Bon opgeslagen naar {filePath}");
        }
    }
}
