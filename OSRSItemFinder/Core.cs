using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace OSRSItemFinder
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class ItemHandler
    {
        readonly string itemUrl = "http://services.runescape.com/m=itemdb_oldschool/api/catalogue/detail.json?item=";
        public string jsonUrl = "items.json";
        public List<Items> result;

        // Checking if item file exists
        public bool CheckFile()
        {
            if (File.Exists(jsonUrl))
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }
        public bool LoadFile()
        {
            try {
                string text = File.ReadAllText(jsonUrl);
                result = JsonConvert.DeserializeObject<List<Items>>(text);
                MessageBox.Show("Loaded file");
                return true;

            }
            catch (JsonSerializationException)
            {
                MessageBox.Show("JSON file has incorrect syntax");
                return false;
            }
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
                case 'm':
                    if (response_json["item"]["members"] == "false"){return false;}
                    else { return true; }
                default:
                    return null;
                   
            }
        }

        public Dictionary<int,string> SelectItem(string itemsel)
        {
            Dictionary<int, string> pal = new Dictionary<int, string>();
            
            foreach(Items item in result)
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

