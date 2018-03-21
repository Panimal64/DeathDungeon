using System;
using System.Collections.Generic;
using System.Text;

namespace DeathDungeon.Models
{
    public static class GlobalVariables
    {
        //sets autoplay on sql change to accept new sql values
        public static bool auto_battle_reset = false;
        // Turn on to force Rolls to be non random
        public static bool ForceRollsToNotRandom = false;

        // What number should return for random numbers 
        public static int ForcedRandomValue;

        // What number to use for HitChance values (default is -1 for off)
        public static int ForceToHitValue = -1;

        //Critical miss's are off
        public static bool critMiss = false;

        //Critical hit's off
        public static bool critHit = false;

        //Holds sum of character levels to find average
        public static int CharacterLevelSum = 0;

        //Holds sum of character health to find average
        public static int CharacterHealthSum = 0;

        //Holds sum of character attack to find average
        public static int CharacterAttackSum = 0;

        //Holds sum of character defense to find average
        public static int CharacterDefenseSum = 0;

        //Holds sum of character speed to find average
        public static int CharacterSpeedSum = 0;

        //Hold average level of characters for scaling
        public static int AverageLevel = 0;

        //Hold avgerage health of charactes to use for scaling 
        public static int AverageHealth = 0;

        //Hold average attack of characters for scaling
        public static int AverageAttack = 0;

        //Hold average defense of characters for scaling
        public static int AverageDefense = 0;

        //Hold average speed of characters for scaling
        public static int AverageSpeed = 0;         
    }
}
