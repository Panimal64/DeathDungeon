using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DeathDungeon.Models
{
    public class Monster:Attributes
    {
        //---------------Variable-----------------------------------------------
        //Amount of experience to give after being attack
        //int ExperienceToGive{get; set;}

        //copied code //Change TODO
        [PrimaryKey]
        public string Id { get; set; } //ID for Monster

        public string Description { get; set; } //set to remove later

        public void Update(Monster newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            Level = newData.Level;
            CurrentExperience = newData.CurrentExperience;
            MaximumHealth = newData.MaximumHealth;
            CurrentHealth = newData.CurrentHealth;
            Attack = newData.Attack;
            Defense = newData.Defense;
            Speed = newData.Speed;
            Description = newData.Description;
            Living = newData.Living;
        }

        //----------------------------------------------------------------------

        //---------------Constructor--------------------------------------------
        //Method to name moster and set stats
        public Monster(){
            Name = "Default Monster";                       //Monster Name
            MaximumHealth = 10;              //Monster total health
            CurrentHealth = 10;       //Monster start health
            CurrentExperience = 0;      //Monster experience to give
            Speed = 0;                       //Monster speed
            Attack = 0;                    //Monster attack
            Defense = 0;                   //Monster defense
            Level = 1;                       //Monster level
            Living = true;
        }
        public Monster(string name, int health, int currentHealth,
                       int experience, int speed, int attack, int defense,
                       int level)
        {
            Name = name;                         //Monster Name
            MaximumHealth = health;              //Monster total health
            CurrentHealth = currentHealth;       //Monster start health
            CurrentExperience = experience;      //Monster experience to give
            Speed = speed;                       //Monster speed
            Attack = attack;                     //Monster attack
            Defense = defense;                   //Monster defense
            Level = level;                       //Monster level
            Living = true;
        } 
        //-----------------Death-----------------------------------------------
        //Checks to see if monster is dead
        public bool IsLiving()
        {
            if (Living)
                return true;
            else
                return false;
        }

        //----------------------------------------------------------------------

        //----------------Items-------------------------------------------------
        //Does the monster have item to drop
        public void HoldingItem()
        {
            //TODO
        }

        //Checks to see if monster is dead if so drop item into pool
        public void DropItems()
        {
            //if (!IsLiving)
            //    put all items into pool
        }
        //----------------------------------------------------------------------

        //-----------------Combat-----------------------------------------------
        //Applys damage to monster after attack
        public void ApplyIncomingDamage(int damage)
        {
           
            //sets new HP to incoming attack dmg
            int newHealth = CurrentHealth - damage;
            CurrentHealth = newHealth;

            
           
            //Set character to dead if HP drops to 0
            if (CurrentHealth <= 0){
                DropItems();
                Living = false;
            }
                
        }


        public int GivenExperience(int damage)
        {

            decimal percentage = (((100.0M/CurrentHealth)* damage)/100.0M);
            decimal xp = CurrentExperience * percentage;
            int ExperienceToGive = (int)xp;

            if(ExperienceToGive >= CurrentExperience)
            {
                ExperienceToGive = CurrentExperience;
            }

            CurrentExperience = CurrentExperience - ExperienceToGive;
            return ExperienceToGive;
            //Checks the amount of experience to give
        }
        //----------------------------------------------------------------------
    }
}
