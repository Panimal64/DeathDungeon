using System;
using System.Collections.Generic;
using System.Text;

namespace DeathDungeon.Models
{
    public class DiceRoll
    {
        public enum Dice : uint
        {
            //Used for Random character starting level (between 1 and 6)
            D6 = 6,

            //Used for rolling for Health Increase
            D10 = 10,

            //Used for Attack rolls
            D20 = 20,
        };

        Random rng;

        public DiceRoll()
        {
            rng = new Random();
        }

        //Default rolling method
        private int DefaultRoll(uint dice)
        {
            return 1 + rng.Next((int)dice);
        }

        //Used to roll with specific number of die faces
        public int Roll(Dice faces)
        {
            return DefaultRoll((uint)faces);
        }

        //Used to roll x number of times of a die with x number of faces
        public List<int> MultiRoll(Dice faces, uint number)
        {
            List<int> rolls = new List<int>();
            for (int i = 0; i < number; i++)
            {
                rolls.Add(DefaultRoll((uint)faces));
            }
            return rolls;
        }
    }
}