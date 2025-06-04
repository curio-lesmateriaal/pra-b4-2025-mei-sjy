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
    public class SearchController
    {

        public static Home Window { get; set; }

        public void Start()
        {
            // Geen opstartlogica nodig
        }

        public void SearchButtonClick()

        {
            string input = SearchManager.GetSearchInput();
            if (string.IsNullOrEmpty(input)) return;

            string photosDir = "fotos";

            if (!Directory.Exists(photosDir)) return;



            var files = Directory.GetFiles(photosDir);

            foreach (string file in files)
            {
                if (file.Contains(input))
                {
                    SearchManager.SetPicture(file);
                    break;
                }
            }
        }
    }
}
