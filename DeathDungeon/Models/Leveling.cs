using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace DeathDungeon.Models
{
    public class Leveling
    {


        //I know it's long and repetative, tested  Mikes logic/List
        //but the problem with that is the characters level too fast.
        //Since we have multiple classes each with different base attributes
        //each time level would increase each list item would get added to
        //their current attributes and they would become overpowered too quickly.
        //I'm sure if I went though and changed the values between levels
        //where that stat doesn't get increased I could have gotten it working
        //that way but risk last minute bugs.

        //Example
        //  LevelDetailsList.Add(new LevelDetails(1, 0, 1, 1, 1));
        //  LevelDetailsList.Add(new LevelDetails(2, 300, 1, 2, 1));
        //  LevelDetailsList.Add(new LevelDetails(3, 900, 2, 3, 1));
        //would have to become
        //  LevelDetailsList.Add(new LevelDetails(1, 0, 1, 1, 1));
        //  LevelDetailsList.Add(new LevelDetails(2, 300, 0, 1, 0));
        //  LevelDetailsList.Add(new LevelDetails(3, 900, 1, 1, 0));


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
                    //Increase level by 1
                    character.Level += 1;

                    //Call method to increase health
                    HealthAdjust(character);
                                                            
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                
                else if (character.CurrentExperience >= 900 && character.Level == 2)
                {
                    character.Level += 1;

                    HealthAdjust(character);

                   
                    character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 2700 && character.Level == 3)
                {
                    character.Level += 1;

                    HealthAdjust(character);

                    
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 6500 && character.Level == 4)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    character.Defense += 1;
                    character.Speed += 1;
                }

                else if (character.CurrentExperience >= 14000 && character.Level == 5)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 23000 && character.Level == 6)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 34000 && character.Level == 7)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 48000 && character.Level == 8)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 64000 && character.Level == 9)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                   
                    character.Attack += 1;
                    character.Defense += 1;
                    character.Speed += 1;
                }

                else if (character.CurrentExperience >= 85000 && character.Level == 10)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                   
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 100000 && character.Level == 11)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 120000 && character.Level == 12)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 140000 && character.Level == 13)
                {
                    character.Level +=1;

                    HealthAdjust(character);
                    
                    character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 165000 && character.Level == 14)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    //character.Defense += 1;
                    character.Speed += 1;
                }

                else if (character.CurrentExperience >= 195000 && character.Level == 15)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 225000 && character.Level == 16)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    //character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 265000 && character.Level == 17)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    character.Attack += 1;
                    //character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 305000 && character.Level == 18)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    character.Attack += 1;
                    character.Defense += 1;
                    //character.Speed += 1;
                }

                else if (character.CurrentExperience >= 355000 && character.Level == 19)
                {
                    character.Level += 1;

                    HealthAdjust(character);
                    
                    character.Attack += 1;
                    character.Defense += 1;
                    character.Speed += 1;
                }
            }
            
            

        }
       

        //Method to update character Maxhealth and currentHealth
        public void HealthAdjust(Character character)
        {
            //New DiceRoll
            DiceRoll bag = new DiceRoll();

            //Holds int's from multiroll in list
            List<int> rolls = bag.MultiRoll(DiceRoll.Dice.D10, character.Level);

            //Set's rollsum to 0
            int rollsum = 0;

            //Character's current HP
            int currentHP = character.CurrentHealth;

            //Characters maximum HP
            int oldmax = character.MaximumHealth;

            //Loops though rolls list and add's values to rollsum
            for (int i = 0; i < rolls.Count; i++)
            {
                rollsum += rolls[i];
            }

            //Sets new MaximumHealth to currentMax + rollsum
            character.MaximumHealth = character.MaximumHealth + rollsum;

            //Maximum HP is 20d10
            if (character.MaximumHealth > 200)
            {
                character.MaximumHealth = 200;
            }
                
                       
            //Set's characters CurrentHealth to CurrentMaximum -(OldMaximumHealth - currentHP)
            character.CurrentHealth = (character.MaximumHealth - (oldmax - currentHP));

            //if Health of 200 exceeded, set to 200
            if (character.CurrentHealth > 200)
            {
                character.CurrentHealth = 200;
            }
          
        }
    }

}
