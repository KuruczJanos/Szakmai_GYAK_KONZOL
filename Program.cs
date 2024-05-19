using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealESTATES
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fileName = "realestates.csv"; // Itt határozzuk meg a fájl nevét!
            List<Ad> ads = Ad.LoadFromCsv(fileName); //Az ads nevű listára meghivjuk az Ad osztály LoadFromCsv metódusát!

            Console.WriteLine("Programozás feladat"); // Itt kiiratjuk a konzol ablakba ezt a szöveget.
            // A következő sorok csak ellenőrzésként vannak itt! (nem kötelező!)
            foreach (var ad in ads) // Ezzel a foreach ciklussal kiiratjuk az ads listában lévő ad elemeket.
                                    // Ez a ciklus addig fut amig van "ad" elem a listánkban.
            {
                //Console.WriteLine(ad); // A ciklusmagban adjuk meg ezzel, hogy kiirja a konzol ablakba az "ad" elemet.
                //Console.WriteLine(); // Ezzel csak rakunk egy sortörést minden "ad" elem után,
                                     // hogy jobban átlátható legyen.
            }

            
            var groundFloorAds = ads.Where(ad => ad.Floors == 0).ToList(); 
            if (groundFloorAds.Count > 0)
            { // Definiálunk egy fölszinti lakás hirdetések változót(var groundFloorAds =).
              // Ezután megkeressük a listánkban azokat a lakásokat amik a 0. emeleten vannak.
              // És listába rendezzük azokat. (ads.Where(ad => ad.Floors == 0).ToList();)
              // Az "if (groundFloorAds.Count > 0)" jelentése, hogy akkor fusson le a kód,
              // Ha a földszinti lakások száma több mint Nulla.
                double averageArea = groundFloorAds.Average(ad => ad.Area);
              // Ez a képlet az átlag számitáshoz.
                Console.WriteLine("6. Feladat: ");
                Console.WriteLine($"A földszinti ingatlanok átlagos alapterülete: {averageArea:F2} nm");
                Console.WriteLine();
              // Itt irjuk képernyőre.
            }
            else
            { // Ez az ág teljesül, ha nincs a feltételnek megfelelő eredmény.
                Console.WriteLine("Nincsenek földszinti ingatlanok.");
            }
            // 7. Feladat
            double exampleLat = 47.4979; // Példa szélességi fok (Budapest)
            double exampleLon = 19.0402; // Példa hosszúsági fok (Budapest)

            foreach (var ad in ads)
            {   // A DistanceTo függvény meghivása.
                double distance = ad.DistanceTo(exampleLat, exampleLon);
                // Ez az eredméynek képernyőre iratása.
                //Console.WriteLine($"Ad Id: {ad.Id} távolság a példabeli koordinátától: {distance:F2} egység");
            }

            // 8. Feladat 
            // Megadjuk változóként a Mesevár óvoda kordinátáit.
            double mesevarLat = 47.4164220114023;
            double mesevarLon = 19.066342425796986;

            // Felveszünk egy tehermentes ingatlan hirdetések változót. (var freeOfChargeAds =)
            // Majd megkeressük azokat. (ads.Where(ad => ad.FreeOfCharge == 1).ToList();)
            // És listába rendezzük.
            var freeOfChargeAds = ads.Where(ad => ad.FreeOfCharge == 1).ToList();
            // Felveszünk egy legközelebbi (Ad closestAd = null;) változót és null értéket adunk neki.
            Ad closestAd = null;
            // Ez a változó a számitáshoz.
            double minDistance = double.MaxValue;

            foreach (var ad in freeOfChargeAds)
            {   //Meghivjuk rá az előző feladatban készitett függvényt.
                double distance = ad.DistanceTo(mesevarLat, mesevarLon);
                if (distance < minDistance)
                { // A feltétel után a kapott értéket hozzá rendeljük a változókhoz.
                    minDistance = distance;
                    closestAd = ad;
                }
            }
            // Ezután, ha a legközelebbi ingatlan nem egyenlő nullával, tehát van ingatlan.
            if (closestAd != null)
            {   // Akkor megtörténik a kiiratás.
                Console.WriteLine("8. Feladat ");
                Console.WriteLine("A mesevár óvodához legközelebbi tehermentes ingatlan adatai:");
                Console.WriteLine();
                Console.WriteLine($"\tEladó neve     : {closestAd.Seller.SellerName}"); 
                Console.WriteLine($"\tEladó telefonja: {closestAd.Seller.SellerPhone}");
                                                       // Itt a Seller osztály miatt kell igy hivatkozni.
                                                       // Mivel nem helyi változót iratunk ki.
                Console.WriteLine($"\tAlapterület    : {closestAd.Area} nm");
                Console.WriteLine($"\tSzobák száma   : {closestAd.Rooms}");
                                                       // Ezeknél csak simán mert ezek helyi változók.
            }
            else
            { // Ez az ág fut le, ha nincs a feltételnek megfelelő eredmény.
                Console.WriteLine();
                Console.WriteLine("Nincs tehermentes ingatlan.");
            }

            // Ez azért kell, hogy ne záródjon be a lefutás után az ablak azonnal.
            Console.WriteLine();
            Console.WriteLine("Nyomj meg egy gombot a kilépéshez..."); // Kiirás.
            Console.ReadKey(); //Beolvas egy bármilyen leütött billentyűt. 
        }
    }
}
