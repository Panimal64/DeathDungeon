using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


//may need to move to separate file


namespace DeathDungeon.Models
{
    public class Character : Attributes
    {

        //-----------------VARIABLES-------------------------------------------
        public int classType { get; set; } //default class
        //public ItemLocation itemSlots = new ItemLocation(); // hold item slots
        public string ClassName { get; set;} //holds binding name of class

        //copied var//CHANGE TODO
        [PrimaryKey]
        public string Id { get; set; } //hold PK for Character class

        public string Description { get; set; } //default description, may remove

        public void Update(Character newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            classType = newData.classType;
            CheckClass(classType);
            ClassName = newData.ClassName;
            Description = newData.Description;
            Level = newData.Level;
            Speed = newData.Speed;
            Defense = newData.Defense;
            Attack = newData.Attack;
            LevelUp();
            //test to see if stat change
        }


    

        //---------------------------------------------------------------------

        //--------------CONSTRUCTOR & Class SEARCH------------------------------
        public Character()
        {
            Name = "Default ";
            MaximumHealth = 10;
            CurrentHealth = 10;
            CurrentExperience = 0;
            Speed = 1;
            Attack = 1;
            Defense = 1;
            Level = 1;
            ClassName = "UNDEFINED";//default class sting to hold class name
            //classType = Class.undefined; no need takes int now
            classType = 1; //cast type to be warrior to test if not set intially
            CheckClass(classType); //will adjust name based on determined class test ,, cast int
            Living = true;
        }
        //default constructor
        public Character(string name, int health, int currentHealth, 
                         int experience, int speed, int attack, int defense,
                         int level, int characterClass)
        {  
            Name = name;
            MaximumHealth = health;
            CurrentHealth = currentHealth;
            CurrentExperience = experience;
            Speed = speed;
            Attack = attack;
            Defense = defense;
            Level = level;
            classType = characterClass;
            CheckClass(classType);
            Living = true;
        }
        //constructor base with passed by value, testing
       
        public Character(string name, int characterClass)
        {
            Name = name;
            MaximumHealth = 10;
            CurrentHealth = 10;
            classType = characterClass;
            CheckClass(classType); // will subsitute, see Attributes.cs
            CurrentExperience = RandomLevel();
            CheckExperience(); //should random level and experience
            Living = true;
        }
        //constructor based on class with just classtype

        public void CheckClass(int classType)
        {
            CharacterClass checkclass = new CharacterClass();
            checkclass.swapStats(classType);
            Attack = checkclass.Attack;
            Speed = checkclass.Speed;
            Defense = checkclass.Defense;
            ClassName = checkclass.ClassName;

        }
        //substitute all   specific skills with Class version
        //----------------------------------------------------------------------

        //---------------LEVEL & EXPERIENCE-------------------------------------
        public void CheckExperience()
        {
            Leveling lvl= new Leveling();
            lvl.ExperienceCheck(this); //check return of value

        }
        //scaling level to granted experience
        //main source for leveling up

        public void LevelUp()
        {
            CheckExperience();
        }
        //due to delete if unchanged
        //Checks character experience level to see if it enough to level up

        public int RandomLevel()
        {
            Random rand = new Random();
            int randomlvl= rand.Next(1,14000);
            return randomlvl;
        }
        //creates random lvl between 1-5 by giving experience range of 1-1400

      
        public void AddExperience(int experienceToAdd)
        {
            CurrentExperience= CurrentExperience + experienceToAdd;
         
        }
        //Add's experience to character after attack
        //----------------------------------------------------------------------

        //--------------RESET GAME & REROLLING----------------------------------

        public void Reroll()
        {
            // will subsitute stats with level
            CheckClass(classType);
            CurrentExperience = RandomLevel();//should random level and experience
            CheckExperience(); //will scale skill according to experience
        } 
        //reroll character stats with name
        //----------------------------------------------------------------------


        //----------------COMBAT------------------------------------------------
        public void ApplyIncomingDamage(int damage)
        {
            //sets new HP to incoming attack dmg
            int newHealth = CurrentHealth - damage;
            CurrentHealth = newHealth;

            //Set character to dead if HP drops to 0
            if (CurrentHealth <= 0)
                Living = false;
        }

        //Checks to see if character is living, if living returns true, otherwise false
        public bool IsLiving()
        {
            if (Living){
                return true;
            }
            else
                return false;
        }
        //----------------------------------------------------------------------

        //-----------------ITEMS------------------------------------------------
        //getitem value at a location
        public void GetItem()
        {
            //TODO
        }
       
        //method to place specifc item on character
        //methods inside will apply stat modifiers when called
        public void AttachItemm(Item item)
        {
            //TODO
            //release items
            //itemSlots.ReplaceSlot(item);
        }
        //item will already have locaiton 
        //method to remove item from location
        //methods inside will adjust stats as a result
        //of removing an item
        public void RemoveItem()
        {
            //TODO   //release items to pool
            //itemSlots.EmptySlot(Itemlocation);//empty slot based on location
          
        }

        public void DropItems()
        {
            //TODO//will add items to pool, and empty slots
            if(!IsLiving())
            {
                //itemSlots.EmptyAllSlots();
            }

        }

        //----------------------------------------------------------------------

       
       
    }
}


