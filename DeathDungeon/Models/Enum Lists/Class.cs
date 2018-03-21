using System;
using System.Collections.Generic;
using System.Linq;

namespace DeathDungeon.Models
{
    public enum ClassEnum
    {
        undefined = 0,
        Warrior = 1,
        Wizard = 2,
        Rogue = 3,
        Cleric = 4,
        Ranger = 5,
        Druid = 6
    }

    public static class ClassList
    {
        //Convert Enum to String
        public static List<string> GetListClass
        {
            get
            {
                var myList = Enum.GetNames(typeof(ClassEnum)).ToList();
                var myReturn = myList.Where(a =>
                                    a.ToString() != ClassEnum.undefined.ToString()
                                    ).ToList();
                return myReturn;

            }
        }


        public static List<string> GetClassListCharacter
        {
            get
            {
                var myList = Enum.GetNames(typeof(ClassEnum)).ToList();
                var myReturn = myList.Where(a =>
                                                a.ToString() != ClassEnum.undefined.ToString()
                                            ).ToList();
                return myReturn;
            }
        }

        //Return Enum as string
        public static ClassEnum ConvertStringToEnum(string value)
        {
            return (ClassEnum)Enum.Parse(typeof(ClassEnum), value);
        }

    }
}
