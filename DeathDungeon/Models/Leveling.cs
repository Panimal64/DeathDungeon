using System;
using System.Collections.Generic;
using System.Text;

namespace DeathDungeon.Models
{
    public class Leveling
    {
        

        //I know it's long and repetative, anyone have a simplier idea?
        //I commented out the stat's that don't get affected on that level
        //increase, just incase he changes it on us later, that way we just have
        //to remove slash's and corresponding stat will be updated.
        //Need to implement a dice roll class so we can do the health increase logic

        //------------------CONSTRUCTOR-----------------------------------------
        public Leveling()
        {
        }
        //----------------------------------------------------------------------
       
        //-------------Experience Stat Bonus------------------------------------
        public void ExperienceCheck(Character character)
        {
            //If player has reached Level cap, if not checks current experience and level and
            //level and stats if need to
            if (character.Level != 20)
            {
                //Checks if character experience is greater than 300 and level is still level 1
               if (character.CurrentExperience >= 300 && character.Level == 1)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                
                else if (character.CurrentExperience >= 900 && character.Level == 2)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 2700 && character.Level == 3)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 6500 && character.Level == 4)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    character.Defense += 1;
                    character.Speed += 1;
                }

                else if (character.CurrentExperience >= 14000 && character.Level == 5)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 23000 && character.Level == 6)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 34000 && character.Level == 7)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 48000 && character.Level == 8)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 64000 && character.Level == 9)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    character.Attack += 1;
                    character.Defense += 1;
                    character.Speed += 1;
                }

                else if (character.CurrentExperience >= 85000 && character.Level == 10)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 100000 && character.Level == 11)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 120000 && character.Level == 12)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 140000 && character.Level == 13)
                {
                    character.Level +=1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 165000 && character.Level == 14)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    //character.Defense += 1;
                    character.Speed += 1;
                }

                else if (character.CurrentExperience >= 195000 && character.Level == 15)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 225000 && character.Level == 16)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 265000 && character.Level == 17)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 305000 && character.Level == 18)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 355000 && character.Level == 19)
                {
                    character.Level += 1;
                    //Input logic for diceroll = MaximumHealth increase
                    //Input logic for current health increase;
                    character.Attack += 1;
                    character.Defense += 1;
                    character.Speed += 1;
                }
            }
            
            

        }
        //----------------------------------------------------------------------

    }


   
}
