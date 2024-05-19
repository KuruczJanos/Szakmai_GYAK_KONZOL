using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealESTATES
{
    internal class Ad
    {
        public int Area { get; set; }
        public Category Category { get; set; }
        public string CreateAt { get; set; }
        public string Description { get; set; }
        public int Floors { get; set; }
        public int FreeOfCharge { get; set; }
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string LatLong
        {
            get
            {
                return String.Format($"{this.lat},${this.lon}");
            }
            set
            {
                this.lat = Convert.ToDouble(value.Split(',')[0].Replace(".", ","));
                this.lon = Convert.ToDouble(value.Split(',')[1].Replace(".", ","));
            }
        }
        private double lat { get; set; }
        private double lon { get; set; }
        public int Rooms { get; set; }
        public Seller Seller { get; set; } //Először a publikus változókat(adat tagokat) hozzuk létre!
                                           //Illetve a hozzá tartozó Getter és Setter elemeket!
        public Ad() { } //Második lépésben csinálunk egy konstruktort ami nem kér paramétert! (Listához kell majd!)

        public Ad(int id, int rooms, string latLong, int floors, int area, string description,
            int freeOfCharge, string imageUrl, string createAt, Seller seller, Category category)
        {
            this.Area = area;
            this.Category = category;
            this.CreateAt = createAt;
            this.Description = description;
            this.Floors = floors;
            this.FreeOfCharge = freeOfCharge;
            this.Id = id;
            this.ImageUrl = imageUrl;
            this.LatLong = latLong;
            this.Rooms = rooms;
            this.Seller = seller;
        } // Harmadik lépésként létrehozzuk a paraméteres konstruktort az osztályhoz, hogy lehessen példányositani!
        public override string ToString()
        { // Ez a ToString metódus azért kell, hogy az ellenőrzésnél képernyőre tudjuk iratni az adatokat.
            return $"{Id}; {Rooms}; {LatLong}; {Floors}; {Area};" +
                    $" {Description}; {FreeOfCharge}; {ImageUrl}; {CreateAt}; {Seller};{Category};";
        }

        public static List<Ad> LoadFromCsv(string fileName)
        //A public static a statikus metódus!
        //A List<Ad> elem a metódus visszatérési értéke!
        //A LoadFromCsv a metódus neve!
        //A (string fileName) a metódus paramétere!
        {

            List<Ad> ads = new List<Ad>(); //Itt hozzuk létre az Ad osztályból képzett listát!

            using (StreamReader sr = new StreamReader(fileName)) //Ezzel az osztállyal fogjuk olvasni a fájl tartalmát!
            { // A using (StreamReader... jelöli a használatba vételt és az osztályt!
              // Az "sr" egy tetszőlegesen választott név!
              // A new StreamReader(fileName) a példányositás és használata a fileName változón!
                
                string line; //Létrehozzuk a sor nevű változót!
                sr.ReadLine();// Az első sort beolvassuk, de nem vesszük figyelembe mivel az csak az oszlop neveket tartalmazza!

                while ((line = sr.ReadLine()) != null) //A ciklus addig fut amig a line egyenlő a beolvasott sorral ami nem egyenlő nullával!
                                                       //Tehát ha a sorok véget érnek a ciklus befejezi önmagát!
                {
                    string[] values = line.Split(';'); //Létrehozunk egy string tipusú tömböt a beolvasott adatok "values" tárolására!
                                                       //Illetve megadjuk a line.split(';') tulajdonsággal,
                                                       //hogy pontos vesszővel tagoltak az adatok!
                    if (values.Length >= 13)
                    { // Feltételhez szabjuk az adatok felvételét a tömb-be, ha a beolvasott sor hossza nagyobb vagy egyenlő
                      // 13 (A csv fájlban kell megszámolni, hogy tudjuk mennyi) akkor hozzá adja a tömbhöz!
                        
                        int id = int.Parse(values[0]); // Minden olyan elemet át kell konvertálni, ami nem "string" (karakterlánc) tipusú!
                                                       // Mert a tömb amibe tároljuk String tipusú!
                                                       // A values[...] jelöli  a fájlban a helyét az adatnak. 
                        int rooms = int.Parse(values[1]);
                        string latLong = values[2];
                        int floors = int.Parse(values[3]);
                        int area = int.Parse(values[4]);
                        string description = values[5];
                        int freeOfCharge = int.Parse(values[6]);
                        string imageUrl = values[7];
                        string createAt = values[8];
                        int SellerId = int.Parse(values[9]); //Itt külön megadjuk a létrehozott osztályok adat tagjait!
                        string SellerName = values[10];      //Mivel a fájlban is igy szerepel!
                        string SellerPhone = values[11];
                        int CategoryId = int.Parse(values[12]);
                        string CategoryName = values[13];

                        //Most létrehozunk az első feladatban készitett osztályokhoz példányokat.
                        //A Category -> az osztály jelölő | Category -> a példány neve
                        //A new Category -> ez jelöli, hogy új osztály pédányt hozok létre.
                        //A (CategoryId, CategoryName); -> A paraméter amivel létre tudom hozni!

                        Category Category = new Category(CategoryId, CategoryName); 
                        Seller Seller = new Seller(SellerId, SellerName, SellerPhone);
                        //Az osztályban beállitott konstruktorral létrehozunk egy példányt!
                        //Ez minden beolvasott sornál megtörténik, igy minden sor egy példányt jelent!
                        Ad ad = new Ad(id, rooms, latLong, floors, area, description,
                                        freeOfCharge, imageUrl, createAt, Seller, Category);

                        ads.Add(ad); // Ezzel adjuk hozzá a listához a kapott példányokat!
                    }
                    else
                    {
                        // A feltétel (If) másik ágába beirhatjuk ezt a sort.
                        // Ez kiirja az alábbi szöveget, ha a feltétel nem teljesül!
                        Console.WriteLine($"Hiba történt a sor feldolgozása során: {line}");
                    }
                }

            }
            return ads; //Itt térünk vissza az Ad osztályból képzett listához!
        }
        public double DistanceTo(double lat, double lon)
        {   // A metódus double tipusú mivel nem egész számmal dolgozunk.
            // Paraméterként az előző feladatban felvett változókat használjuk.
            
            // Itt definiáljuk a két oldalt.
            double dx = Math.Abs(lat - this.lat);
            double dy = Math.Abs(lon - this.lon);

            // Itt a Pitagorasz tétel képlete.
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            // A metódus vissza térési értéke a kapott eredmény. 
        }
    }
}
