using System;
using System.Collections.Generic;
using System.Text;

namespace DeathDungeon.Models
{
    public class DiceRoll
    {
        public enum Dice 
        {
            //Used for rolling for Health Increase
            D10 = 10,

            //Used for Attack rolls
            D20 = 20,
        };

        //Hold random number
        Random rng;

        //Set random number
        public DiceRoll()
        {
            rng = new Random();
        }

        //Default rolling method
        private int DefaultRoll(int dice)
        {
            return 1 + rng.Next((int)dice);
        }

        //Used to roll with specific number of die faces
        public int Roll(Dice faces)
        {
            return DefaultRoll((int)faces);
        }
       
        //Used to roll a die with x number of faces, x number of times of
        //EG Multiroll(D20, 5)
        public List<int> MultiRoll(Dice faces, int number)
        {
            List<int> rolls = new List<int>();
            for (int i = 0; i < number; i++)
            {
                rolls.Add(DefaultRoll((int)faces));
            }
            return rolls;
        }
    }
}