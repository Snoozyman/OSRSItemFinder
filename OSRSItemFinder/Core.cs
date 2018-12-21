using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Windows.Input;


namespace OSRSItemFinder
{
   
    class ItemHandler
    {
        string itemUrl = "http://services.runescape.com/m=itemdb_oldschool/api/catalogue/detail.json?item=";

        public dynamic Initilize()
        {
            string text = System.IO.File.ReadAllText(@"items.json");
            dynamic result = JsonConvert.DeserializeObject<dynamic>(text);
            
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

        public Image DownloadImageFromUrl(string imageUrl)
        {
            Image image = null;

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                WebResponse webResponse = webRequest.GetResponse();

                Stream stream = webResponse.GetResponseStream();

                image = Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception)
            {
                ;
                return null;
            }

            return image;
        }


        public Dictionary<int,string> SelectItem(string itemsel)
        {
            dynamic results = this.Initilize();
            List<string> lista_name = new List<string>();
            List<int> lista_id = new List<int>();
            Dictionary<int, string> pal= new Dictionary<int, string>();
            int pituus;

            for (int i = 0; i < results.Count; i++)
            {
                string k = results[i]["name"].ToString().ToLower();
                int p = Convert.ToInt32(results[i]["id"]);

                if (k.Contains(itemsel.ToLower()))
                {
                    pal.Add(p,k);
                    
                }
            }

            pituus = lista_name.Count;

            return pal;
    

        }

    }
}

