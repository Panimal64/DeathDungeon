using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeathDungeon.Models;
using Character = DeathDungeon.Models.Character;

namespace DeathDungeon.Services
{
    public sealed class MockDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MockDataStore _instance;

        public static MockDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MockDataStore();
                }
                return _instance;
            }
        }

        private List<Item> _itemDataset = new List<Item>();
        private List<Character> _characterDataset = new List<Character>();
        private List<Monster> _monsterDataset = new List<Monster>();
        private List<Score> _scoreDataset = new List<Score>();
        
        private List<Item> _itemPool = new List<Item>();


        private MockDataStore()
        {
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Name = "Sword of Truth", Description="On hit foes speak the truth or cry out in pain, typically the latter", Attribute= AttributeEnum.Attack, Location=ItemLocationEnum.PrimaryHand, Value = 5 , ImageURI="d20.png"},
           
                new Item { Id = Guid.NewGuid().ToString(), Name = "Golden Uggs", Description="So comfortable you don't care what anyone thinks of them" , Attribute= AttributeEnum.Speed, Location=ItemLocationEnum.Feet , Value = 5, ImageURI="d20.png" },

            };

            foreach (var data in mockItems)
            {
                _itemDataset.Add(data);
            }

            var mockCharacters = new List<Character>
            {
                new Character { Id = Guid.NewGuid().ToString(), Name = "Warrior", classType = ClassEnum.Warrior, Level = 1, CurrentExperience = 0, MaximumHealth = 10, CurrentHealth = 10,  
                    Attack = 7, Defense = 7, Speed = 3,  Description ="This is an Character description.", ClassName = "Warrior" , EntityImage = "WarriorClass.png" },

                new Character { Id = Guid.NewGuid().ToString(), Name = "Wizard", classType = ClassEnum.Wizard, Level = 1, CurrentExperience = 0, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 7, Defense = 3, Speed = 2,  Description ="This is an Character description." , ClassName = "Wizard", EntityImage = "WizardClass.png" },

                new Character { Id = Guid.NewGuid().ToString(), Name = "Cleric", classType = ClassEnum.Cleric, Level = 1, CurrentExperience = 0, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 3, Defense = 3, Speed = 5,  Description ="This is an Character description." , ClassName = "Cleric" , EntityImage = "ClericClass.png"},

                 new Character { Id = Guid.NewGuid().ToString(), Name = "Rogue", classType = ClassEnum.Rogue, Level = 1, CurrentExperience = 0, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 7, Defense = 5, Speed = 10,  Description ="This is an Character description.", ClassName = "Rogue" , EntityImage = "RogueClass.png" },

                new Character { Id = Guid.NewGuid().ToString(), Name = "Ranger", classType = ClassEnum.Ranger, Level = 1, CurrentExperience = 0, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 7, Defense = 3, Speed = 10,  Description ="This is an Character description." , ClassName = "Ranger", EntityImage = "RangerClass.png"},

                new Character { Id = Guid.NewGuid().ToString(), Name = "Druid", classType = ClassEnum.Druid, Level = 1, CurrentExperience = 0, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 5, Defense = 5, Speed = 8,  Description ="This is an Character description." , ClassName = "Druid", EntityImage = "DruidClass.png"},

            };

            foreach (var data in mockCharacters)
            {
                _characterDataset.Add(data);
               
            }

            var mockMonsters = new List<Monster>
            {
                new Monster { Id = Guid.NewGuid().ToString(), Name = "Bat", Level = 1, CurrentExperience = 10, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 3, Defense = 7, Speed = 5, Description ="This is an Monster description." , EntityImage = "Cyclops.png"},

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Imp", Level = 2, CurrentExperience = 20, MaximumHealth = 20, CurrentHealth = 20,
                    Attack = 4, Defense = 5, Speed = 5, Description ="This is an Monster description." , EntityImage ="Cyclops.png"},

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Goblin", Level = 3, CurrentExperience = 30, MaximumHealth = 30, CurrentHealth = 30,
                    Attack = 5, Defense = 3, Speed = 7, Description ="This is an Monster description." ,EntityImage ="WizardClass.png"},

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Rabbit", Level = 1, CurrentExperience = 10, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 2, Defense = 2, Speed = 10, Description ="This is an Monster description." ,EntityImage ="Cyclops.png" },

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Blob", Level = 2, CurrentExperience = 20, MaximumHealth = 20, CurrentHealth = 20,
                    Attack = 7, Defense = 3, Speed = 5, Description ="This is an Monster description." ,EntityImage ="Cyclops.png"},

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Bandit", Level = 3, CurrentExperience = 30, MaximumHealth = 30, CurrentHealth = 30,
                    Attack = 3, Defense = 3, Speed = 7, Description ="This is an Monster description.",EntityImage ="Cyclops.png" },
            };

            foreach (var data in mockMonsters)
            {
                _monsterDataset.Add(data);
             
            }

            var mockScores = new List<Score>
            {
                new Score { Id = Guid.NewGuid().ToString(), Name = "First Score", ScoreTotal = 111},
                new Score { Id = Guid.NewGuid().ToString(), Name = "Second Score", ScoreTotal = 222},
                new Score { Id = Guid.NewGuid().ToString(), Name = "Third Score", ScoreTotal = 333},
            };

            foreach (var data in mockScores)
            {
                _scoreDataset.Add(data);

            }

        }

        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Item(data.Id);
            if (oldData == null)
            {
                // If it does not exist, add it to the DB
                var InsertResult = await AddAsync_Item(data);
                if (InsertResult)
                {
                    return true;
                }

                return false;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Item(data);
            if (UpdateResult)
            {
                return true;
            }

            return false;
        }
        
        public async Task<bool> AddAsync_Item(Item data)
        {
            _itemDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var myData = _itemDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _itemDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            return await Task.FromResult(_itemDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            return await Task.FromResult(_itemDataset);
        }


        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            _characterDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterDataset.Remove(myData);
//            _characterParty.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            return await Task.FromResult(_characterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            return await Task.FromResult(_characterDataset);
        }

       
        //Monster
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            _monsterDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _monsterDataset.Remove(myData);
            return await Task.FromResult(true);
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            return await Task.FromResult(_monsterDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            return await Task.FromResult(_monsterDataset);
        }

        
        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            _scoreDataset.Add(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var myData = _scoreDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _scoreDataset.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            return await Task.FromResult(_scoreDataset.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            return await Task.FromResult(_scoreDataset);
        }

    }
}