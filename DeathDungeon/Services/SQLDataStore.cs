using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeathDungeon.Models;
using DeathDungeon.ViewModels;
using Character = DeathDungeon.Models.Character;

namespace DeathDungeon.Services
{
    public sealed class SQLDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static SQLDataStore _instance;

        public static SQLDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLDataStore();
                }
                return _instance;
            }
        }

        private SQLDataStore()
        {

            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Character>().Wait();
            App.Database.CreateTableAsync<Monster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<Character>().Wait();
            App.Database.CreateTableAsync<Monster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();

        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<Character>().Wait();
            App.Database.DropTableAsync<Monster>().Wait();
            App.Database.DropTableAsync<Score>().Wait();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        public void InitializeDatabaseNewTables()
        {
            // Delete the tables
            DeleteTables();

            // make them again
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        private async void InitilizeSeedData()
        {
            //Item Seed Data
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "First item", Description = "This is an item description." });


            //Character Seed Data
            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Warrior",
                classType = ClassEnum.Warrior,
                Level = 1,
                CurrentExperience = 0,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 7,
                Defense = 7,
                Speed = 3,
                Description = "This is an Character description.",
                ClassName = "Warrior",
                EntityImage = "WarriorClass.png"
            });

            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Wizard",
                classType = ClassEnum.Wizard,
                Level = 1,
                CurrentExperience = 0,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 7,
                Defense = 3,
                Speed = 2,
                Description = "This is an Character description.",
                ClassName = "Wizard",
                EntityImage = "WizardClass.png"
            });

            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cleric",
                classType = ClassEnum.Cleric,
                Level = 1,
                CurrentExperience = 0,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 3,
                Defense = 3,
                Speed = 5,
                Description = "This is an Character description.",
                ClassName = "Cleric",
                EntityImage = "ClericClass.png"
            });

            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Rogue",
                classType = ClassEnum.Rogue,
                Level = 1,
                CurrentExperience = 0,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 7,
                Defense = 5,
                Speed = 10,
                Description = "This is an Character description.",
                ClassName = "Rogue",
                EntityImage = "RogueClass.png"
            });

            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Ranger",
                classType = ClassEnum.Ranger,
                Level = 1,
                CurrentExperience = 0,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 7,
                Defense = 3,
                Speed = 10,
                Description = "This is an Character description.",
                ClassName = "Ranger",
                EntityImage = "RangerClass.png"
            });

            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Druid",
                classType = ClassEnum.Druid,
                Level = 1,
                CurrentExperience = 0,
                MaximumHealth = 10,
                CurrentHealth = 10,
                Attack = 5,
                Defense = 5,
                Speed = 8,
                Description = "This is an Character description.",
                ClassName = "Druid",
                EntityImage = "DruidClass.png"
            });


            //Monster Seed Data
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "ManBearPig", Level = 1, CurrentExperience = 10,
                MaximumHealth = 10, CurrentHealth = 10, Attack = 10, Defense = 10, Speed = 1, Description = "Half Man, Half Bear, Half Pig." });



            //Score Seed Data
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "First Score", ScoreTotal = 111 });

        }

        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {
            // Check to see if the item exists
            var old = await GetAsync_Item(data.Id);

            // If it does not exist, then Insert it into the DB
            if (old == null)
            {
                var Insert = await App.Database.InsertAsync(data);
                if (Insert == 1)
                {
                    return true;
                }
            }

            // If it does exist, Update it into the DB
            var Update = await UpdateAsync_Item(data);
            if (Update)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Item> GetAsync_Item(string id)
        {
            try
            {
                var result = await App.Database.GetAsync<Item>(id);
                return result;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

        }

        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Item>().ToListAsync();
            return result;
        }


        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Character> GetAsync_Character(string id)
        {
            var result = await App.Database.GetAsync<Character>(id);
            return result;
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Character>().ToListAsync();

            return result;
        }
        
        //Monster
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            var result = await App.Database.GetAsync<Monster>(id);
            return result;
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Monster>().ToListAsync();
            return result;

        }

        // Score
        public async Task<bool> AddAsync_Score(Score data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync_Score(Score data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<Score> GetAsync_Score(string id)
        {
            var result = await App.Database.GetAsync<Score>(id);
            return result;
        }

        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Score>().ToListAsync();
            return result;

        }

    }
}