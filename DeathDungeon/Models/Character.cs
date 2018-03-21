using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SQLite;
using DeathDungeon.ViewModels;
using DeathDungeon.Models;

//may need to move to separate file


namespace DeathDungeon.Models
{
    public class Character : Attributes
    {

        //-----------------VARIABLES-------------------------------------------
        public ClassEnum classType { get; set; } //default class

        public string ClassName { get; set; } //holds binding name of class

        // Head is a string referencing the database table
        public string Head { get; set; }

        // Feet is a string referencing the database table
        public string Feet { get; set; }

        // Necklasss is a string referencing the database table
        public string Necklass { get; set; }

        // PrimaryHand is a string referencing the database table
        public string PrimaryHand { get; set; }

        // Offhand is a string referencing the database table
        public string OffHand { get; set; }

        // RightFinger is a string referencing the database table
        public string RightFinger { get; set; }

        // LeftFinger is a string referencing the database table
        public string LeftFinger { get; set; }

                        
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
            EntityImage = newData.EntityImage;
            CheckClass(classType);
            ClassName = newData.ClassName;
            Description = newData.Description;
            Level = newData.Level;
            Speed = newData.Speed;
            Defense = newData.Defense;
            Attack = newData.Attack;

            CheckExperience();            //test to see if stat change
        }




        //---------------------------------------------------------------------

        //--------------CONSTRUCTOR & Class SEARCH------------------------------

        //Default Constructor
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
            Description = "";
            ClassName = "UNDEFINED";//default class sting to hold class name
            classType = ClassEnum.Warrior; //cast type to be warrior to test if not set intially
            CheckClass(classType); //will adjust name based on determined class test ,, cast int
            Living = true;
            EntityImage = "Icon-60";
            Head = null;
            Feet = null;
            Necklass = null;
            PrimaryHand = null;
            OffHand = null;
            RightFinger = null;
            LeftFinger = null;


        }

        //makes new character
        public Character(Character _character)
        {
            Name = _character.Name;
            classType = _character.classType;
            ClassName = _character.ClassName;
            Description = _character.Description;
            Level = _character.Level;
            Speed = _character.Speed;
            Defense = _character.Defense;
            Attack = _character.Attack;
            EntityImage = _character.EntityImage;
            

            MaximumHealth = _character.MaximumHealth;
            CurrentHealth = _character.CurrentHealth;
            CurrentExperience = _character.CurrentExperience;
            CheckExperience();
            Level = _character.Level;
            Living = true;

            Head = null;
            Feet = null;
            Necklass = null;
            PrimaryHand = null;
            OffHand = null;
            RightFinger = null;
            LeftFinger = null;

        }

        //for random class
        public Character(int charType)  
        {
            CharacterClass checkclass = new CharacterClass(charType);

            checkclass.swapStats((ClassEnum)charType);
            Name = checkclass.ClassName;
            Attack = checkclass.Attack;
            Speed = checkclass.Speed;
            Defense = checkclass.Defense;
            ClassName = checkclass.ClassName;
            EntityImage = checkclass.ClassImage;
            Description = checkclass.Description;
           
           MaximumHealth = 10;
           CurrentHealth = 10;
           CurrentExperience = 0;
           Level = 1;
           Living = true;
           
           Head = null;
           Feet = null;
           Necklass = null;
           PrimaryHand = null;
           OffHand = null;
           RightFinger = null;
           LeftFinger = null;
        }


        //Check Character class and set's appropriate Attributes
        public void CheckClass(ClassEnum classType)
        {
            CharacterClass checkclass = new CharacterClass();
            
            checkclass.swapStats(classType);
            this.Name = checkclass.ClassName;
            this.Attack = checkclass.Attack;
            this.Speed = checkclass.Speed;
            this.Defense = checkclass.Defense;
            this.ClassName = checkclass.ClassName;
            this.EntityImage = checkclass.ClassImage;
            this.Description = checkclass.Description;

        }
       

        //---------------LEVEL & EXPERIENCE-------------------------------------
        //Method to check character experience and see if enough to level up
        public void CheckExperience()
        {
            Leveling lvl = new Leveling();
            lvl.ExperienceCheck(this); //check return of value

        }

        //Add experience to character after they attack
        public void AddExperience(int experienceToAdd)
        {
            CurrentExperience = CurrentExperience + experienceToAdd;
        }

        //----------------------------------------------------------------------


        //----------------COMBAT------------------------------------------------

        //Method to apply damage to character from monster attacks
        public void ApplyIncomingDamage(int damage)
        {
            
            int newHealth = CurrentHealth - damage;
            CurrentHealth = newHealth;

            
            if (CurrentHealth <= 0)
                Living = false;
        }



        //----------------------------------------------------------------------

        //-----------------ITEMS------------------------------------------------
       
        //getitem value at a location
        public Item GetItem(string itemString)
        {
            return ItemsViewModel.Instance.GetItem(itemString);

        }

        //Method to get item at location 
        public string GetItemAtLocation(ItemLocationEnum itemlocation)
        {
            Item myReturn;

            switch (itemlocation)
            {
                case ItemLocationEnum.Feet:
                    myReturn = GetItem(Feet);
                    //Feet = itemID;
                    break;

                case ItemLocationEnum.Head:
                    myReturn = GetItem(Head);
                    //Head = itemID;
                    break;

                case ItemLocationEnum.Necklass:
                    myReturn = GetItem(Necklass);
                    //  Necklass = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    myReturn = GetItem(PrimaryHand);
                    //  PrimaryHand = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    myReturn = GetItem(OffHand);
                    //  OffHand = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    myReturn = GetItem(RightFinger);
                    //  RightFinger = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    myReturn = GetItem(LeftFinger);
                    // LeftFinger = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }
            if (myReturn == null)
            {
                myReturn = new Item();
                myReturn.Name = "no Item";
            }
            return myReturn.Name;

        }

        public Item GetItemAtLocationNotString(ItemLocationEnum itemlocation)
        {
            Item myReturn;

            switch (itemlocation)
            {
                case ItemLocationEnum.Feet:
                    myReturn = GetItem(Feet);
                    //Feet = itemID;
                    break;

                case ItemLocationEnum.Head:
                    myReturn = GetItem(Head);
                    //Head = itemID;
                    break;

                case ItemLocationEnum.Necklass:
                    myReturn = GetItem(Necklass);
                    //  Necklass = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    myReturn = GetItem(PrimaryHand);
                    //  PrimaryHand = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    myReturn = GetItem(OffHand);
                    //  OffHand = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    myReturn = GetItem(RightFinger);
                    //  RightFinger = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    myReturn = GetItem(LeftFinger);
                    // LeftFinger = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }
            
            return myReturn;

        }
        //Method to add item at location
        public Item AddItem(ItemLocationEnum itemlocation, string itemID)
        {
            Item myReturn;

            switch (itemlocation)
            {
                case ItemLocationEnum.Feet:
                    myReturn = GetItem(Feet);
                    Feet = itemID;
                    break;

                case ItemLocationEnum.Head:
                    myReturn = GetItem(Head);
                    Head = itemID;
                    break;

                case ItemLocationEnum.Necklass:
                    myReturn = GetItem(Necklass);
                    Necklass = itemID;
                    break;

                case ItemLocationEnum.PrimaryHand:
                    myReturn = GetItem(PrimaryHand);
                    PrimaryHand = itemID;
                    break;

                case ItemLocationEnum.OffHand:
                    myReturn = GetItem(OffHand);
                    OffHand = itemID;
                    break;

                case ItemLocationEnum.RightFinger:
                    myReturn = GetItem(RightFinger);
                    RightFinger = itemID;
                    break;

                case ItemLocationEnum.LeftFinger:
                    myReturn = GetItem(LeftFinger);
                    LeftFinger = itemID;
                    break;

                default:
                    myReturn = null;
                    break;
            }

            return myReturn;
        }



        //item will already have locaiton 
        //method to remove item from location
        //methods inside will adjust stats as a result
        //of removing an item

        //Method to drop all items
        public List<Item> DropAllItems()
        {
            var myReturn = new List<Item>();

            // Drop all Items
            Item myItem;

            myItem = RemoveItem(ItemLocationEnum.Head);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Necklass);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.PrimaryHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.OffHand);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.RightFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.LeftFinger);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            myItem = RemoveItem(ItemLocationEnum.Feet);
            if (myItem != null)
            {
                myReturn.Add(myItem);
            }

            return myReturn;
        }

        //Method to remove item at a location
        public Item RemoveItem(ItemLocationEnum itemlocation)
        {
            var myReturn = AddItem(itemlocation, null);

            // Save Changes
            return myReturn;
        }

        //Method to get sum of attributes from item's equipped 
        public int StatModifier(AttributeEnum attribute)
        {
            var myreturn = new int();
            myreturn = 0;
            Item temp;

            if (Head != null)
            {
                temp = GetItem(Head);
                if (temp.Attribute == attribute)
                {
                    myreturn += temp.Value;
                }
            }

            if (Feet != null)
            {
                temp = GetItem(Feet);
                if (temp.Attribute == attribute)
                {
                    myreturn += temp.Value;
                }
            }

            if (Necklass != null)
            {
                temp = GetItem(Necklass);
                if (temp.Attribute == attribute)
                {
                    myreturn += temp.Value;
                }
            }

            if (PrimaryHand != null)
            {
                temp = GetItem(PrimaryHand);
                if (temp.Attribute == attribute)
                {
                    myreturn += temp.Value;
                }
            }

            if (OffHand != null)
            {
                temp = GetItem(OffHand);
                if (temp.Attribute == attribute)
                {
                    myreturn += temp.Value;
                }
            }

            if (RightFinger != null)
            {
                temp = GetItem(RightFinger);
                if (temp.Attribute == attribute)
                {
                    myreturn += temp.Value;
                }
            }

            if (LeftFinger != null)
            {
                temp = GetItem(LeftFinger);
                if (temp.Attribute == attribute)
                {
                    myreturn += temp.Value;
                }
            }

            return myreturn;
        }

        //Get Weapon damage
        public int WeaponDamage(AttributeEnum attribute)
        {

            Item temp;

            if (PrimaryHand != null)
            {
                temp = GetItem(PrimaryHand);
                if (temp.Attribute == AttributeEnum.Attack)
                {
                    return temp.Value;
                }
            }
            return 0;
        }

        //----------------------------------------------------------------------
        public string CharacterAtDeath()
        {
            var myvar = "{" + Name
                + ": " + Description
                + ", " + ClassName + ", Level: " + Level.ToString()
                + ", Attack: " + Attack.ToString() +
                ", Defense: " + Defense.ToString()
                + ", Speed: " + Speed.ToString() +
                ", Experience: " + CurrentExperience.ToString() + " }";
               
            return myvar;
        }


    }
}


