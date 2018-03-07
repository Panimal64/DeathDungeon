using System;
using System.Collections.Generic;
using System.Text;
//Dave Pannu
namespace DeathDungeon.Models
{
    public class Attributes
    {
        public Leveling experience;                 //Attributes calls experience allowing subclasses to call it
        public string Name { get; set; }            //Get/Set for name
        public int MaximumHealth { get; set; }          //Get/Set for Max Health
        public int CurrentHealth { get; set; }      //Get/Set for Current Health
        public int CurrentExperience { get; set; }  //Get/Set for Current Experience
        public int Speed { get; set; }              //Get/Set for Speed
        public int Attack { get; set; }             //Get/Set for Attack
        public int Defense { get; set; }            //Get/Set for Defense
        public int Level { get; set; }              //Get/Set for Level
        public bool Living { get; set; }            //Get/Set for Living

        //Default Constructor
        public Attributes()
        {
            Name = "Death Dungeon";
            MaximumHealth = 10;
            CurrentHealth= 10;
            CurrentExperience = 0;
            Speed = 1;
            Attack = 1;
            Defense = 1;
            Level = 1;
            Living = true;
        }


        //Used to link base stats to class stats

    }
}
