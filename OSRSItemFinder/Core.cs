using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;



namespace OSRSItemFinder
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    static class ItemHandler
    {
        static string itemUrl = "http://services.runescape.com/m=itemdb_oldschool/api/catalogue/detail.json?item=";
        static public string jsonUrl = "items.json";
        static public List<Items> result;

        // Checking if item file exists
        public static bool CheckFile()
        {
            try
            {
                string text = File.ReadAllText(jsonUrl);
                result= JsonConvert.DeserializeObject<List<Items>>(text);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }

        }
        
        public static dynamic GetItemInfo(int itemid, char k)
        {
            
            var response = new WebClient().DownloadString(itemUrl+itemid);
            dynamic response_json = JsonConvert.DeserializeObject<dynamic>(response);

            
            switch (k)
            {
                case 'p':
                    return response_json["item"]["current"]["price"] + " GP";
                case 'i':
                    return response_json["item"]["icon"];
                case 'm':
                    if (response_json["item"]["members"] == "false"){return false;}
                    else { return true; }
                default:
                    return null;
                   
            }
        }

        public static Dictionary<int,string> SelectItem(string itemsel)
        {
            Dictionary<int, string> pal = new Dictionary<int, string>();
            foreach (Items item in result)
            {
                if (item.Name.ToLower().Contains(itemsel.ToLower()))
                {
                    pal.Add(item.Id, item.Name);
                }
                
            }

            return pal;

        }

    }
}

