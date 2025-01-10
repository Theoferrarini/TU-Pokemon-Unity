
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            Character1 = character1 ?? throw new ArgumentNullException(nameof(character1));
            Character2 = character2 ?? throw new ArgumentNullException(nameof(character2));
        }

        public Character Character1 { get; }
        public Character Character2 { get; }

        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontrée ?
        /// </summary>
        public bool IsFightFinished => !Character1.IsAlive || !Character2.IsAlive;

        /// <summary>
        /// Jouer l'enchaînement des attaques. Attention à bien gérer l'ordre des attaques par rapport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque sélectionnée par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque sélectionnée par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            if (skillFromCharacter1 == null) throw new ArgumentNullException(nameof(skillFromCharacter1));
            if (skillFromCharacter2 == null) throw new ArgumentNullException(nameof(skillFromCharacter2));

            if (Character1.Speed >= Character2.Speed)
            {
                if (Character1.CurrentStatus == null || Character1.CurrentStatus.CanAttack)
                {
                    Character2.ReceiveAttack(skillFromCharacter1);
                }
                if (Character2.IsAlive && (Character2.CurrentStatus == null || Character2.CurrentStatus.CanAttack))
                {
                    Character1.ReceiveAttack(skillFromCharacter2);
                }
            }
            else
            {
                if (Character2.CurrentStatus == null || Character2.CurrentStatus.CanAttack)
                {
                    Character1.ReceiveAttack(skillFromCharacter2);
                }
                if (Character1.IsAlive && (Character1.CurrentStatus == null || Character1.CurrentStatus.CanAttack))
                {
                    Character2.ReceiveAttack(skillFromCharacter1);
                }
            }
        }

        /// <summary>
        /// Termine le tour et applique les effets de statut
        /// </summary>
        public void EndTurn()
        {
            Character1.ApplyEffect();
            Character2.ApplyEffect();
        }
    }
}
