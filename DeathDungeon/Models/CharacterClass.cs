
namespace DeathDungeon.Models
{

    //Used to modify character base stats depending on selected class
    public class CharacterClass
    {
        //Get set Character Speed
        public int Speed { get; set; }  
        
        //Get set character Description
        public string Description { get; set; }

        //Get set Character Attack
        public int Attack { get; set; } 

        //Get set Character Defense
        public int Defense { get; set; }

        //Get set character class name
        public string ClassName { get; set; }

        //get set character class image
        public string ClassImage { get; set; }

        //Default constructor
        public CharacterClass()
        {
            Speed = 0;
            Attack = 0;
            Defense = 0;
            ClassName = "";
            ClassImage = "";
            Description = "";
        }

        //Call swap stats with character class enum
        public CharacterClass(ClassEnum eenum)
        {
            swapStats(eenum);
        }

        //Call swap stats with character class enum
        public CharacterClass(int eenum)
        {
            swapStats((ClassEnum)eenum);
        }
        

        //Method to set character default name and images depending on enum
        public void swapStats(ClassEnum id)
        {
            switch (id)
            {
                case ClassEnum.Warrior:
                    Warrior();
                    ClassName = "Warrior";
                    ClassImage = "WarriorClass.png";
                    break;
                case ClassEnum.Wizard:
                    Wizard();
                    ClassName = "Wizard";
                    ClassImage = "WizardClass.png";
                    break;
                case ClassEnum.Rogue:
                    Rogue();
                    ClassName = "Rogue";
                    ClassImage = "RogueClass.png";
                    break;
                case ClassEnum.Cleric:
                    Cleric();
                    ClassName = "Cleric";
                    ClassImage = "ClericClass.png";
                    break;
                case ClassEnum.Ranger:
                    Ranger();
                    ClassName = "Ranger";
                    ClassImage = "RangerClass.png";
                    break;
                case ClassEnum.Druid:
                    Druid();
                    ClassName = "Druid";
                    ClassImage = "DruidClass.png";
                    break;
                default:
                    break;

            }
        }
        //----------------------------------------------------------------------

        //-----------Class STAT SWAP--------------------------------------------
        //Method to set character class to Warrior
        public void Warrior()
        {
            Speed = 3;
            Attack = 7;
            Defense = 7;
            Description = "Will beat you to a pulp with a wooden spoon";
        }

        //Method to set character class to Wizard
        public void Wizard()
        {
            Speed = 7;
            Attack = 7;
            Defense = 3;
            Description = "A master of fire AND ice, unlike the Starks";
        }

        //Method to set character class to Rogue
        public void Rogue()
        {
            Speed = 5;
            Attack = 7;
            Defense = 5;
            Description = "Stabs you in the back and leaves in puff of smoke";
        }

        //Method to set character class to Cleric
        public void Cleric()
        {
            Speed = 7;
            Attack = 3;
            Defense = 3;
            Description = "Would heal you, if this game had healing abilities";
        }

        //Method to set character class to Ranger
        public void Ranger()
        {
            Speed = 5;
            Attack = 7;
            Defense = 3;
            Description = "I dare you to try and get close before dying";
        }

        //Method to set character class to Druid
        public void Druid()
        {
            Speed = 5;
            Attack = 5;
            Defense = 5;
            Description = "Jack of all trades, master of none. Boomkin 4tw!";
        }
        //----------------------------------------------------------------------
    }

}