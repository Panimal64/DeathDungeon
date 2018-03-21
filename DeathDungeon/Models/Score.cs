
using System;
using System.Collections.Generic;
using SQLite;

namespace DeathDungeon.Models
{
    public class Score
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }

        public int ScoreTotal { get; set; }

        public int Turn { get; set; }

        public int Round { get; set; }

        // The Date the game played, and when the score was saved
        public DateTime GameDate { get; set; }

        // Tracks if auto battle is true, or if user battle = false
        public bool AutoBattle { get; set; }

        // The number of turns the battle took to finish

        // Missign property, to add as part of the SQL lecture...
        //public int TurnNumber { get; set; }

        // The count of monsters slain during battle
        public int MonsterSlainNumber { get; set; }

        // The total experience points all the characters received during the battle
        public int ExperienceGainedTotal { get; set; }

        // A list of all the characters at the time of death and their stats.  Needs to be in json format, so saving a string
        public string CharacterAtDeathList { get; set; }

        public List<Character> DeadCharacters = new List<Character>();

        // All of the monsters killed and their stats. Needs to be in json format, so saving as a string
        public string MonstersKilledList { get; set; }

        public List<Monster> DeadMonsters = new List<Monster>();

        // All of the items dropped and their stats. Needs to be in json format, so saving as a string
        public string ItemsDroppedList { get; set; }

        public Score()
        {
            GameDate = DateTime.Now;    // Set to be now by default.
            AutoBattle = false;         //assume user battle
            MonsterSlainNumber = 0; 
            ItemsDroppedList = null;
            ExperienceGainedTotal = 0;
            Round = 1;
            Turn = 0;
        }

        public void Update(Score newData)
        {
            if (newData == null)
            {
                return;
            }

            // Update all the fields in the Data, except for the Id
            Name = newData.Name;
            ScoreTotal = ScoreTotal;
        }
    }
}