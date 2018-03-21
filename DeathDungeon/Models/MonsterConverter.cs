namespace DeathDungeon.Models
{

    //Used to modify character base stats depending on selected class
    public class MonsterConverter
    {
        //adjust monster stats on level of character maybe?
        public Leveling experience;                 //Attributes calls experience allowing subclasses to call it
        public string Name { get; set; }            //Get/Set for name
        public int MaximumHealth { get; set; }      //Get/Set for Max Health
        public int CurrentHealth { get; set; }      //Get/Set for Current Health
        public int CurrentExperience { get; set; }  //Get/Set for Current Experience
        public int Speed { get; set; }              //Get/Set for Speed
        public int Attack { get; set; }             //Get/Set for Attack
        public int Defense { get; set; }            //Get/Set for Defense
        public int Level { get; set; }              //Get/Set for Level
        public string Description { get; set; }   //Get/Set Description

        public string MonsterImage { get; set; }

        //---------------SWITCH CALL -------------------------------------------

        //Set monster base attributes and image
        public void swapStats(int id)
        {
            switch ((MonsterType)id)
            {
                case MonsterType.bats:
                    Name = "Bat";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 6;
                    Attack = 2;
                    Defense = 3;
                    Level = 1;
                    MonsterImage = "batType.png";
                    Description = "Just keep it out of your hair and you'll be fine";
                    break;

                case MonsterType.bandit:
                    Name = "Bandit";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 5;
                    Attack = 4;
                    Defense = 4;
                    Level = 1;
                    MonsterImage = "banditType.png";
                    Description = "He just want's your wallet";
                    break;

                case MonsterType.blob:
                    Name = "Blob";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 2;
                    Attack = 6;
                    Defense = 7;
                    Level = 1;
                    MonsterImage = "blobType.png";
                    Description = "You thought this was going to be a big pile of ooze";
                    break;

                case MonsterType.demon:
                    Name = "Demon";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 3;
                    Attack = 7;
                    Defense = 5;
                    Level = 1;
                    MonsterImage = "demonType.png";
                    Description = "Big scary";
                    break;

                case MonsterType.dragon:
                    Name = "Dragon";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 8;
                    Attack = 7;
                    Defense = 7;
                    Level = 1;
                    MonsterImage = "dragonType.png";
                    Description = "You have to have a dragon in a d&d game!";
                    break;

                case MonsterType.ghost:
                    Name = "Ghost";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 7;
                    Attack = 4;
                    Defense = 3;
                    Level = 1;
                    MonsterImage = "ghostType.png";
                    Description = "BOO!";
                    break;

                case MonsterType.giant:
                    Name = "Giant";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 3;
                    Attack = 7;
                    Defense = 7;
                    Level = 1;
                    MonsterImage = "giantType.png";
                    Description = "Fi fi fo fum";
                    break;

                case MonsterType.goblin:
                    Name = "Goblin";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 7;
                    Attack = 3;
                    Defense = 3;
                    Level = 1;
                    MonsterImage = "goblinType.png";
                    Description = "Damn he's ugly";
                    break;

                case MonsterType.imp:
                    Name = "Imp";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 7;
                    Attack = 3;
                    Defense = 5;
                    Level = 1;
                    MonsterImage = "imgType.png";
                    Description = "Aww isn't it cute, AHH little f*cker bit me";
                    break;

                case MonsterType.lich:
                    Name = "Lich";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 1;
                    Attack = 7;
                    Defense = 7;
                    Level = 1;
                    MonsterImage = "lichType.png";
                    Description = "Undead and unholy";
                    break;

                case MonsterType.manbearpig:
                    Name = "Man Bear Pig";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 10;
                    Attack = 7;
                    Defense = 7;
                    Level = 1;
                    MonsterImage = "manbearpigType.png";
                    Description = "Half Man, Half Bear, Half Pig";
                    break;

                case MonsterType.rabbit:
                    Name = "Rabbit";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 9;
                    Attack = 2;
                    Defense = 2;
                    Level = 1;
                    MonsterImage = "rabbitType.png";
                    Description = "That is one creepy rabbit";
                    break;

                case MonsterType.stonegolem:
                    Name = "Stone Golem";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 2;
                    Attack = 5;
                    Defense = 10;
                    Level = 1;
                    MonsterImage = "stonegolemType.png";
                    Description = "Sticks and stones may break my bones but...";
                    break;

                case MonsterType.succubus:
                    Name = "Succubus";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 5;
                    Attack = 5;
                    Defense = 5;
                    Level = 1;
                    MonsterImage = "succubusType.png";
                    Description = "Chains and Whips excite me";
                    break;

                case MonsterType.wolf:
                    Name = "Wolf";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 8;
                    Attack = 4;
                    Defense = 5;
                    Level = 1;
                    MonsterImage = "wolfType.png";
                    Description = "My what big teeth you have";
                    break;


                case MonsterType.undefined:
                    Name = "Undefined";
                    MaximumHealth = 10;
                    CurrentHealth = 10;
                    CurrentExperience = 500;
                    Speed = 5;
                    Attack = 5;
                    Defense = 5;
                    Level = 1;
                    MonsterImage = "d20.png";
                    break;


                default:
                    break;

            }
        }

    }
}
