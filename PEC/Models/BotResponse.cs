using Newtonsoft.Json;
using System.Collections.Generic;
namespace API_Clinica.Model
{
    // R E Q U E S T    C L A S
    public class Credentials_Response
    {

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]

        public string Text { get; set; }
        public Attachments Attachments { get; set; }
        public Item Item { get; set; }


    }

    public class Attachments
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Item
    {
        public int number { get; set; }
        public string text { get; set; }
        public Callback callback { get; set; }
    }

    public class Callback
    {
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        public Data2 Data { get; set; }

    }

    public class Data2
    {
        [JsonProperty("example")]
        public string Example { get; set; }


    }

    public class Attachment
    {
        public string position { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

   
    public class Root
    {
        public string type { get; set; }
        public string text { get; set; }
        public List<Attachment> attachments { get; set; }
        //public List<Item> items { get; set; }
    }
}
