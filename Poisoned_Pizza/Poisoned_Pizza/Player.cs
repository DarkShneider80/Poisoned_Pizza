using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poisoned_Pizza
{
    public class Player
    {
        public string _Name;
        public Player(string Name)
        {
            _Name = Name;
        }

        public bool EatPizza(int NrPizzasEated, ref int TotalPizzasNumber)
        {
            bool next = true;

            if ((TotalPizzasNumber - NrPizzasEated) <= 0)
            {
                System.Console.WriteLine(_Name + " Hai Perso! ");
                next = false;
            }
            else
            {
                System.Console.WriteLine("Il " + _Name + " Ha Mangiato " + NrPizzasEated + ((NrPizzasEated == 1) ? " Pizza" : " Pizze"));
            }

            TotalPizzasNumber -= NrPizzasEated;

            return next;
        }
    }
}
