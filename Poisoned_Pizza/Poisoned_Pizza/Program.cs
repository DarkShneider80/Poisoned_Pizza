using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poisoned_Pizza
{
    public class Program
    {
        public class Combination
        {
            public int Quantity;
            public bool chosen;
        }

        static public int TotalNrPizzas;

        static void Main(string[] args)
        {
            Random rnd = new Random();
            Player PlayerA = new Player("Giocatore A");
            Player PlayerB = new Player("Giocatore B");
            TotalNrPizzas = 10 * rnd.Next(1, 4);
            
            System.Console.WriteLine("**POISONED PIZZA**");
            System.Console.WriteLine("Ogni giocatore sceglie quante pizze mangiare ad ogni turno");
            System.Console.WriteLine("Non è possibile scegliere lo stesso numero di pizze dell'altro giocatore");
            System.Console.WriteLine("Chi Mangia l'ultima pizza che è avvelenata, perde" +'\n');
            System.Console.WriteLine("Ci sono " + TotalNrPizzas + " Pizze Sul tavolo" + '\n');

            List<Combination> CombinationsList = ResetCombinations();

            while (TotalNrPizzas > 0)
            {
                if (DoWork(PlayerA, ref CombinationsList) == true)
                    System.Console.WriteLine(((TotalNrPizzas == 1) ? " Rimane " : " Rimangono ") + TotalNrPizzas + ((TotalNrPizzas == 1) ? " Pizza" : " Pizze") + ('\n'));

                if (DoWork(PlayerB, ref CombinationsList) == true)
                    System.Console.WriteLine(((TotalNrPizzas == 1) ? " Rimane " : " Rimangono ") + TotalNrPizzas + ((TotalNrPizzas == 1) ? " Pizza" : " Pizze"));
            }

            System.Console.ReadLine();
        }

        static private bool DoWork(Player _player, ref List<Combination> CombinationsList)
        {
            string response = string.Empty;
            bool ret = false, next = true;

            while (ret == false)
            {
                if ((TotalNrPizzas == 1) && (CombinationsList.Where(p => p.Quantity == 1 && p.chosen == true).ToList().Count != 0))
                {
                    System.Console.WriteLine(_player._Name + " Salta il turno  ");
                    CombinationsList = ResetCombinations();
                    break;

                }
                else if(TotalNrPizzas == 1)
                {
                    System.Console.WriteLine(_player._Name + " Puoi mangiare solo la pizza avvelenata! ");
                    response = "1";
                    ret = true;
                    break;
                }
                else if (TotalNrPizzas <= 0)
                {
                    next = false;
                    break;
                }
                else
                {
                    System.Console.WriteLine(_player._Name + " Quante Pizze vuoi mangiare ?  ");

                    // I selezionabili devono essere disponibili (Chosen == false) e le quantità consumabili non devono essere superiori alle pizze totali
                    CombinationsList.Where(p => p.chosen == false && (p.Quantity <= TotalNrPizzas)).ToList().ForEach(p => System.Console.Write(" [" + p.Quantity.ToString() + "] "));
                    System.Console.Write('\n');
                    response = System.Console.ReadLine();

                    // La quantità scelta deve essere tra quelle visualizzate a schermo
                    List<Combination> choice = CombinationsList.Where(p => p.Quantity == Int32.Parse(response) && p.chosen == false && (p.Quantity <= TotalNrPizzas)).ToList();

                    if (choice.Count != 0)
                    {
                        // i criteri di uguaglianza usati precedentemente devono consentire un solo elemento nella lista
                        Combination Chosen = choice.FirstOrDefault();
                        // riporto al defaul le possibilità di scelta 
                        CombinationsList = ResetCombinations();
                        // setto la scelta corrente
                        CombinationsList.Where(p => p.Quantity == Chosen.Quantity).ToList().FirstOrDefault().chosen = true;

                        ret = true;
                    }
                    else
                    {
                        System.Console.WriteLine(_player._Name + " Non puoi mangiare questo numero di pizze!  ");
                        ret = false;
                    }

                }
            }

            if ((ret == true) && (response != string.Empty))
                next = _player.EatPizza(Int32.Parse(response), ref TotalNrPizzas);

            return next;
        }

        static private List<Combination> ResetCombinations()
        {
            List<Combination> CurrentCombination = new List<Combination>();

            for (int i = 1; i <= 3; i++)
            {
                Combination _combination = new Combination();

                _combination.chosen = false;
                _combination.Quantity = i;

                CurrentCombination.Add(_combination);
            }

            return CurrentCombination;
        }

    }
}
