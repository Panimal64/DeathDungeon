using System;
namespace DeathDungeon.Models.EnumLists
{
    public class GameRunningClass
    {
       
        public bool ActiveGame = false;

        public GameRunningClass()
        {
        }

        //Start battle
        public void activateGame(){
            ActiveGame = true;
        }

        //Turn off battle after all characters dead
        public void decativateGame(){
            ActiveGame = false;
        }
    }
}
