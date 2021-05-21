using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;

namespace WPLClassLibraryTeam13.Tools
{
    public static class ImageHelper
    {

        public static string Path { get; set; }
 


        // Het is veel eenvoudiger voor de bovenliggende folder 'WebshopFrontEndTeam13' te gebruiken. In het opzicht van het 'doorverkopen' van de webshop is
        // de folder 'images' een logischere keuze, ook al heeft dit zeker zijn nadelen.     
        public static string GetEersteDeelPad(string pad)
        {
            string padEersteDeel = "";
            //we verwijderen de string 'images' in het pad, omdat het voorkomt in zowel het eerste deel als het laatste
            padEersteDeel = pad.Replace("images", "");

            return padEersteDeel;
        }

        //Ben niet zeker van deze code

        //public static void PadExists(string pad)
        //{
            
        //    if (pad != null)
        //    {
        //        var eersteIndex = pad.IndexOf("images");
        //        var laatsteIndex = pad.LastIndexOf("images");
        //        // Als 'images' in het pad voorkomt...
        //        if (eersteIndex != -1)
        //        {
        //            {
        //                // en het komt maar één keer voor..
        //                if (eersteIndex == laatsteIndex)
        //                {
        //                    //OK Computer
        //                }
        //                else
        //                {
        //                    MessageBox.Show("We zoeken naar het woord 'images' in het pad. Als dit meer dan één keer voorkomt, mag enkel de laatste blijven staan.");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Kan de folder 'images' niet vinden, selecteer deze in het toegangsrechtenmenu. Fout in ImageHelper.PadExists");
        //        }

        //    }
            
        //}

        public static string[] GetVolledigPad(string imagesKolom, Admin admin)
        {
            //Een array van paden uit de database
            string[] images = imagesKolom.Trim().Split('|');

            //maakt een array zo groot als er images zijn
            string[] pathArray = new string[images.Length];
            {
                for (int i = 0; i < images.Length; i++)
                {
                    pathArray[i] = admin.Afbeeldingen_wpf  + images[i];

                }
                return pathArray;

            }
        }
    }
}
