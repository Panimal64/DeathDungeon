using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DeathDungeon.ViewModels;
using System.Threading.Tasks;
namespace DeathDungeon.Models
{
    public class Monster : Attributes
    {
        //---------------Variable-----------------------------------------------
        

        
        public string ItemHolder { get; set; }
  

        public string Description { get; set; } //set to remove later


        public int monsterType{ get; set; }


        public DiceRoll rand;

        //method to update monster
        public void Update(Monster newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            Level = newData.Level;
            monsterType = newData.monsterType;
            CurrentExperience = newData.CurrentExperience;
            MaximumHealth = newData.MaximumHealth;
            CurrentHealth = newData.CurrentHealth;
            Attack = newData.Attack;
            Defense = newData.Defense;
            Speed = newData.Speed;
            Description = newData.Description;
            Living = newData.Living;
            EntityImage = newData.EntityImage; //add image test
            CheckType(monsterType);
            
        }

        //----------------------------------------------------------------------

        //---------------Constructor--------------------------------------------
        //Method to name moster and set stats
        public Monster(){
            Name = "Default Monster";       //Monster Name
            MaximumHealth = 10;             //Monster total health
            CurrentHealth = 10;             //Monster start health
            CurrentExperience = 0;          //Monster experience to give
            Speed = 0;                      //Monster speed
            Attack = 0;                     //Monster attack
            Defense = 0;                    //Monster defense
            Level = 1;                      //Monster level
            Description = "";               //Monster Description
            Living = true;                  //Set living to true
            monsterType = 0;                //Monster type enum
            EntityImage = "Icon-60";        //test icon
            //GrabItem();                   //fill monster with item
        }

         //copy new monster
        public Monster (Monster _monster){  
           this.Name = _monster.Name;
           this.Level = _monster.Level;
           this.CurrentExperience = _monster.CurrentExperience;
           this.MaximumHealth = _monster.MaximumHealth;
           this.CurrentHealth = _monster.CurrentHealth;
           this.Attack = _monster.Attack;
           this.Defense = _monster.Defense;
           this.Speed = _monster.Speed;
           this.Description = _monster.Description;
           this.Living = _monster.Living;
           this.EntityImage = _monster.EntityImage;
            //GrabItem();

        }

        //Check monster type
        public Monster(int MonsterType) {
            CheckType(MonsterType);
        }

        //Method to check monster type and set stats from converter
        public void CheckType(int MonsterType)
        {
            MonsterConverter checkTyper = new MonsterConverter();
            checkTyper.swapStats(MonsterType);
            this.Name = checkTyper.Name;
            this.MaximumHealth = checkTyper.MaximumHealth;
            this.CurrentHealth = checkTyper.CurrentHealth;
            this.CurrentExperience = checkTyper.CurrentExperience;
            this.Level = checkTyper.Level;
            this.Attack = checkTyper.Attack;
            this.Speed = checkTyper.Speed;
            this.Defense = checkTyper.Defense;
            this.Description = checkTyper.Description;
            this.EntityImage = checkTyper.MonsterImage;

        }

        //----------------Items-------------------------------------------------
        //Does the monster have item to drop
        public bool HoldingItem()
        {
            if(ItemHolder != null)
            {
                return true;
            }
            return false;
        }

        //Checks to see if monster is dead if so drop item into pool
        public Item DropItems()
        {
            var myItem = ItemHolder;
            if (!IsLiving())
            {
                return GetItem(myItem);
            }
            //    put all items into pool
            return null ;
        }

        //Get name of item
        public Item GetItem(string itemString)
        {
            return ItemsViewModel.Instance.GetItem(itemString);

        }
        //----------------------------------------------------------------------

        //-----------------Combat-----------------------------------------------
        //Applys damage to monster after attack
        public void ApplyIncomingDamage(int damage)
        {
           
            //sets new HP to incoming attack dmg
            int newHealth = CurrentHealth - damage;
            CurrentHealth = newHealth;
                                   
            //Set monster to dead if HP drops to 0 and drop items
            if (CurrentHealth <= 0){
                DropItems();
                Living = false;
            }
                
        }

        //Calculate experience to give based on damage done
        public int GivenExperience(int damage)
        {
            decimal percentage;

            //Calculate percent of damage done
            if (CurrentHealth != 0)
            {
                percentage = (((100.0M / CurrentHealth) * damage) / 100.0M);
            }
            else
            {
                percentage = 0;
            }
          
            //Calculate experience to give based on damage percent and current experience
            decimal xp = CurrentExperience * percentage;

            //Set experience to give to whole number
            int ExperienceToGive = (int)xp;

            //If more damage done than experience to give (monster dies on attack)
            if(ExperienceToGive >= CurrentExperience)
            {
                //Set's experience to give to current experience
                ExperienceToGive = CurrentExperience;
            }
            
            //Reduces experience monster has to left to give
            CurrentExperience = CurrentExperience - ExperienceToGive;
                       
            //returns experience to give
            return ExperienceToGive;
         }

        //----------------------------------------------------------------------
        public string FormatOutput()
        {
            var UniqueOutput = "None";
            //get uniqueitem variable per mikes game version
            var myUnique = ItemsViewModel.Instance.GetItem("UniqueItem");
            if (myUnique != null)
            {
                UniqueOutput = myUnique.FormatOutput();
            }

            var myReturn = Name;
            myReturn += " , " + Description;
            myReturn += " , Level : " + Level.ToString();
            //current experience should be updated to total experience
            //at time of monster creation, must add new variable to monster
            myReturn += " , Total Experience : " + CurrentExperience;
            myReturn += " , Unique Item : " + UniqueOutput;

            return myReturn;
        }

        public void GrabItem()
        {
             
            string myVar="";
            int x=0;
            if (ItemsViewModel.Instance.Dataset.Count == 0)
            {

            }
            else
            {
                Task.Delay(1000);
                x = MonstersViewModel.Instance.randomMonster.Next(ItemsViewModel.Instance.Dataset.Count);
                Debug.WriteLine("the rand value is "+ x);

                myVar = ItemsViewModel.Instance.Dataset[x].Id;

                ItemHolder = myVar;
            }
        }
        public string MonsterDetailString()
        {
            var myvar = "{" + Name + ": " +
                Description +
                ", Attack: " + Attack.ToString() +
                ", Defense: " + Defense.ToString() +
                ", Speed: " + Speed.ToString() +
                ", Max Health" + MaximumHealth.ToString() + "}";


            return myvar;

        }
    }
}

