using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineApp.Models
{
    public class Wine
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Designation { get; set; }
        public int Points { get; set; }
        public decimal Price { get; set; }
        public string Province { get; set; }
        public string Region_1 { get; set; }
        public string Region_2 { get; set; }
        public string Variety { get; set; }
        public string Winery { get; set; }

        public static List<Wine> GetWineList()
        {
            List<Wine> winelist = new List<Wine>();
            string[] winelines = File.ReadAllLines("wwwroot/wine.csv");
            string winetxt;
            char[] chars;
            string[] winedetails = new string[10];
            int index = 0;
            bool inquotes = false;
            StringBuilder detail = new StringBuilder();

            for (int i = 1; i < winelines.Length; i++)
            {
                winetxt = winelines[i];
                chars = winetxt.ToCharArray();

                for (int j = 0; j < chars.Length; j++)
                { 
                    if (chars[j] == ',' && !inquotes)
                    {
                        winedetails[index] = detail.ToString();
                        index++;
                        detail.Clear();
                    }
                    else
                    {
                        if (chars[j] == '"') inquotes = !inquotes;
                        else
                        {
                            detail.Append(chars[j]);
                        }
                    }
                }

                // Only creates a new Wine object when all properties have been parsed.
                if (index == 10)
                {
                    winelist.Add(new Wine()
                    {
                        ID = winedetails[0] == "" || winedetails[0] is null ? 0 : int.Parse(winedetails[0]),
                        Country = winedetails[1],
                        Description = winedetails[2],
                        Designation = winedetails[3],
                        Points = winedetails[4] == "" || winedetails[4] is null ? 0 : int.Parse(winedetails[4]),
                        Price = winedetails[5] == "" || winedetails[5] is null ? 0 : decimal.Parse(winedetails[5]),
                        Province = winedetails[6],
                        Region_1 = winedetails[7],
                        Region_2 = winedetails[8],
                        Variety = winedetails[9],
                        Winery = detail.ToString()
                    });

                    inquotes = false;
                    detail.Clear();
                    index = 0;
                }
            }
            return winelist;
        }
    }
}
