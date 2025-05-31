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

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen: \n");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = 2.55 });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 15x20", Price = 4.00 });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto sleutelhanger", Price = 7.00 });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto Mok", Price = 9.33 });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto T-Shirt", Price = 12.69 });

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
            KioskProduct selectedProduct = ShopManager.GetSelectedProduct();
            int? fotoId = ShopManager.GetFotoId();
            int? amount = ShopManager.GetAmount();

            if (selectedProduct == null)
            {
                MessageBox.Show("Selecteer een product");
                return;
            }

            if (fotoId == null)
            {
                MessageBox.Show("Vul een ID in");
                return;
            }

            if (amount == null)
            {
                MessageBox.Show("Vul een aantal in");
                return;
            }


            double final = selectedProduct.Price * (int)amount;

            string finalString = final.ToString();

            ShopManager.SetShopReceipt("Eindbedrag\n€" + finalString);
        }


        public void SaveButtonClick()
        {
            // Vraag de huidige bon op
            string receipt = ShopManager.GetShopReceipt();

            string fileName = $"Bon_{DateTime.Now:yyyyMMdd}.txt";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                // Sla de bon op als tekstbestand
                File.WriteAllText(filePath, receipt);
                MessageBox.Show($"Bon opgeslagen als:\n{filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er is iets misgegaan bij het opslaan van de bon:\n");
            }
        }


        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {

        }
        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {
            string receipt = ShopManager.GetShopReceipt();

            string fileName = $"Bon_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                File.WriteAllText(filePath, receipt);

                MessageBox.Show($"Bon succesvol opgeslagen in:\n{filePath}", "Bon opgeslagen", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is iets misgegaan bij het opslaan van de bon:\n{ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}