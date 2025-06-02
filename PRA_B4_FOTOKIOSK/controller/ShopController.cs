using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {

        public static Home Window { get; set; }
        public List<(string ProductName, double Price, int Amount)> receiptItems = new List<(string, double, int)>();

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen: \n");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15 -", Price = 2.55M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 15x20 -", Price = 4.00M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto sleutelhanger -", Price = 7.00M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto Mok -", Price = 9.33M });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto T-Shirt -", Price = 12.69M });

            foreach (KioskProduct item in ShopManager.Products)
            {
                ShopManager.AddShopPriceList(item.Name + ": €" + item.Price + "\n");
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
                receiptItems.Add(((string ProductName, double Price, int Amount))(product.Name, product.Price, amount.Value));
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
                double itemTotal = item.Price * item.Amount;
                receiptBuilder.AppendLine($"{item.Amount} x {item.ProductName}: €{itemTotal:F2}");
                totalAmount += itemTotal;
            }

            receiptBuilder.AppendLine($"Eindbedrag\n€{totalAmount:F2}");
            ShopManager.SetShopReceipt(receiptBuilder.ToString());
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {
            receiptItems.Clear();
            // Als reset dan laat dit zien:
            ShopManager.SetShopReceipt("\n---\n");
        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            string receipt = ShopManager.GetShopReceipt();
            string filePath = "receipt.txt";
            File.WriteAllText(filePath, receipt);
            MessageBox.Show($"Bon opgeslagen naar {filePath}");

        }
    }
}
