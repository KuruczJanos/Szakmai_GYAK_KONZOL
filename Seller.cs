using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealESTATES
{
    internal class Seller
    {
        public int SellerId;
        public string SellerName;
        public string SellerPhone; //Először a privát változókat(adat tagokat) hozzuk létre!

        public Seller(int SellerId, string SellerName, string SellerPhone)
        {
            this.SellerId = SellerId;
            this.SellerName = SellerName;
            this.SellerPhone = SellerPhone; // Második lépésként létrehozzuk a konstruktort az osztályhoz, hogy lehessen példányositani!
        }
        public override string ToString()
        { // Ez a ToString metódus azért kell, hogy az ellenőrzésnél képernyőre tudjuk iratni az adatokat.
            return $"{SellerId}; {SellerName}; {SellerPhone}";
        }

      
    }
}
