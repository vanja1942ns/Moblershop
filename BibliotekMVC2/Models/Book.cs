using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BibliotekMVC2.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public int InitialCount { get; set; }
        public int LendCount { get; set; }
        public int Points { get; set; }
        public int Pris { get; set; }
        public int tempPointsInitialCount { get; set; }
        public int tempPointsLendCount { get; set; }
        //Content of table-list
        public static List<Book> CreateData()
        {
            List<Book> BookList = new List<Book>();

            BookList.Add(new Book { Id = 1, Title = "Soffa ", Type = "Vardagsrum", Count = 20, InitialCount = 20, Pris = 2000  });
            BookList.Add(new Book { Id = 2, Title = "Fåtölj ", Type = "Vardagsrum", Count = 15, InitialCount = 15, Pris = 800 });
            BookList.Add(new Book { Id = 3, Title = "Säng ", Type = "Sovrum", Count = 12, InitialCount = 12, Pris = 5000 });
            BookList.Add(new Book { Id = 4, Title = "Hylla ", Type = "Sovrum", Count = 10, InitialCount = 10, Pris = 800 });
            BookList.Add(new Book { Id = 5, Title = "Matta ", Type = "Köket", Count = 8, InitialCount = 8, Pris = 2500 });
            BookList.Add(new Book { Id = 6, Title = "Klocka ", Type = "Köket", Count = 5, InitialCount = 5, Pris = 2200 });

            return BookList;
        }
        
        //filepath and libary json
        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/library.json");

        //Används för att spara data-Save data json

        public static bool SaveData(List<Book> booklist)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(booklist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);

            return true;
        }

        public static List<Book> GetData()
        {
            List<Book> data;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                data = JsonConvert.DeserializeObject<List<Book>>(json, settings);
            }
            else
            {
                data = CreateData();
            }
            // End of code for saved data
            // Algoritm för poänger

            data = data.OrderBy(x => x.InitialCount).ToList();
            int points = 0;
            foreach(var d1 in data)
            {
                points = points + 5;
                d1.tempPointsInitialCount = points;
                d1.Points = points;
            }


            data = data.OrderBy(x => x.LendCount).ToList();
            points = 0;
            foreach(var d2 in data)
            {
                points = points + 3;
                d2.tempPointsLendCount = points;
                d2.Points += points;
            }

            data = data.OrderByDescending(x => x.Points).ToList();
            SaveData(data);
            return data;
           
            }
        }
    }
    


