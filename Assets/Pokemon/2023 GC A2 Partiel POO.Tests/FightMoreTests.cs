using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        [Test]
        public void HealDoesNotExceedMaxHealth()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var punch = new Punch();

            c.ReceiveAttack(punch);
            c.Heal(100);
            Assert.That(c.CurrentHealth, Is.EqualTo(100));
        }

        [Test]
        public void HPCurrentFollowsHPMaxWhenLowered() 
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var e = new Equipment(-20, 90, 70, 12);

            c.Equip(e);
            Assert.That(c.MaxHealth, Is.EqualTo(80));
            Assert.That(c.CurrentHealth, Is.EqualTo(80));
        }

        [Test]
        public void PriorityEquipmentWorksCorrectly()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var e = new Equipment(0, 0, 0, 0, true);
            c.Equip(e);
            Assert.That(c.HasPriority, Is.EqualTo(true));
            c.Unequip();
            Assert.That(c.HasPriority, Is.EqualTo(false));
        }

        [Test]
        public void StatusEffectsWorkCorrectly()
        {
            var c = new Character(100, 50, 30, 20, TYPE.NORMAL);
            var fight = new Fight(c, new Character(100, 50, 30, 20, TYPE.NORMAL));

            // Test BURN status
            var burn = StatusEffect.GetNewStatusEffect(StatusPotential.BURN);
            c.ApplyStatus(burn);
            Assert.That(c.CurrentStatus, Is.EqualTo(burn));
            fight.EndTurn();
            Assert.That(c.CurrentHealth, Is.EqualTo(90)); // Assuming burn does 10 damage per turn
            fight.EndTurn();
            Assert.That(c.CurrentHealth, Is.EqualTo(80));
            fight.EndTurn();
            Assert.That(c.CurrentHealth, Is.EqualTo(70));
            Assert.That(c.CurrentStatus, Is.EqualTo(null));

            // Test SLEEP status
            var sleep = StatusEffect.GetNewStatusEffect(StatusPotential.SLEEP);
            c.ApplyStatus(sleep);
            Assert.That(c.CurrentStatus, Is.EqualTo(sleep));
            fight.EndTurn();
            Assert.That(c.CurrentStatus.CanAttack, Is.EqualTo(false));
            fight.EndTurn();
            Assert.That(c.CurrentStatus, Is.EqualTo(null));

            // Test CRAZY status
            var crazy = StatusEffect.GetNewStatusEffect(StatusPotential.CRAZY);
            c.ApplyStatus(crazy);
            Assert.That(c.CurrentStatus, Is.EqualTo(crazy));
            fight.EndTurn();
            Assert.That(c.CurrentHealth, Is.EqualTo(70 - (int)(c.Attack * crazy.DamageOnAttack))); // Assuming crazy does a portion of attack as damage
            Assert.That(c.CurrentStatus, Is.EqualTo(null));
        }
    }
}






// Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
// À présent c'est à toi de créer des features et les TU sur le reste du projet

// Ce que tu peux ajouter:
// - Ajouter davantage de sécurité sur les tests apportés
// ✓ un heal ne régénère pas plus que les HP Max 
// ✓ si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
// ✓ ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
// ✓ Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
// - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
// - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type
// - L'utilisation d'objets : Potion, SuperPotion, Vitess+, Attack+ etc.
// - Gérer les PP (limite du nombre d'utilisation) d'une attaque,
// si on selectionne une attack qui a 0 PP on inflige 0

// Choisis ce que tu veux ajouter comme feature et fait en au max.
// Les nouveaux TU doivent être dans ce fichier.
// Modifiant des features il est possible que certaines valeurs
// des TU précédentes ne matchent plus, tu as le droit de réadapter les valeurs
// de ces anciens TU pour ta nouvelle situation.
