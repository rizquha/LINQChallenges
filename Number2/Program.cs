using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace TaskNumber2
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"[{
                            ""order_id"": ""SO-921"",
                            ""created_at"": ""2018-02-17T03:24:12"",
                            ""customer"": { ""id"": 33, ""name"": ""Ari"" },
                            ""items"": [
                                        { ""id"": 24, ""name"": ""Sapu Lidi"", ""qty"": 2, ""price"": 13200 }, 
                                        { ""id"": 73, ""name"": ""Sprei 160x200 polos"", ""qty"": 1, ""price"": 149000 }
                            ]
                            },
                            {
                            ""order_id"": ""SO-922"",
                            ""created_at"": ""2018-02-20T13:10:32"",
                            ""customer"": { ""id"": 40, ""name"": ""Ririn"" },
                            ""items"": [
                                        { ""id"": 83, ""name"": ""Rice Cooker"", ""qty"": 1, ""price"": 258000 },
                                        { ""id"": 24, ""name"": ""Sapu Lidi"", ""qty"": 1, ""price"": 13200 }, 
                                        { ""id"": 30, ""name"": ""Teflon"", ""qty"": 1, ""price"": 190000 }
                            ]
                            },
                            {
                            ""order_id"": ""SO-923"",
                            ""created_at"": ""2018-02-28T15:20:43"",
                            ""customer"": { ""id"": 33, ""name"": ""Ari"" },
                            ""items"": [
                                        { ""id"": 303, ""name"": ""Pematik Api"", ""qty"": 1, ""price"": 12000 }, 
                                        { ""id"": 49, ""name"": ""Panci"", ""qty"": 2, ""price"": 70000 }
                            ]
                            },
                            {
                            ""order_id"": ""SO-924"",
                            ""created_at"": ""2018-03-02T14:30:54"",
                            ""customer"": { ""id"": 40, ""name"": ""Ririn"" },
                            ""items"": [
                                        { ""id"": 986, ""name"": ""TV LCD 40 inch"", ""qty"": 1, ""price"": 6000000 }
                            ]
                            },
                            {
                            ""order_id"": ""SO-925"",
                            ""created_at"": ""2018-03-03T14:52:22"",
                            ""customer"": { ""id"": 33, ""name"": ""Ari"" },
                            ""items"": [
                                        { ""id"": 1033, ""name"": ""Nintendo Switch"", ""qty"": 1, ""price"": 4990000 }, 
                                        { ""id"": 2003, ""name"": ""Macbook Air 11 inch 128 GB"", ""qty"": 1, ""price"": 12000000 },
                                        { ""id"": 23, ""name"": ""Pocari Sweat 600ML"", ""qty"": 5, ""price"": 7000 }
                            ]
                            },
                            {
                            ""order_id"": ""SO-926"",
                            ""created_at"": ""2018-03-05T16:23:20"",
                            ""customer"": { ""id"": 58, ""name"": ""Annis"" },
                            ""items"": [
                                        { ""id"": 24, ""name"": ""Sapu Lidi"", ""qty"": 3, ""price"": 13200 }
                            ]
                            }
                            ]";

            var user = JsonConvert.DeserializeObject<List<User>>(json);    

            Console.WriteLine("1. All purchases made in February : ");
            var a = from item in user
                    where item.CreatedAt.Month==2
                    select item.OrderId;
            foreach(var i in a)
            {
                Console.WriteLine("Order Id : "+i);
            }

            Console.WriteLine("\n");
            Console.WriteLine("2. All purchases made by Ari : ");
            var b = from item in user
                    where item.Customer.Name.Contains("Ari")
                    select item.OrderId;
            foreach(var i in b)
            {
                Console.WriteLine("Id Items : "+i);
            }
            Console.WriteLine("Grand Total : "+AriTotal());

            Console.WriteLine("\n");
            Console.WriteLine("3. People who have purchases with grand total lower than 300000 : ");
            Dictionary<string,int> myList = new Dictionary<string, int>()
            {
                {"Ari",AriTotal()},
                {"Ririn",RirinTotal()},
                {"Annis",AnnisTotal()}
            };
            var namaLower = from KeyValuePair<string,int> item in myList
                            where item.Value<300000
                            select item.Key;
            foreach(var i in namaLower)
            {
                Console.WriteLine("- "+i);
            }
            
            int AriTotal()
            {
                int GrandTotal = 0;
                var grandtotal = from item in user
                                 where item.Customer.Name.Contains("Ari")
                                 from items in item.Items
                                 select new {price = items.Price,qty =items.Qty};
                foreach(var total in grandtotal)
                {
                    GrandTotal +=total.price*total.qty;
                }
                return GrandTotal;
            }

            int RirinTotal()
            {
                int GrandTotal = 0;
                var grandtotal = from item in user
                                 where item.Customer.Name.Contains("Ririn")
                                 from items in item.Items
                                 select new {price = items.Price,qty =items.Qty};
                foreach(var total in grandtotal)
                {
                    GrandTotal +=total.price*total.qty;
                }
                return GrandTotal;
            }
            int AnnisTotal()
            {
                int GrandTotal = 0;
                var grandtotal = from item in user
                                 where item.Customer.Name.Contains("Annis")
                                 from items in item.Items
                                 select new {price = items.Price,qty =items.Qty};
                foreach(var total in grandtotal)
                {
                    GrandTotal +=total.price*total.qty;
                }
                return GrandTotal;
            }

        }
    }
    class User
    {
        [JsonProperty("order_id")]
        public string OrderId {get;set;}
        [JsonProperty("created_at")]
        public DateTime CreatedAt {get;set;}
        [JsonProperty("customer")]
        public Customer Customer{get;set;}
        [JsonProperty("items")]
        public List<Items> Items{get;set;} = new List<Items>();



    }
    class Customer
    {
        [JsonProperty("id")]
        public int Id{get;set;}
        [JsonProperty("name")]
        public string Name{get;set;}
    }
    class Items
    {
        [JsonProperty("id")]
        public int Id{get;set;}
        [JsonProperty("name")]
        public string Name{get;set;}
        [JsonProperty("qty")]
        public int Qty{get;set;}
        [JsonProperty("price")]
        public int Price{get;set;}

    }
}
