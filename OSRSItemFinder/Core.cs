using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;



namespace OSRSItemFinder
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal class ItemHandler
    {
        string itemUrl = "http://services.runescape.com/m=itemdb_oldschool/api/catalogue/detail.json?item=";

        public dynamic Initilize()
        {
            string text = System.IO.File.ReadAllText(@"items.json");
            List<Items> result = JsonConvert.DeserializeObject<List<Items>>(text);

            return result;

        }
        
        public dynamic GetItemInfo(int itemid, char k)
        {
            
            var response = new WebClient().DownloadString(itemUrl+itemid);
            dynamic response_json = JsonConvert.DeserializeObject<dynamic>(response);

            
            switch (k)
            {
                case 'p':
                    return response_json["item"]["current"]["price"] + " GP";
                case 'i':
                    
                    return response_json["item"]["icon"];
                default:
                    return null;
                   
            }
        }

        public Dictionary<int,string> SelectItem(string itemsel)
        {
            List<Items> results = this.Initilize();
            Dictionary<int, string> pal = new Dictionary<int, string>();

            foreach (Items item in results)
            {
                pal.Add(item.Id, item.Name);
            }

            return pal;

        }

    }
}

