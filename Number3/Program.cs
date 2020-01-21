using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace TaskNumber3
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"[{
                    ""inventory_id"": 9382,
                    ""name"": ""Brown Chair"",
                    ""type"": ""furniture"",
                    ""tags"": [
                        ""chair"",
                        ""furniture"",
                        ""brown""
                    ],
                    ""purchased_at"": 1579190471,
                    ""placement"": {
                        ""room_id"": 3,
                        ""name"": ""Sangkuriang""
                    }
                    },
                    {
                    ""inventory_id"": 9380,
                    ""name"": ""Big Desk"",
                    ""type"": ""furniture"",
                    ""tags"": [
                        ""desk"",
                        ""furniture"",
                        ""brown""
                    ],
                    ""purchased_at"": 1579190642,
                    ""placement"": {
                    ""room_id"": 3,
                    ""name"": ""Sangkuriang""
                    }
                    },
                    {
                    ""inventory_id"": 2932,
                    ""name"": ""LG Monitor 50 inch"",
                    ""type"": ""electronic"",
                    ""tags"": [
                        ""monitor""
                    ],
                    ""purchased_at"": 1579017842,
                    ""placement"": {
                        ""room_id"": 3,
                        ""name"": ""Sangkuriang""
                    }
                    },
                    {
                    ""inventory_id"": 232,
                    ""name"": ""Sharp Pendingin Ruangan 2PK"",
                    ""type"": ""electronic"",
                    ""tags"": [
                        ""ac""
                    ],
                    ""purchased_at"": 1578931442,
                    ""placement"": {
                        ""room_id"": 5,
                        ""name"": ""Dhanapala""
                    }
                    },
                    {
                    ""inventory_id"": 9382,
                    ""name"": ""Alat Makan"",
                    ""type"": ""tableware"",
                    ""tags"": [
                        ""spoon"",
                        ""fork"",
                        ""tableware""
                    ],
                    ""purchased_at"": 1578672242,
                    ""placement"": {
                    ""room_id"": 10,
                    ""name"": ""Rajawali""
                    }
                    }
                    ]";


            var user = JsonConvert.DeserializeObject<List<Items>>(json);    

            Console.WriteLine("1. Total item in Sangkuriang room : ");
            int counter = 0 ;
            var a = from item in user
                    where item.Placement.Name.Contains("Sangkuriang")
                    select counter;
            foreach(var i in a)
            {
                counter++;   
            }
            Console.WriteLine("Ada "+counter+" item");

            Console.WriteLine("\n");
            Console.WriteLine("2. All electronic devices : ");
            var b = from item in user
                    where item.type.Contains("electronic")
                    select item.Name;
            foreach(var i in b)
            {
                Console.WriteLine("* "+i);
            }

            Console.WriteLine("\n");
            Console.WriteLine("3. All furnitures : ");
            var c = from item in user
                    where item.type.Contains("furniture")
                    select item.Name;
            foreach(var i in c)
            {
                Console.WriteLine("* "+i);
            }

            Console.WriteLine("\n");
            Console.WriteLine("4. all items was purchased at 16 Januari 2020 : ");
            var d = from item in user   
                    where DateTimeOffset.FromUnixTimeSeconds(item.PurchasedAt).DateTime.Day==16 && 
                          DateTimeOffset.FromUnixTimeSeconds(item.PurchasedAt).DateTime.Month==1 && 
                          DateTimeOffset.FromUnixTimeSeconds(item.PurchasedAt).DateTime.Year==2020
                    select item.Name;
            foreach(var i in d)
            {
                Console.WriteLine("* "+i);
            }

            Console.WriteLine("\n");
            Console.WriteLine("5. All items with brown color");
            var e = from item in user
                    where item.Tags.Contains("brown")
                    select item.Name;
            foreach(var i in e)
            {
                Console.WriteLine("* "+i);
            }
        }
    }
    class Items
    {
        [JsonProperty("inventory_id")]
        public int InventoryId{get;set;}
        [JsonProperty("name")]
        public string Name{get;set;}
        [JsonProperty("type")]
        public string type {get;set;}
        [JsonProperty("tags")]
        public List<string> Tags {get;set;}= new List<string>();
        [JsonProperty("purchased_at")]
        public long PurchasedAt{get;set;}
        [JsonProperty("placement")]
        public Placement Placement {get;set;}
    }

    class Placement
    {
        [JsonProperty("room_id")]
        public int RoomId{get;set;}
        [JsonProperty("name")]
        public string Name{get;set;}
    }
}
