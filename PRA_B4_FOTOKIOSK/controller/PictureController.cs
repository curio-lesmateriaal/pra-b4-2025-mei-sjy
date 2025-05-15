using Microsoft.VisualBasic;
using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            // Initializeer de lijst met fotos
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                if (!(dir.Split("\\")[1].StartsWith(day.ToString()))) continue;
                
                foreach (string file in Directory.GetFiles(dir))
                {
                    /**
                     * file string is de file van de foto. Bijvoorbeeld:
                     * \fotos\0_Zondag\10_05_30_id8824.jpg
                     */


                    var fileDate = DateTime.Parse(file.Split("\\")[2].Split("_id")[0].Replace("_", ":"));
                    if (fileDate >= now.AddMinutes(-30) && fileDate <= now.AddMinutes(-2))
                    {
                        PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });

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
