using Microsoft.VisualBasic;
using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }


        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                if (!(dir.Split("\\")[1].StartsWith(day.ToString()))) continue; // check juiste dag

                foreach (string file in Directory.GetFiles(dir))
                {
                    var fileDate = DateTime.Parse(file.Split("\\")[2].Split("_id")[0].Replace("_", ":")); // van bestand naar datetime
                    if (fileDate >= now.AddMinutes(-30) && fileDate <= now.AddMinutes(-2)) // tussen 2 en 30 minuten geleden
                    {

                        bool added = false;
                        foreach (KioskPhoto photo in PicturesToDisplay)
                        {

                            var photoDate = DateTime.Parse(photo.Source.Split("\\")[2].Split("_id")[0].Replace("_", ":")); // van foto naar datetime
                            if (photoDate.AddSeconds(60) != fileDate) continue; // check of het precies 60 verschil is

                            int index = PicturesToDisplay.IndexOf(photo);
                            PicturesToDisplay.Insert(index, new KioskPhoto() { Id = 0, Source = file });
                            added = true;
                            break;
                        }

                        if (!added)
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });

                        }

                    }





                }
                

            }
            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

       

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {

        }

    }
}
