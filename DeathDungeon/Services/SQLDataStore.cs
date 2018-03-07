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
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Second item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Third item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Description = "This is an item description." });
            await AddAsync_Item(new Item { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Description = "This is an item description." });


            //Character Seed Data
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "First Character", classType = 0, Level = 1, CurrentExperience = 100,
                MaximumHealth = 10, CurrentHealth = 10, Attack = 10, Defense = 10, Speed = 1, Description = "This is an Character description." });

            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Second Character", classType = 0, Level = 2, CurrentExperience = 200,
                MaximumHealth = 20, CurrentHealth = 20, Attack = 20, Defense = 20, Speed = 2, Description = "This is an Character description." });

            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Third Character", classType = 0, Level = 3, CurrentExperience = 300,
                MaximumHealth = 30, CurrentHealth = 30, Attack = 30, Defense = 30, Speed = 3, Description = "This is an Character description." });

            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Fourth Character", classType = 0, Level = 4, CurrentExperience = 400,
                MaximumHealth = 40, CurrentHealth = 40, Attack = 40, Defense = 40, Speed = 4, Description = "This is an Character description." });

            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Fifth Character", classType = 0, Level = 5, CurrentExperience = 500,
                MaximumHealth = 50, CurrentHealth = 50, Attack = 50, Defense = 50, Speed = 5, Description = "This is an Character description." });

            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "Sixth Character", classType = 0, Level = 6, CurrentExperience = 600,
                MaximumHealth = 60, CurrentHealth = 60, Attack = 60, Defense = 60, Speed = 6, Description = "This is an Character description." });


            //Monster Seed Data
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "ManBearPig", Level = 1, CurrentExperience = 10,
                MaximumHealth = 10, CurrentHealth = 10, Attack = 10, Defense = 10, Speed = 1, Description = "Half Man, Half Bear, Half Pig." });

            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Second Monster", Level = 2, CurrentExperience = 20,
                MaximumHealth = 20, CurrentHealth = 20, Attack = 20, Defense = 20, Speed = 2, Description = "This is an Monster description." });

            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Third Monster", Level = 3, CurrentExperience = 30,
                MaximumHealth = 10, CurrentHealth = 10, Attack = 10, Defense = 10, Speed = 3, Description = "This is an Monster description." });

            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Fourth Monster", Level = 4, CurrentExperience = 40,
                MaximumHealth = 10, CurrentHealth = 10, Attack = 10, Defense = 10, Speed = 4, Description = "This is an Monster description." });

            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Fifth Monster", Level = 5, CurrentExperience = 50,
                MaximumHealth = 10, CurrentHealth = 10, Attack = 10, Defense = 10, Speed = 5, Description = "This is an Monster description." });

            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "Sixth Monster", Level = 6, CurrentExperience = 60,
                MaximumHealth = 60, CurrentHealth = 60, Attack = 60, Defense = 60, Speed = 6, Description = "This is an Monster description." });


            //Score Seed Data
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "First Score", ScoreTotal = 111 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Second Score", ScoreTotal = 222 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Third Score", ScoreTotal = 333 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Fourth Score", ScoreTotal = 444 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Fifth Score", ScoreTotal = 555 });
            await AddAsync_Score(new Score { Id = Guid.NewGuid().ToString(), Name = "Sixth Score", ScoreTotal = 666 });

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
        public async Task<bool> RecruitAsync_Character(Character data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Character>> GetPartyAsync_Character(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Character>().ToListAsync();

            return result;
        }
        public async Task<bool> DeleteAsync_CharacterParty(Character data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
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

        //monster party
        public async Task<bool> RecruitAsync_Monster(Monster data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }
       public async  Task<IEnumerable<Monster>> GetPartyAsync_Monster(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Monster>().ToListAsync();
            return result;
        }
        public async Task<bool> DeleteAsync_MonsterParty(Monster data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
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