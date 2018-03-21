using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using DeathDungeon.Models;
using DeathDungeon.Views;
using System.Linq;

namespace DeathDungeon.ViewModels 
{
    public class BattleViewModel : BaseViewModel
    {
        #region variables
        private static BattleViewModel _instance;
        
        
        public BattleClass battleInstance { get; set; }

        //____________________CONTAINERS AND VARIABLES_________________________________________________
        public ObservableCollection<Monster> DatasetMonster { get; set; }

        public ObservableCollection<Character> DatasetCharacter { get; set; }

        public string MessageMonster { get; set; }

        public string MessageCharacter { get; set; }

        bool fightButton;
        bool characterOutput;
        bool monsterOutput;

       

        public List<string> PartyList { get; set; }
        public List<string> monsterList { get; set; }
        //lload command
        public Command LoadDataCommand { get; set; }

        public List<Item> Pool { get; set; }

        private bool _needsRefresh;
        #endregion

        //_________________INITIALIZERS___________________________________________________
        public static BattleViewModel Instance // manual instance
        {
            get
            {
                if (_instance == null)
                {
                  
                    _instance = new BattleViewModel();//new manual
                }
                return _instance;
            }
        }


        public static BattleViewModel AutoInstance //auto instance
        {
            get
            {
                if (_instance == null)
                {

                    _instance = new BattleViewModel(true); //new auto
                }
                return _instance;
            }
        }

        public BattleViewModel() //construct new manual battle
        {
            Title = "Battle";
            DatasetMonster = new ObservableCollection<Monster>();
            DatasetCharacter = new ObservableCollection<Character>();
            PartyList = new List<string>();

            battleInstance = new BattleClass();
            Pool = new List<Item>();
            
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            //___________________CRUD_________________________________________________________
            //crudi monster

            MessagingCenter.Subscribe<BattlePage, Monster>(this, "DeleteDataMonster", async (obj, data) =>
            {
                DatasetMonster.Remove(data);
                //await DataStore.DeleteAsync_MonsterParty(data);
            });
            MessagingCenter.Subscribe<BattlePage, Character>(this, "DeleteDataCharacter", async (obj, data) =>
            {
                DatasetCharacter.Remove(data);
                //await DataStore.DeleteAsync_CharacterParty(data);
            });

        }

        //Reset for auto battle
        public void resetAutoBattleView()
        {
            
            battleInstance = new BattleClass();
            _instance = new BattleViewModel(true);
        }

        //reset for manual battle
        public void resetBattleView()
        {

            battleInstance = new BattleClass();
            _instance = new BattleViewModel();
        }

        //Autoplay
        public BattleViewModel(bool autoplay) //construct new auto battle
        {
            Title = "Battle";
            DatasetMonster = new ObservableCollection<Monster>();
            DatasetCharacter = new ObservableCollection<Character>();
            PartyList = new List<string>();

            battleInstance = new BattleClass(true);
            SetAutoPlay();
            Debug.WriteIf(battleInstance.AutoPlay==false, "AUTOPLAY didnt turn one ");

            Pool = new List<Item>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommandAuto());

            //___________________CRUD_____________________________________________________
            //crudi monster

            MessagingCenter.Subscribe<BattlePage, Monster>(this, "DeleteDataMonster", async (obj, data) =>
            {
                DatasetMonster.Remove(data);
                //await DataStore.DeleteAsync_MonsterParty(data);
            });
            MessagingCenter.Subscribe<BattlePage, Character>(this, "DeleteDataCharacter", async (obj, data) =>
            {
                DatasetCharacter.Remove(data);
                //await DataStore.DeleteAsync_CharacterParty(data);
            });

        }
        

        #region refresh commands
        // Return True if a refresh is needed
        // It sets the refresh flag to false
        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }
            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }
        //private async Task ExecuteTurnClearCommand()
        #endregion

        #region battle comands

        //look to see if any side is fully dead
        //Checks to see if entire team is dead
        public  string entityCheck()
        {
            //additional cases in event where all monster or characters die after message
            //push page if character count was not caught
            if (DatasetCharacter.Count == 0)
            {
                //update current round and mosnters slain
                updateScore();
                
                return "EndGame";

            }
            else if (DatasetMonster.Count == 0)
            {
                updateScore();
                return "refresh";
            }
            updateScore();
            return "";
        }

        //execute manual
        private async Task  ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DatasetMonster.Clear();
                DatasetCharacter.Clear();
                MonstersViewModel.Instance.newRoundMonsters(); // call new instance of monster
                
                var datasett = CharactersViewModel.Instance.Dataset;
                
                battleInstance.setViewModel();
                refreshMonster();//adds new monsters
               
                //Load Characters from party
                List<Character> SortedCharacterList = new List<Character>();
                foreach (var data in PartyList) //place current partylist created into a list for sorting
                {
                    var mydata = datasett.FirstOrDefault(arg => arg.Id == data);

                    if (mydata.Id == data)
                    {
                        Character _character = new Character(mydata);//character copy
                        _character.Id = Guid.NewGuid().ToString(); //sets new ID for coppy character
                        SortedCharacterList.Add(_character); //sorts character to lst for sorting
                    }

                }
                var newlist = SortedCharacterList.OrderByDescending(s => s.Speed + s.StatModifier(AttributeEnum.Speed)).ThenByDescending(n => n.Name).ToList(); //sort character
                DatasetCharacter.Clear(); //clear DatasetCharacter even if blank
                foreach (var _character in newlist) //loop to add sortedlistcharacters to dataset
                {
                    DatasetCharacter.Add(_character);
                }
                //Load Characters and Monster turns
                battleInstance.GetMonsters();
                Debug.WriteLineIf(battleInstance.ListMonsters.Count ==0, "null getmonsters");
                battleInstance.GetCharacters();
                Debug.WriteLineIf(battleInstance.ListMonsters.Count == 0, "null getcharacters");
                battleInstance.LoadTurnOrder(); //at start of execution load battle turns for initial battle

               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        //execute auto
        private async Task ExecuteLoadDataCommandAuto()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DatasetMonster.Clear();
                DatasetCharacter.Clear(); ;
                
                MonstersViewModel.Instance.newRoundMonsters(); // call new instance of monster
                 CharactersViewModel.Instance.FillParty();//fill party with new characters
                
                battleInstance.setAutoViewModel();//auto set of VM
                await Task.Delay(1000);

                NewCharacters();
                Debug.WriteLineIf(DatasetCharacter.Count == 0, "Datset on load is empty for character");
                
                refreshMonster();//adds new monsters
                Debug.WriteLineIf(DatasetMonster.Count == 0, "Datset on load is empty for Monster");
                await Task.Delay(1000);
                //Load Characters and Monster turns
                battleInstance.GetMonsters();
                Debug.WriteLineIf(battleInstance.ListMonsters.Count == 0, "null getmonsters");
                battleInstance.GetCharacters();
                Debug.WriteLineIf(battleInstance.ListMonsters.Count == 0, "null getcharacters");
                battleInstance.LoadTurnOrder(); //at start of execution load battle turns for initial battle

             
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        //________________________TURN LOGICS__________________________________________________

        //monster is current monster of turn, chosen by charecter
        public void SetTurn(Monster _monster) 
        {
            battleInstance.TurnMonster = _monster;
        }

        //character is current character of turn, or charcter chosen by monster to attack
        public void SetTurn(Character _character) 
        {
            battleInstance.TurnCharacter = _character;
        }

        //User clicked flight
        public string FightClicked()
        {
            battleInstance.GetCharacters(); //load DatasetCharacter to a current battle instance
            battleInstance.GetMonsters(); //load DatasetMonster to current battle insnance
            return battleInstance.Fight(); //check turn of monster and instance a fight
        }

        //Autoplay clicked
        public async Task<string> FightClickedAutoAsync()
        {
            
                if (battleInstance.AutoPlay == true)
                {

                    battleInstance.GetMonsters();
                    Debug.WriteLineIf(battleInstance.ListMonsters.Count == 0, "null getmonsters in fightclick");
                    battleInstance.GetCharacters();
                    Debug.WriteLineIf(battleInstance.ListMonsters.Count == 0, "null getcharacters in fightclick");
                    battleInstance.LoadTurnOrder();
                    Debug.WriteLine("__________________START OF AUTOBATTLE_____________________________________");
                    while (DatasetCharacter.Count != 0)
                    {
                        battleInstance.FightAuto();
                  
                        if (DatasetCharacter.Count == 0)
                        {
                            return "endgame";

                        }
                        if (DatasetMonster.Count == 0)
                        {
                            Debug.WriteLine(" Round of Monster Beat");
                            refreshMonster();
                            battleInstance.ResetTurns();
                            battleInstance.GetMonsters();
                            battleInstance.score.Round += 1;
                            Debug.WriteLineIf(battleInstance.ListMonsters.Count == 0, "null getmonsters in fightclick");
                            battleInstance.GetCharacters();
                            Debug.WriteLineIf(battleInstance.ListMonsters.Count == 0, "null getcharacters in fightclick");
                            battleInstance.LoadTurnOrder();

                            GiveCharactersItems();

                        }

                    }
                    Debug.WriteLine("__________________END OF AUTOBATTLE_____________________________________");
                    return "endgame";
                }
                else
                {
                    Debug.WriteLine("__________________Wrong Instace OF AUTOBATTLE_____________________________________");

                    battleInstance.GetCharacters(); //load DatasetCharacter to a current battle instance
                    battleInstance.GetMonsters(); //load DatasetMonster to current battle insnance
                    return battleInstance.Fight(); //check turn of monster and instance a fight
                }
         
        }

        //Set autoplay to true
        public void SetAutoPlay()
        {
            battleInstance.AutoPlay = true;
        }

        //change fight button 
        public void SetFightButton(bool args) 
        {
            fightButton = args;
            OnPropertyChanged();
        }

        #endregion

        #region messages
        
        
        //____________________MESSAGES_______________________________________________
        //set monster output to this outpust
        public void SetMonsterMessage(string message)
        {
            MessageMonster = null;
            MessageMonster = message;
            Debug.WriteLine(message);
            monsterOutput = true;
            OnPropertyChanged();
        }

        //set characer output to this
        public void SetCharacterMessage(string message) 
        {
            MessageCharacter = null;
            MessageCharacter = message;
            Debug.WriteLine(message);
            characterOutput = true;
            OnPropertyChanged();
        }

        //End round message if characters beat all monsters
        public string checkCharacterMessage() {
            string message ="Character Feed: ";
            if (DatasetMonster.Count == 0)
            {
                message = "Characters have Won";
                return message;
            }
            if (characterOutput == true)
            {
                message += MessageCharacter;
                characterOutput = false;
            }
             return message;         
        }

        //Start round message announcing new mosnters
        public string checkMonsterMessage()
        {
            string message = "Monster Feed: ";
            if (DatasetMonster.Count == 0)
            {
                message = " New monsters have arrived! ";
                return message;
            }
            if (monsterOutput == true)
            {
                message += MessageMonster;
                characterOutput = false;
            }
            return message;

        }

        #endregion

        #region update functions
         //get current round

        //__________________________SCORES AND TRACKING______________________________________________
        //Get current round #
        public string getRound()
        {
            return battleInstance.score.Round.ToString();
        }

        //update score
        public void updateScore() 
        {
            if (DatasetMonster.Count == 0)
            {
                battleInstance.score.Round += 1;
                battleInstance.score.MonsterSlainNumber += 6;
            }
            else if (DatasetCharacter.Count == 0)
            {
                battleInstance.score.MonsterSlainNumber += 6- DatasetMonster.Count;
            }
            
        }
        #endregion
       
        //_____________________NEW MONSTER__________________________________________________
        #region commands
        //refresher for monster list 

        public void refreshMonster()
        {
            DatasetMonster.Clear(); //clear any current datasetmonster
            
            //Call levelscale to reset globals and get averages before new round
            battleInstance.LevelScale();

            // call new instance of monster
            MonstersViewModel.Instance.newRoundMonsters();

            //new monsters get but into var dataset 
            var dataset = MonstersViewModel.Instance.Dataset;
            
            List<Monster> SortedList = new List<Monster>();

            //insert dataset for sorting
            foreach (var data in dataset) 
            {
                //var mydata = dataset.FirstOrDefault(arg => arg.Id == data);
                if (DatasetMonster.Count < 6)
                {
                    Monster _monster = new Monster(data); // monster copy ////////TEST
                    _monster.Id = Guid.NewGuid().ToString();

                    //If round count does not equal 1?
                    
                    //Set's monster's Level to average character level
                    _monster.Level = (int)(GlobalVariables.AverageLevel);

                    //Set's monster's experience to current experience level if level is 1 else experience*level
                    if(_monster.Level <= 1)
                    {
                        _monster.CurrentExperience = _monster.CurrentExperience;
                    }
                    else
                    {
                        _monster.CurrentExperience = _monster.CurrentExperience * _monster.Level;
                    }
                    
                   
                    //Set's monsters health to 50% of Averagehealth * base max health
                    _monster.MaximumHealth = (int)(0.5*(GlobalVariables.AverageHealth + _monster.MaximumHealth));
                    
                    //Set current health to maximum health
                    _monster.CurrentHealth = _monster.MaximumHealth;

                    //Set's attack to 50% of average attack + base attack
                    _monster.Attack = (int)(0.5 * (GlobalVariables.AverageAttack + _monster.Attack));

                    //Set's defense to 50% of average defense + base defense
                    _monster.Defense= (int)(0.5 * (GlobalVariables.AverageDefense + _monster.Defense));

                    //Set's speed to 50% of average speed + base speed
                    _monster.Speed = (int)(0.5 * (GlobalVariables.AverageSpeed + _monster.Speed));


                    SortedList.Add(_monster);
                }
            }
            var newlist = SortedList.OrderByDescending(s => s.Speed).ThenByDescending(n => n.Name).ToList();

            foreach (var data in newlist) //load dataset with sorted list
            {
                data.GrabItem();
                DatasetMonster.Add(data);
            }

            battleInstance.GetCharacters();
            battleInstance.GetMonsters();


        }
        //________________NEW CHARACTERS____________________________________________________
        //New character to dataset, sort by initiative
        public void NewCharacters()
        {
            DatasetCharacter.Clear(); //clear DatasetCharacter even if blank

            var datasett = CharactersViewModel.Instance.Dataset;

            List<Character> SortedCharacterList = new List<Character>();
            foreach (var data in datasett) //place current partylist created into a list for sorting
            {
                if( DatasetCharacter.Count < 6)
                {
                    Character _character = new Character(data);//character copy
                    _character.Id = Guid.NewGuid().ToString(); //sets new ID for coppy character
                    SortedCharacterList.Add(_character); //sorts character to lst for sorting
                }

            }
            var newlist = SortedCharacterList.OrderByDescending(s => s.Speed).ThenByDescending(n => n.Name).ToList(); //sort character
          
            foreach (var _character in newlist) //loop to add sortedlistcharacters to dataset
            {
                _character.Id = Guid.NewGuid().ToString();
                DatasetCharacter.Add(_character);
                Debug.WriteLine(_character.Name + " added in  Battle view newcharacters()"); //shows inserts
            }
            Debug.WriteLine(DatasetCharacter.Count.ToString()+ " current cout of datsetcharacter"); //debug display

        }

        //______________________SPEED COMPARE___________________________________________________
        //Checks sorted character speed
        public void SpeedCheck()
        {
            List<Character> SortedCharacterList = new List<Character>();
            foreach( var data in DatasetCharacter)
            {
                SortedCharacterList.Add(data);
                Debug.WriteLine(data.Name + " <--name  speed->> " + data.Speed + "  " + data.StatModifier(AttributeEnum.Speed) + " <--speed modifier");
            }
            var newlist = SortedCharacterList.OrderByDescending(s => s.Speed + s.StatModifier(AttributeEnum.Speed)).ThenByDescending(n => n.Name).ToList(); //sort character
            DatasetCharacter.Clear(); //clear DatasetCharacter even if blank
            foreach (var _character in newlist) //loop to add sortedlistcharacters to dataset
            {
                DatasetCharacter.Add(_character);
            }
        }

        //to move
        //____________________________________________________________________________________
        #endregion

        public void GiveCharactersItems()
        {
            bool equipped = false;
            
            int count = DatasetCharacter.Count;

            int counter = 0;
            while(Pool.Count > 0 ) {
                var data = Pool[0];
                    equipped = false;
                    counter = 0;
                    while (equipped == false && counter < count)
                    {
                        var helditem = DatasetCharacter[counter].GetItemAtLocationNotString(data.Location);
                        if (helditem == null)
                        {
                            DatasetCharacter[counter].AddItem(data.Location, data.Id);
                            Pool.Remove(data);
                            equipped = true;
                        }
                        else if (helditem.Value > data.Value)
                        {
                            var item = DatasetCharacter[counter].AddItem(data.Location, data.Id);
                            Pool.Remove(data);
                            equipped = true;

                            if (item != null)
                            {
                                Pool.Add(item);
                            }
                        }
                        else
                        {
                            counter += 1;
                        }
                    }
                    if(equipped == false)
                    {
                        Pool.Remove(data);
                        Pool = Pool.Where(x => x != null).ToList();
                    }

                
            }
            Pool.Clear();
        }
       
    }
}

