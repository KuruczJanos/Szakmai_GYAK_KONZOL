using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealESTATES
{
    internal class Category
    {
        public int CategoryId;
        public string CategoryName; //Először a privát változókat(adat tagokat) hozzuk létre!

        public Category(int CategoryId, string CategoryName)
        {
            this.CategoryId = CategoryId;
            this.CategoryName = CategoryName;// Második lépésként létrehozzuk a konstruktort az osztályhoz, hogy lehessen példányositani!
        }
        public override string ToString()
        { // Ez a ToString metódus azért kell, hogy az ellenőrzésnél képernyőre tudjuk iratni az adatokat.
            return $"{CategoryId}; {CategoryName}";
        }


    }
}
