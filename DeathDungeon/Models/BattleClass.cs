using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DeathDungeon.Models;
using DeathDungeon.Views;
using System.Linq;
using DeathDungeon.ViewModels;

namespace DeathDungeon.Models
{
    public class BattleClass
    {

        #region variables
        //----------------------------------------------------------------------
        public bool _gameactive { get; set; }
        public bool AutoPlay { get; set; }
        public String CharacterAttack;                                      //String to hold characters attack/miss for output display
        public String MonsterAttack;                                        //String to hold monster's attack/miss for output display
        public Score score;                                                 
        public List<Item> ItemList = new List<Item>();
        public List<Monster> MonsterList = new List<Monster>();
        public List<Character> CharacterList = new List<Character>();

        private static BattleClass _instance;


        #endregion

        #region new variables
        //---------------OBJECT CONTAINERS---------------------------------------
        public Monster TurnMonster { get; set; }

        public Character TurnCharacter { get; set; }

        public List<Monster> ListMonsters = new List<Monster>();

        public List<Character> ListCharacters = new List<Character>();

        public int turncheckMonster = 0;
        public int turncheckCharacter = 0;

        public int numberMonsterDead = 0;
        public int numbercharacterdead = 0;
        
        public Queue<Monster> monsterq = new Queue<Monster>();
        public Queue<Character> characterq = new Queue<Character>();
        //----------------------------------------------------------------------
        #endregion

        #region startup
        //-------------------INITIALIZERS------------------------------------
        private BattleViewModel _viewModel;
        //sets instance for manual play
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
        //set instance of battle for auto
        public static BattleClass AutoInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BattleClass(true);
                }
                return _instance;
            }
        }

        //constructor for manual play
        public BattleClass()
        {
            _gameactive = false;//set active game
            AutoPlay = false;
            score = new Score();
            CharacterList = new List<Character>();
            ItemList = new List<Item>();
            

        }
        //constructor for auto play
        public BattleClass(bool auto)
        {
            _gameactive = false;//set active game
            AutoPlay = true;
            score = new Score();
            score.AutoBattle = true;
            CharacterList = new List<Character>();
            ItemList = new List<Item>();


        }
        //sets VM
        public void setViewModel()
        {
            _viewModel = BattleViewModel.Instance;
        }
        //sets autoVM
        public void setAutoViewModel()
        {
            _viewModel = BattleViewModel.AutoInstance;

        }
        //---------------SORTING-------------------------------------------
        //get monsters from dataset for list sorting
        public void GetMonsters()
        {
            ListMonsters.Clear();
            foreach (var data in _viewModel.DatasetMonster)
            {
                ListMonsters.Add(data);
                
            }
        }
        //get characters from dataset for list sorting
        public void GetCharacters()
        {
            ListCharacters.Clear();
            foreach (var data in _viewModel.DatasetCharacter)
            {
                ListCharacters.Add(data);

            }
        }
        //sets the turn of monsters and characters on speed
        public void LoadTurnOrder()
        {
            if (monsterq.Count > 0)
            {
                monsterq.Clear();
            }

            if (characterq.Count > 0)
            {
                characterq.Clear();
            }

            foreach (var entity in ListMonsters)
            {
                monsterq.Enqueue(entity);
            }
            foreach (var entity in ListCharacters)
            {
                characterq.Enqueue(entity);
            }
           
        }
        //----------TOGGLES AND OUTPUTS---------------------------------------------
        //switch fight button manual mode
        public void SetFightButton(bool args)
        {
            _viewModel.SetFightButton(args);
        }
        //monster output on debuger
        public void MonsterMessageOutput(string message)
        {
            _viewModel.SetMonsterMessage(message);
        }
        //character output in debuger
        public void CharacterMessageOutput(string message)
        {
            _viewModel.SetCharacterMessage(message);
        }
        //-----------FIGHT CALLS-------------------------------

        #endregion

        #region Fight function
        //fight call in VM for fight in manualplay
        public string Fight()
        {
            return checkTurns();
        }
        //fight call in VM for fight in autoplay
        public async void FightAuto()
        {
            Debug.WriteLineIf(_viewModel.DatasetCharacter.Count == 0, "characters count is at 0 for class dataset (fightauto)");
            if (_viewModel.DatasetCharacter.Count != 0) {
                checkTurnsAuto();
            }
            
        }
        public string checkTurns() //check queue of monsters and characters and look at first element, character default goes first
        {
            Monster m = monsterq.Peek(); 
            Character c = characterq.Peek();
            if (m.IsDead()) //check if monster is dead, if so remove
            {
                    if (monsterq.Count > 1)
                    {
                        monsterq.Dequeue();
                       // numberMonsterDead = numberMonsterDead +1;

                        return checkTurns();
                    }

                }
                if (c.IsDead()) //check if character is dead, remove if dead
                {

                    if (characterq.Count > 1)
                    {
                        characterq.Dequeue();
                       // numbercharacterdead = numbercharacterdead +1;

                        return checkTurns();
                    }

                }
                //case where count 0 now
                //CASES WHERE currenct charactes and monsters will go through turns, if monster/character dies turn count will be added to that side
                //could exceed 6 due to case where 6 entities of one side exceeds 6 turns because attack by otherside increase their count 
                //ex 6 character went, monsters kill 1 character , now count is 7, should account for turn
                if (turncheckCharacter < 6 && turncheckMonster < 6) //case where both parties have not gone full 6 entity
                {

                    if (c.Speed >= m.Speed) //character first
                    {
                        TurnCharacter = c; //character set as turn character
                        SetFightButton(true); //allow character to 
                        turncheckCharacter = turncheckCharacter + 1; // automatically add this character as taken its turn

                        characterq.Enqueue(characterq.Dequeue());//add back to end of queue since didnt die
                        return CharacterTurnAttack();
                         
                    }
                    else 
                    {

                        TurnMonster = m; //case monster higher than character then monster turn
                        turncheckMonster = turncheckMonster + 1;     //add turn as taken         
                        monsterq.Enqueue(monsterq.Dequeue()); //add monster back to end of quueue
                        return MonsterTurnAttack(); 
                        
                    }
                }
                else if (turncheckCharacter < 6 && turncheckMonster >= 6) //case characters havent taken their turns yet but monsters have
                {
                    //see top ln183
                    TurnCharacter = c;
                
                    SetFightButton(true); //allow character to 
                    turncheckCharacter = turncheckCharacter + 1; // automatically add this character as taken its turn

                    characterq.Enqueue(characterq.Dequeue());//add back to end of queue since didnt die
                    return CharacterTurnAttack();
                   
                }
                else if (turncheckCharacter >= 6 && turncheckMonster < 6) //case all characters go through but not monster
                {

                    //set top ln 193
                    TurnMonster = m;
                    turncheckMonster = turncheckMonster + 1;
               
                    monsterq.Enqueue(monsterq.Dequeue());

                    return MonsterTurnAttack();
                }
                else if (turncheckCharacter >= 6 && turncheckMonster >= 6) //case both are filled reset them
                {
                    turncheckCharacter = numbercharacterdead;
                    turncheckMonster = numberMonsterDead;
                    return checkTurns();
                }
                else
                { //alert if deadlock of queueing and fight button
                    SetFightButton(true);
                    Debug.WriteLine("DEADLOCK");
                    return "";
                }
        }
        public void checkTurnsAuto() //check queue of monsters and characters and look at first element, character default goes first
        {

            Monster m = monsterq.Peek();
            Character c = characterq.Peek();
            if (m.IsDead()) //check if monster is dead, if so remove
            {
                if (monsterq.Count > 1)
                {
                    monsterq.Dequeue();
                    // numberMonsterDead = numberMonsterDead +1;
                    m= monsterq.Peek();
                }

            }
            if (c.IsDead()) //check if character is dead, remove if dead
            {

                if (characterq.Count > 1)
                {
                    characterq.Dequeue();
                    // numbercharacterdead = numbercharacterdead +1;
                    c = characterq.Peek();
                }

            }
            //case where count 0 now
            //CASES WHERE currenct charactes and monsters will go through turns, if monster/character dies turn count will be added to that side
            //could exceed 6 due to case where 6 entities of one side exceeds 6 turns because attack by otherside increase their count 
            //ex 6 character went, monsters kill 1 character , now count is 7, should account for turn
            if (turncheckCharacter < 6 && turncheckMonster < 6) //case where both parties have not gone full 6 entity
            {

                if (c.Speed + c.StatModifier(AttributeEnum.Speed)> m.Speed) //character first
                {
                    TurnMonster = m;
                    TurnCharacter = c; //character set as turn character
                    turncheckCharacter = turncheckCharacter + 1; // automatically add this character as taken its turn
                    characterq.Enqueue(characterq.Dequeue());//add back to end of queue since didnt die
                    CharacterTurnAttackAuto();
                    //checkTurnsAuto();
                }
                else if (m.Speed >= c.Speed + c.StatModifier(AttributeEnum.Speed))
                {
                    TurnCharacter = c;
                    TurnMonster = m; //case monster higher than character then monster turn
                    turncheckMonster = turncheckMonster + 1;     //add turn as taken         
                    monsterq.Enqueue(monsterq.Dequeue()); //add monster back to end of quueue
                    MonsterTurnAttackAuto();
                    //checkTurnsAuto();

                }
            }
            else if (turncheckCharacter < 6 && turncheckMonster >= 6) //case characters havent taken their turns yet but monsters have
            {
                //see top ln183
                TurnMonster = m;
                TurnCharacter = c; //character set as turn character
                turncheckCharacter = turncheckCharacter + 1; // automatically add this character as taken its turn
                characterq.Enqueue(characterq.Dequeue());//add back to end of queue since didnt die
                CharacterTurnAttackAuto();
                //checkTurnsAuto();
            }
            else if (turncheckCharacter >= 6 && turncheckMonster < 6) //case all characters go through but not monster
            {

                //set top ln 193
                TurnMonster = m;
                TurnCharacter = c;
                turncheckMonster = turncheckMonster + 1;
                monsterq.Enqueue(monsterq.Dequeue());
                MonsterTurnAttackAuto();
                //checkTurnsAuto();

            }
            else if (turncheckCharacter >= 6 && turncheckMonster >= 6) //case both are filled reset them
            {
                turncheckCharacter = numbercharacterdead;
                turncheckMonster = numberMonsterDead;
                //checkTurnsAuto();
            }
            else
            { //alert if deadlock of queueing and fight button
               
                Debug.WriteLine("DEADLOCK");
              
            }


        }

        #endregion 

        #region attack commands
        public void CharacterTurnAttackAuto()
        {
            if (ListCharacters.Count <= 0 || ListMonsters.Count <= 0)
            {
                if (ListCharacters.Count <= 0) //check to see if there even is characters
                {
                    EndGame();
                    Debug.WriteLine("Characters are dead");

                }
                else if (ListMonsters.Count <= 0)
                {

                    ResetTurns();
                    Template_NewRound();//resets turns, monsters, turn loader
                    Debug.WriteLine("turn reset ln 380"); //should not be called in auto

                }
            }
            else if (ListCharacters.Count != 0 && ListMonsters.Count != 0)
            {
                if (TurnCharacter == null)
                {

                    TurnCharacter = ListCharacters[0];

                }
                if (TurnMonster == null)
                {

                    TurnMonster = ListMonsters[0];

                }

                CharacterAttacks(TurnCharacter, TurnMonster);
                EndTurn();


                if (TurnMonster.IsLiving())
                {
                    CharacterMessageOutput(TurnCharacter.Name + " attacks " + TurnMonster.Name + CharacterAttack + " HP Remaining: " + TurnMonster.CurrentHealth);

                }
                else
                {
                    CharacterMessageOutput(TurnCharacter.Name + " attacks " + TurnMonster.Name + CharacterAttack + " Monster Died");

                    //_viewModel.Pool.Add(TurnMonster.DropItems());
                    //make this a quick function
                    //ListMonsters.Remove(TurnMonster);
                    for (int i=0; i < monsterq.Count ; i++)
                    {
                        var checker = monsterq.Peek();
                        if (checker.IsDead())
                        {
                            monsterq.Dequeue();
                        }
                        else
                            monsterq.Enqueue(monsterq.Dequeue());
                    }
                    var data = TurnMonster.DropItems();
                    _viewModel.Pool.Add(data);
                    _viewModel.battleInstance.score.ItemsDroppedList += data.ItemDetailString();
                    _viewModel.battleInstance.score.MonstersKilledList += TurnMonster.MonsterDetailString();
                    _viewModel.DatasetMonster.Remove(TurnMonster);//kill monster
                    turncheckMonster += 1; //adds a count to monsters turn currently so its closer to 6 turns
                    numberMonsterDead = numberMonsterDead + 1; //if monster dead add to monsterdead, will count
                    TurnMonster = null;
                    score.MonsterSlainNumber += 1;

                    if (ListMonsters.Count == 0)
                    {
                        ResetTurns();

                        Template_NewRound();//resets turns, monsters, turn loader
                                            
                    }
                }

            }
        }

       
        public void MonsterTurnAttackAuto()
        {

            if (ListCharacters.Count <= 0) //check if character party is already dead
            {
                EndGame();
                
                //send endgame function to the battle page
            }

            else if (ListMonsters.Count <= 0) //checks if monsters already dead
            {
                //NEWRound
                // RoundRefreshPage();
                ResetTurns();
                Template_NewRound();
                //await Navigation.PushModalAsync(new ItemDropPage(_viewModel.Pool));

            }

            if (TurnCharacter == null)
            {

                TurnCharacter = ListCharacters[0]; //highest hp later

            }
            if (TurnMonster == null)
            {

                TurnMonster = ListMonsters[0]; //fastest on list next

            }
            MonsterAttacks(TurnCharacter, TurnMonster);
            EndTurn();


            if (TurnCharacter.IsLiving())
            {
                MonsterMessageOutput(TurnMonster.Name + " attacks " + TurnCharacter.Name + MonsterAttack + " HP Remaining: " + TurnCharacter.CurrentHealth);
                

            }
            else
            {

                MonsterMessageOutput(TurnMonster.Name + " attacks " + TurnCharacter.Name + MonsterAttack + " Character Died");

                //OutputMonster.IsVisible = true;
                foreach (var data in TurnCharacter.DropAllItems())
                {
                    _viewModel.Pool.Add(data);
                }
                //make this a quick function
                for (int i = 0; i < characterq.Count; i++)
                {
                    var checker = characterq.Peek();
                    if (checker.IsDead())
                    {
                        characterq.Dequeue();
                    }
                    else
                        characterq.Enqueue(characterq.Dequeue());
                }
                _viewModel.battleInstance.score.CharacterAtDeathList += TurnCharacter.CharacterAtDeath();
                _viewModel.DatasetCharacter.Remove(TurnCharacter);
                TurnCharacter = null;
                turncheckCharacter += 1; //adds a turn to symbolize that a character has died
                numbercharacterdead = numbercharacterdead + 1; //adds to turn 
               
                if (ListCharacters.Count == 0)
                {
                    EndGame();
                }
                
            }

        }


     
        public string MonsterTurnAttack()
        {

            if (ListCharacters.Count <= 0) //check if character party is already dead
            {
                EndGame();
                return "EndGame";
                //send endgame function to the battle page
            }

            else if (ListMonsters.Count <= 0) //checks if monsters already dead
            {
                //NEWRound
                // RoundRefreshPage();
                ResetTurns();
                EndRound();
                return "refresh";
                //await Navigation.PushModalAsync(new ItemDropPage(_viewModel.Pool));

            }

            if (TurnCharacter == null)
            {

                TurnCharacter = ListCharacters[0]; //highest hp later

            }
            if (TurnMonster == null)
            {

                TurnMonster = ListMonsters[0]; //fastest on list next

            }
            MonsterAttacks(TurnCharacter, TurnMonster);
            EndTurn();


            if (TurnCharacter.IsLiving())
            {
                MonsterMessageOutput(TurnMonster.Name + " attacks " + TurnCharacter.Name + MonsterAttack + " HP Remaining: " + TurnCharacter.CurrentHealth);

                return "";

            }
            else
            {

                MonsterMessageOutput(TurnMonster.Name + " attacks " + TurnCharacter.Name + MonsterAttack + " Character Died");
                //OutputMonster.IsVisible = true;
                foreach (var data in TurnCharacter.DropAllItems())
                {
                    _viewModel.Pool.Add(data);
                    
                }
                //make this a quick function
                _viewModel.battleInstance.score.CharacterAtDeathList += TurnCharacter.CharacterAtDeath();
                _viewModel.DatasetCharacter.Remove(TurnCharacter);
                ListCharacters.Remove(TurnCharacter);
                TurnCharacter = null;
                turncheckCharacter += 1; //adds a turn to symbolize that a character has died
                numbercharacterdead = numbercharacterdead + 1; //adds to turn 


                if (ListCharacters.Count == 0)
                {
                    return "EndGame";
                }
                //reloadEntities();
                return "reload";

            }

        }
        
        public string CharacterTurnAttack()
        {
            if (ListCharacters.Count <= 0) //check to see if there even is characters
            {
                EndGame();
                Debug.WriteLine("Characters are dead");
                return "EndGame";
            }
            else if (ListMonsters.Count <= 0)
            {
              
                ResetTurns();
                
               // turncheckCharacter = numbercharacterdead; //will make current turn be number of characters alive ex: if 3 died add 3 to turn so have to wait if 6 till all mosnters go
                //reset turns for all , character amount still same
                return "refresh";

            }

            if (TurnCharacter == null)
            {

                TurnCharacter = ListCharacters[0];

            }
            if (TurnMonster == null)
            {

                TurnMonster = ListMonsters[0];

            }

            CharacterAttacks(TurnCharacter, TurnMonster);
            EndTurn();


            if (TurnMonster.IsLiving())
            {
                CharacterMessageOutput(TurnCharacter.Name + " attacks " + TurnMonster.Name + CharacterAttack + " HP Remaining: " + TurnMonster.CurrentHealth);
                return "";
            }
            else
            {
                MonsterMessageOutput(TurnCharacter.Name + " attacks " + TurnMonster.Name + CharacterAttack + " Monster Died");
                var data = TurnMonster.DropItems();
                _viewModel.Pool.Add(data);
                _viewModel.battleInstance.score.ItemsDroppedList += data.ItemDetailString();
                _viewModel.battleInstance.score.MonstersKilledList += TurnMonster.MonsterDetailString();
                //make this a quick function
                _viewModel.DatasetMonster.Remove(TurnMonster);//kill monster
                ListMonsters.Remove(TurnMonster);
                turncheckMonster += 1; //adds a count to monsters turn currently so its closer to 6 turns
                numberMonsterDead = numberMonsterDead + 1; //if monster dead add to monsterdead, will count
                TurnMonster = null;

                

                if (ListMonsters.Count == 0)
                {
                    ResetTurns();
                    
                    return "refresh";
                }
                //reloadEntities();
                return "reload";

            }

            


        }

        #endregion

        #region new round function
        public void Template_NewRound() //template for new round in autoplay for debug listening
        {
            Debug.WriteLine(" Round of Monster Beat");
            _viewModel.refreshMonster();
            GetMonsters();
            Debug.WriteLineIf(ListMonsters.Count == 0, "null getmonsters in fightclick");
            GetCharacters();
            Debug.WriteLineIf(ListCharacters.Count == 0, "null getcharacters in fightclick");
            LoadTurnOrder();
        }

        public void ResetTurns()
        {
            turncheckCharacter = 0;
            turncheckMonster = 0;
            numbercharacterdead = 0;
            numberMonsterDead = 0;
            turncheckCharacter = numbercharacterdead;
            

        }
        #endregion

        #region game
        //------------------Game Modes------------------------------------------

        public void BeginGame()
        {
            _gameactive = true;

            AddMonstersToRound();
            //START NEW SCORE
            //START NEW BATTLE HISTORY
            //TODO begin game
        }

        //Turn counter
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

        public void EndGame()
        {
            //RECORD SCORES
           
            // Set Score
            score.ScoreTotal = score.ExperienceGainedTotal;

            // Set off state
            _gameactive = false;

          
        }

        //will end game
        public bool AutoPlayGame(bool yes_auto) 
        {
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

        #endregion

        #region combat
        //------------COMBAT----------------------------------------------------

        //Apply damage to character from monster attack
        public void ApplyDamageToCharacter(Character character, int monsterDamage)
        {
            if (character.IsLiving())
            {
                character.ApplyIncomingDamage(monsterDamage);
                if (character.CurrentHealth <= 0)
                {
                    score.DeadCharacters.Add(character);
                    character.Living = false;

                }
            }
        }


        //Apply damage to monster from character attack
        public void ApplyDamageToMonster(Monster monster, int characterDamage)
        {
            if (monster.IsLiving())
            {
                monster.ApplyIncomingDamage(characterDamage);
                if (monster.CurrentHealth < 0)
                {
                    score.DeadMonsters.Add(monster);
                    monster.Living = false;

                }
            }
        }

        //Calculate Attack roll for Character
        public void CharacterAttacks(Character character, Monster monster)
        {
            //Hold character chance to hit
            int hitChance = 0;

            //Hold misschance for monster
            int missChance = 0;

            //New Seed Every Attack
            DiceRoll DiceBag = new DiceRoll();

            //Dice Roll with d20
            int roll = DiceBag.Roll(DiceRoll.Dice.D20);

            //If roll is  1, guarantee miss attack
            if (roll == 1)
            {
                hitChance = 0;
                missChance = 20;
            }

            //else if roll is 20, guarantee hit attack
            else if (roll == 20)
            {
                hitChance = 20;
                missChance = 0;
            }

            //else Calculate hitchance/misschance to see if successfull hit
            else
            {
                //HitChance = D20 dice roll + character level + attack from items
                hitChance = roll + character.Level + character.StatModifier(AttributeEnum.Attack);  


                //Miss change = monster defense, level
                missChance = monster.Defense + monster.Level;
            }
  

            Debug.WriteLine("character: roll:" + roll + " hitchance:" + hitChance + " Misschance:" + missChance);
            Debug.WriteLine("Char defense:" + character.Defense + " character.Level" + character.Level);


            //If hitchance is greater than misschance
            if (hitChance >= missChance)
            {
                //Calcuate damage to apply to monster
                int applyDamage = Convert.ToInt32(Math.Ceiling(character.Level * (.25)) + character.Attack + character.WeaponDamage(AttributeEnum.Attack));

                //Calcuate experience to get from monster to character for % of damage done by character
                int monsterEXP = monster.GivenExperience(applyDamage);

                //Applys damage to monster by calling method
                ApplyDamageToMonster(monster, applyDamage);

                //Add's experience to character corresponding to the percent of damage done
                character.AddExperience(monsterEXP);

                //Add's the experience gained to the score page
                score.ExperienceGainedTotal += monsterEXP;

                //Checks character experience to see if leveled up
                character.CheckExperience();

                //Damage output string
                CharacterAttack = " for " + applyDamage + " damage. ";
            }

            //Hitchance less than misschance
            else
            {
                //Miss string
                CharacterAttack = " Misses attack ";
            }
        }


        //Calculate attack roll for monster
        public void MonsterAttacks(Character character, Monster monster)
        {
            //Hold hitchance for monster
            int hitChance =0;
            
            //Hold misschance for character
            int missChance = 0;

            //New dice bag
            DiceRoll DiceBag = new DiceRoll();

            //Roll d20 dice
            int roll = DiceBag.Roll(DiceRoll.Dice.D20);

            //If roll is  1, guarantee miss attack
            if (roll == 1)
            {
                hitChance = 0;
                missChance = 20;
            }
            //else if roll is 20, guarantee hit attack
            else if (roll == 20)
            {
                hitChance = 20;
                missChance = 0;
            }
            //else Calculate hitchance/misschance to see if successfull hit
            else
            {
                //HitChance = D20 dice roll + monster level + monster attack
                hitChance = roll + monster.Level + monster.Attack;

                //Miss change = character defense, level, and attribute modifiers
                missChance = character.Defense + character.Level + character.StatModifier(AttributeEnum.Defense); 
            }

                        
            Debug.WriteLine("Char defense:" + character.Defense + " character.Level" + character.Level);
            Debug.WriteLine("character: roll:" + roll + " hitchance:" + hitChance + " Misschance:" + missChance + " ");
            //If hitchance is greater than misschance
            if (hitChance >= missChance)
            {
                //Calculate damage to apply
                int applyDamage = Convert.ToInt32(Math.Ceiling(monster.Level * (.25)) + monster.Attack);

                //Call method to apply damage to character
                ApplyDamageToCharacter(character, applyDamage);

                //Damage string
                MonsterAttack = " for " + applyDamage + " damage. ";
            }

            //Else if hitchance is less than misschance
            else
                //Miss string
                MonsterAttack = " Misses attack ";
        }

        #endregion

        #region add monster

        //REMOVE?????????????????????????????????
        private void AddMonstersToRound()
        {
            

            // Check to see if the monster list is full, if so, no need to add more...
            if (MonsterList.Count() >= 6)
            {
                return;
            }

                  
            // Make Sure Monster List exists and is loaded...
            var myMonsterViewModel = MonstersViewModel.Instance;
            var canExecute = myMonsterViewModel.LoadDataCommand.CanExecute(null);
            myMonsterViewModel.LoadDataCommand.Execute(null);

            if (myMonsterViewModel.Dataset.Count() > 0)
            {
                // Get 6 monsters
                do
                {
                    //var rnd = HelperEngine.RollDice(1, myMonsterViewModel.Dataset.Count);
                    //{
                    //    var item = new Monster(myMonsterViewModel.Dataset[rnd - 1]);

                    //    // Help identify which monster it is...
                    //    item.Name += " " + (1 + MonsterList.Count()).ToString();

                    //    var rndScale = HelperEngine.RollDice(ScaleLevelMin, ScaleLevelMax);
                    //    item.ScaleLevel(rndScale);
                    //    MonsterList.Add(item);
                    //}

                } while (MonsterList.Count() < 6);
            }
            else
            {
                // No monsters in DB, so add 6 new ones...
                for (var i = 0; i < 6; i++)
                {
                    var item = new Monster();
                    // Help identify which monster it is...
                    item.Name += " " + MonsterList.Count() + 1;
                    MonsterList.Add(item);
                }
            }
        }
        #endregion
        //---------------GLOBALS---------------------------
        //Method to Reset Global Variables to 0
        public void Rezero() 
        {
            GlobalVariables.AverageLevel = 0;
            GlobalVariables.AverageHealth = 0;
            GlobalVariables.AverageAttack = 0;
            GlobalVariables.AverageDefense = 0;
            GlobalVariables.AverageSpeed = 0;

            GlobalVariables.CharacterLevelSum = 0;
            GlobalVariables.CharacterHealthSum = 0;
            GlobalVariables.CharacterAttackSum = 0;
            GlobalVariables.CharacterDefenseSum = 0;
            GlobalVariables.CharacterSpeedSum = 0;
        }
        
        //Method used to determine monster level scaling
        public void LevelScale()
        {
            //Call reset global variables
            Rezero();
            
            foreach (var data in ListCharacters)
            {
                
                //Sums up living character's level
                GlobalVariables.CharacterLevelSum += data.Level;

                //Sums up living character's Health
                GlobalVariables.CharacterHealthSum += data.MaximumHealth;

                //Sums up living character's Attack
                GlobalVariables.CharacterAttackSum += data.Attack;

                //Sums up living character's Defense
                GlobalVariables.CharacterDefenseSum += data.Defense;

                //Sums up living character's Speed
                GlobalVariables.CharacterSpeedSum += data.Speed;

                //Get's Average of living characters Level
                GlobalVariables.AverageLevel = (GlobalVariables.CharacterLevelSum / ListCharacters.Count);
                
                //Get's Average of living characters Health
                GlobalVariables.AverageHealth = (GlobalVariables.CharacterHealthSum / ListCharacters.Count);
                
                //Get's Average of living characters Attack
                GlobalVariables.AverageAttack = (GlobalVariables.CharacterAttackSum / ListCharacters.Count);
                
                //Get's Average of living characters Defense
                GlobalVariables.AverageDefense = (GlobalVariables.CharacterDefenseSum / ListCharacters.Count);
                
                //Get's Average of living characters Speed
                GlobalVariables.AverageSpeed = (GlobalVariables.CharacterSpeedSum / ListCharacters.Count);
                
            }
          
        }
        
    }
    
}
