﻿using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;
            CurrentHealth = baseHealth; // Initialiser CurrentHealth
        }

        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; set; }
        public TYPE BaseType { get => _baseType; }

        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                return _baseHealth + (CurrentEquipment?.BonusHealth ?? 0);
            }
        }

        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                return _baseAttack + (CurrentEquipment?.BonusAttack ?? 0);
            }
        }

        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                return _baseDefense + (CurrentEquipment?.BonusDefense ?? 0);
            }
        }

        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                return _baseSpeed + (CurrentEquipment?.BonusSpeed ?? 0);
            }
        }

        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }

        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive => CurrentHealth > 0;
        public bool HasPriority => CurrentEquipment?.HasPriority ?? false;

        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        public void ReceiveAttack(Skill s)
        {
            int damage = s.Power - Defense;
            if (damage > 0)
            {
                CurrentHealth -= damage;
                if (CurrentHealth < 0)
                {
                    CurrentHealth = 0;
                }
            }
        }

        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
            {
                throw new ArgumentNullException(nameof(newEquipment));
            }

            CurrentEquipment = newEquipment;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            CurrentEquipment = null;
        }

        /// <summary>
        /// Soigne le personnage
        /// </summary>
        /// <param name="amount">Quantité de HP à régénérer</param>
        public void Heal(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }

        /// <summary>
        /// Applique un statut au personnage
        /// </summary>
        /// <param name="status">Statut à appliquer</param>
        public void ApplyStatus(StatusEffect status)
        {
            CurrentStatus = status;
        }

        /// <summary>
        /// Applique les effets de statut au personnage
        /// </summary>
        public void ApplyEffect()
        {
            if (CurrentStatus != null)
            {
                CurrentStatus.ApplyEffect(this);

                CurrentStatus.RemainingTurn--;
                if (CurrentStatus.RemainingTurn <= 0)
                {
                    CurrentStatus = null;
                }
            }
        }
    }
}
