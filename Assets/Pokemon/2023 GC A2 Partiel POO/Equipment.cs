﻿
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Défintion simple d'un équipement apportant des boost de stats
    /// </summary>
    public class Equipment
    {
        public Equipment(int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed, bool hasPriority = false)
        {
            BonusHealth = bonusHealth;
            BonusAttack = bonusAttack;
            BonusDefense = bonusDefense;
            BonusSpeed = bonusSpeed;
            HasPriority = hasPriority;
        }

        public int BonusHealth { get; protected set; }
        public int BonusAttack { get; protected set; }
        public int BonusDefense { get; protected set; }
        public int BonusSpeed { get; protected set; }
        public bool HasPriority { get; protected set; }
    }
}
