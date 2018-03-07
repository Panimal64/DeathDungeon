using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeathDungeon.Models;

namespace DeathDungeon.Models
{
    public class BattleClass
    {
        //----------------------------------------------------------------------
        public bool _gameactive { get; set; }
        bool AutoPlay;
        public Score score;
        private static BattleClass _instance;
        

        public static BattleClass Instance
        {
            get
           {
               if (_instance == null)
                {
                    _instance = new BattleClass();
                }
                return _instance;
            }
        }
       
        public BattleClass()
        {
            _gameactive = false;//set active game
            AutoPlay = false;
            score = new Score();
        }
        //------------------Game Modes------------------------------------------
        //private static ItemsController _instance;

        //public static ItemsController Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new ItemsController();
        //        }
        //        return _instance;
        //    }
        //}
        //needed to intialize the game
        public void BeginGame(){
            _gameactive=true;
            //START NEW SCORE
            //START NEW BATTLE HISTORY
            //TODO begin game
        }
        public void EndTurn()
        {
            score.Turn += 1;
        }
        public Score EndRound()
        {
            score.Round += 1;
            return score;
        }
        //will begin running game
        public Score EndGame(){
            //RECORD SCORES
            //DISPLAY BATTLE HISTORY
            _gameactive=false;

            return score;
        }
        //will end game
        public bool AutoPlayGame(bool yes_auto){
            if (yes_auto)
            {
                AutoPlay = true;
                return true;
            }
            else
                return false;
            //TODO will let game keep playing if on, 
            //method for battle shoudl implment auto methods, but subject to 
            //human maniuplation if false
        }
        //------------------TURN MANAGEMENT-------------------------------------
        //will autoplay if active
        //QUEUE that should compare speed levels of current characters and mosnter
        List<Character> ListCharacters = new List<Character>();
        List<Monster> ListMonsters = new List<Monster>();

        public void battleInstance(){
            //populate both list with current living character and  random monsters
            //order them according to speed
            //ListCharacters = characters.orderby speed(least to greatest)
            //ListMonsters = monsters.orderby speed
           
            //turn by turn attack. if character or monster die, romove them from queue and game
            //battle is over once one of the queue is empty
            //while(!ListCharacters.Any()||!ListMonsters.Any()){
            //scan through list until end then return to start of list
            //remove entity, shift if dead character
            //empty list end of battle to be used again next time
            if(!ListCharacters.Any()){
                EndGame();//will end game if no more characters active
            }
            //}
        }
        //------------COMBAT----------------------------------------------------

        //Apply damage to character from monster attack
        public void ApplyDamageToCharacter(Character character, int monsterDamage){
            if (character.IsLiving())
            {
                character.ApplyIncomingDamage(monsterDamage);
                if (character.CurrentHealth <= 0)
                {
                    score.DeadCharacters.Add(character);
                    character.Living = false;
                   
                }
                //maybe display monster damage character for monsterDmage

            }
        }

        //Apply damage to monster from character attack
        public void ApplyDamageToMonster(Monster monster,
                                         int characterDamage){
            if (monster.IsLiving())
            {
                monster.ApplyIncomingDamage(characterDamage);
                if(monster.CurrentHealth<0)
                {
                    score.DeadMonsters.Add(monster);
                    monster.Living = false;
                    
                }
                //maybe display character damage monster for characterDamage

            }
        }


        public void CharacterAttacks(Character character,Monster monster)
        {
            
            //New Seed Every Attack
            DiceRoll DiceBag = new DiceRoll();
            
            //Dice Roll
            int roll = DiceBag.Roll(DiceRoll.Dice.D20);

            //Hit chance = roll, character level  attribute modifiers
            int hitChance = roll + character.Level;                             // + item.Attribute (sum of All Modifiers)

            //Miss change = monster defense, level, and attribute modifiers
            int missChance = monster.Defense + monster.Level;                    //+ item.Attribute (sum of all Modifiers)

            
            if (hitChance >= missChance)
            {
                int applyDamage = Convert.ToInt32(Math.Ceiling(character.Level * (.25)) + character.Attack);    //+ item.Attribute (Weapon Damage)
                int monsterEXP = monster.GivenExperience(applyDamage);
                ApplyDamageToMonster(monster, applyDamage);
                character.AddExperience(monsterEXP);
                score.ExperienceGainedTotal += monsterEXP;
                character.CheckExperience();
            }
            else {  }
        }

        
        //
        public void MonsterAttacks(Character character, Monster monster)
        {
            
            DiceRoll DiceBag = new DiceRoll();
            int roll = DiceBag.Roll(DiceRoll.Dice.D20);
            int hitChance = roll + monster.Level;                           //+ item.Attribute (sum of all Modifiers)
            int missChance = character.Defense + character.Level;           //+ item.Attribute (sum of all modifiers)

            if (hitChance >= missChance)
            {
                int applyDamage = Convert.ToInt32(Math.Ceiling(monster.Level * (.25)) + monster.Attack);
                ApplyDamageToCharacter(character, applyDamage);
            }
        }
        
    }


}
