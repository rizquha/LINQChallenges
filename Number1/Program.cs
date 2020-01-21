using System;
using System.Collections.Generic;
// using System.Text.Json;
// using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Linq;

namespace TaskNumber1
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"[{
                                ""id"": 323,
                                ""username"": ""rinood30"",
                                ""profile"": {
                                ""full_name"": ""Shabrina Fauzan"",
                                ""birthday"": ""1988-10-30"",
                                ""phones"": [
                                    ""08133473821"",
                                    ""082539163912"",
                                ],
                                },
                                ""articles"": [
                                {
                                    ""id"": 3,
                                    ""title"": ""Tips Berbagi Makanan"",
                                    ""published_at"": ""2019-01-03T16:00:00""
                                },
                                {
                                    ""id"": 7,
                                    ""title"": ""Cara Membakar Ikan"",
                                    ""published_at"": ""2019-01-07T14:00:00""
                                }
                                ]
                            },
                            {
                                ""id"": 201,
                                ""username"": ""norisa"",
                                ""profile"": {
                                ""full_name"": ""Noor Annisa"",
                                ""birthday"": ""1986-08-14"",
                                ""phones"": [],
                                },
                                ""articles"": [
                                {
                                    ""id"": 82,
                                    ""title"": ""Cara Membuat Kue Kering"",
                                    ""published_at"": ""2019-10-08T11:00:00""
                                },
                                {
                                    ""id"": 91,
                                    ""title"": ""Cara Membuat Brownies"",
                                    ""published_at"": ""2019-11-11T13:00:00""
                                },
                                {
                                    ""id"": 31,
                                    ""title"": ""Cara Membuat Brownies"",
                                    ""published_at"": ""2019-11-11T13:00:00""
                                }
                                ]
                            },
                            {
                                ""id"": 42,
                                ""username"": ""karina"",
                                ""profile"": {
                                ""full_name"": ""Karina Triandini"",
                                ""birthday"": ""1986-04-14"",
                                ""phones"": [
                                    ""06133929341""
                                ],
                                },
                                ""articles"": []
                            },
                            {
                                ""id"": 201,
                                ""username"": ""icha"",
                                ""profile"": {
                                ""full_name"": ""Annisa Rachmawaty"",
                                ""birthday"": ""1987-12-30"",
                                ""phones"": [],
                                },
                                ""articles"": [
                                {
                                    ""id"": 39,
                                    ""title"": ""Tips Berbelanja Bulan Tua"",
                                    ""published_at"": ""2019-04-06T07:00:00""
                                },
                                {
                                    ""id"": 43,
                                    ""title"": ""Cara Memilih Permainan di Steam"",
                                    ""published_at"": ""2019-06-11T05:00:00""
                                },
                                {
                                    ""id"": 58,
                                    ""title"": ""Cara Membuat Brownies"",
                                    ""published_at"": ""2019-09-12T04:00:00""
                                }
                                ]
                            }]";
                var user = JsonConvert.DeserializeObject<List<User>>(json);

                Console.WriteLine("1. Users who doesn't have any phone numbers :");
                var a = from item in user 
                        where item.Profile.Phones.Count==0
                        select item.Username;
                foreach(var i in a)
                {
                    Console.WriteLine(i);
                }

                Console.WriteLine("\n");
                Console.WriteLine("2. users who have articles : ");
                var b = from item in user
                        where item.articles.Count>0
                        select item.Username;
                foreach(var i in b)
                {
                    Console.WriteLine(i);
                }

                Console.WriteLine("\n");
                Console.WriteLine("3. users who have 'annis' on their name :");
                var c = from item in user
                        where item.Profile.FullName.Contains("Annis")
                        select item.Username;
                foreach( var i in c )
                {
                    Console.WriteLine(i);
                }

                Console.WriteLine("\n");
                Console.WriteLine("4. Users who have articles on year 2020 :");
                var d = from item in user
                        from itemX in item.articles
                        where itemX.Published_At.Year==2020
                        select item.Username;
                
                foreach(var i in d)
                {
                    Console.WriteLine(i);
                }

                Console.WriteLine("\n");
                Console.WriteLine("5. users who are born on 1986 : ");
                var e = from item in user
                        where item.Profile.Birthday.Year==1986
                        select item.Username;
                foreach (var i in e)
                {
                    Console.WriteLine(i);
                }

                Console.WriteLine("\n");
                Console.WriteLine("6. articles that contain 'tips' on the title : ");
                var f = from item in user
                        from itemX in item.articles
                        where itemX.Title.Contains("Tips")
                        select new{username=item.Username,title=itemX.Title};
                foreach(var i in f)
                {
                    Console.WriteLine(i.username+" mempunyai artikel : "+i.title);
                }

                Console.WriteLine("\n");
                Console.WriteLine("7. articles published before August 2019 : ");
                var g = from item in user
                        from itemX in item.articles
                        where (itemX.Published_At.Year==2019 && itemX.Published_At.Month<8)
                        select new{title=itemX.Title,username=item.Username};
                foreach(var i in g)
                {
                    Console.WriteLine("* "+i.title+" By "+i.username);
                }

        }
    }
    class User 
    {
        [JsonProperty("id")]
        public int Id {get;set;}
        [JsonProperty("username")]
        public string Username{get;set;} 
        [JsonProperty("profile")]
        public Profile Profile{get;set;}
        [JsonProperty("articles")]
        public List<Articles> articles{get;set;} = new List<Articles>();
        
    }
    class Profile
    {
        [JsonProperty("full_name")]
        public string FullName{get;set;}
        [JsonProperty("birthday")]
        public DateTime Birthday{get;set;}
        [JsonProperty("phones")]
        public List<string> Phones{get;set;} = new List<string>();
    }
    class Articles
    {
        [JsonProperty("id")]
        public int Id{get;set;}
        [JsonProperty("title")]
        public string Title {get;set;}
        [JsonProperty("published_at")]
        public DateTime Published_At{get;set;}
    }
}
