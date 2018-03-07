
namespace DeathDungeon.Models
{

    //Used to modify character base stats depending on selected class
    public class CharacterClass
    {
        public int Speed { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public string ClassName { get; set; }
        //---------------SWITCH CALL -------------------------------------------

        public void swapStats(int id){
            switch (id)
            {
                case 1:
                    Warrior();
                    ClassName = "Warrior";
                    break;
                case 2:
                    Wizard();
                    ClassName = "Wizard";
                    break;
                case 3:
                    Rogue();
                    ClassName = "Rogue";
                    break;
                case 4:
                    Cleric();
                    ClassName = "Cleric";
                    break;
                case 5:
                    Ranger();
                    ClassName = "Ranger";
                    break;
                case 6:
                    Druid();
                    ClassName = "Druid";
                    break;
                default:
                    break;

            }
        }
        public void swapStats(Class id)
        {
            switch (id)
            {
                case Class.Warrior:
                    Warrior();
                    ClassName = "Warrior";
                    break;
                case Class.Wizard:
                    Wizard();
                    ClassName = "Wizard";
                    break;
                case Class.Rogue:
                    Rogue();
                    ClassName = "Rogue";
                    break;
                case Class.Cleric:
                    Cleric();
                    ClassName = "Cleric";
                    break;
                case Class.Ranger:
                    Ranger();
                    ClassName = "Ranger";
                    break;
                case Class.Druid:
                    Druid();
                    ClassName = "Druid";
                    break;
                default:
                    break;

            }
        }
        //----------------------------------------------------------------------

        //-----------Class STAT SWAP--------------------------------------------
        public void Warrior()
        {
            Speed = 3;
            Attack = 7;
            Defense = 7;
        }

        public void Wizard()
        {
            Speed = 7;
            Attack = 7;
            Defense = 3;
        }
        public void Rogue()
        {
            Speed = 5;
            Attack = 7;
            Defense = 5;
        }
        public void Cleric()
        {
            Speed = 7;
            Attack = 3;
            Defense = 3;
        }
        public void Ranger()
        {
            Speed = 5;
            Attack = 7;
            Defense = 3;
        }
        public void Druid()
        {
            Speed = 5;
            Attack = 5;
            Defense = 5;
        }
        //----------------------------------------------------------------------
    }

}