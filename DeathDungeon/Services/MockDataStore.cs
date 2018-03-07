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
        private List<Character> _characterParty = new List<Character>();
        private List<Item> _itemPool = new List<Item>();
        private List<Monster> _monsterParty = new List<Monster>();

        private MockDataStore()
        {
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Name = "Sword of Truth", Description="On hit foes speak the truth or cry out in pain, typically the latter" },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Emperor's New Pants", Description="Only those who are truly worthy can see these." },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Golden Uggs", Description="So comfortable you don't care what anyone thinks of them" },
             };

            foreach (var data in mockItems)
            {
                _itemDataset.Add(data);
            }

            var mockCharacters = new List<Character>
            {
                new Character { Id = Guid.NewGuid().ToString(), Name = "Warrior", classType = 2, Level = 1, CurrentExperience = 10, MaximumHealth = 10, CurrentHealth = 10,  
                    Attack = 7, Defense = 7, Speed = 3,  Description ="This is an Character description.", ClassName = "Wizard" },

                new Character { Id = Guid.NewGuid().ToString(), Name = "Wizard", classType = 3, Level = 2, CurrentExperience = 20, MaximumHealth = 20, CurrentHealth = 20,
                    Attack = 7, Defense = 3, Speed = 7,  Description ="This is an Character description." , ClassName = "Rouge"},

                new Character { Id = Guid.NewGuid().ToString(), Name = "Cleric", classType = 5, Level = 3, CurrentExperience = 30, MaximumHealth = 30, CurrentHealth = 30,
                    Attack = 3, Defense = 3, Speed = 7,  Description ="This is an Character description." , ClassName = "Ranger"},

                 new Character { Id = Guid.NewGuid().ToString(), Name = "Rogue", classType = 2, Level = 1, CurrentExperience = 10, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 7, Defense = 5, Speed = 5,  Description ="This is an Character description.", ClassName = "Wizard" },

                new Character { Id = Guid.NewGuid().ToString(), Name = "Ranger", classType = 3, Level = 2, CurrentExperience = 20, MaximumHealth = 20, CurrentHealth = 20,
                    Attack = 7, Defense = 3, Speed = 7,  Description ="This is an Character description." , ClassName = "Rouge"},

                new Character { Id = Guid.NewGuid().ToString(), Name = "Druid", classType = 5, Level = 3, CurrentExperience = 30, MaximumHealth = 30, CurrentHealth = 30,
                    Attack = 5, Defense = 5, Speed = 5,  Description ="This is an Character description." , ClassName = "Ranger"},

            };

            foreach (var data in mockCharacters)
            {
                _characterDataset.Add(data);
                _characterParty.Add(data);
            }

            var mockMonsters = new List<Monster>
            {
                new Monster { Id = Guid.NewGuid().ToString(), Name = "Bat", Level = 1, CurrentExperience = 10, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 3, Defense = 7, Speed = 5, Description ="This is an Monster description." },

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Imp", Level = 2, CurrentExperience = 20, MaximumHealth = 20, CurrentHealth = 20,
                    Attack = 4, Defense = 5, Speed = 5, Description ="This is an Monster description." },

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Goblin", Level = 3, CurrentExperience = 30, MaximumHealth = 30, CurrentHealth = 30,
                    Attack = 5, Defense = 3, Speed = 7, Description ="This is an Monster description." },

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Rabbit", Level = 1, CurrentExperience = 10, MaximumHealth = 10, CurrentHealth = 10,
                    Attack = 2, Defense = 2, Speed = 10, Description ="This is an Monster description." },

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Blob", Level = 2, CurrentExperience = 20, MaximumHealth = 20, CurrentHealth = 20,
                    Attack = 7, Defense = 3, Speed = 5, Description ="This is an Monster description." },

                new Monster { Id = Guid.NewGuid().ToString(), Name = "Bandit", Level = 3, CurrentExperience = 30, MaximumHealth = 30, CurrentHealth = 30,
                    Attack = 3, Defense = 3, Speed = 7, Description ="This is an Monster description." },
            };

            foreach (var data in mockMonsters)
            {
                _monsterDataset.Add(data);
                _monsterParty.Add(data);
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
            _characterParty.Remove(myData);

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

        //character party

        public async Task<bool> RecruitAsync_Character(Character data)
        {
            if (_characterParty.Count < 6)
            {
                _characterParty.Add(data);
            }

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteAsync_CharacterParty(Character data)
        {
            var myData = _characterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _characterParty.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Character>> GetPartyAsync_Character(bool forceRefresh = false)
        {
            return await Task.FromResult(_characterParty);
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
            _monsterParty.Remove(myData);
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

        //Monster Party
        public async Task<bool> RecruitAsync_Monster(Monster data)
        {
            if (_monsterParty.Count < 6)
            {
                _monsterParty.Add(data);
            }

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteAsync_MonsterParty(Monster data)
        {
            var myData = _monsterDataset.FirstOrDefault(arg => arg.Id == data.Id);
            _monsterParty.Remove(myData);

            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Monster>> GetPartyAsync_Monster(bool forceRefresh = false)
        {
            return await Task.FromResult(_monsterParty);
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