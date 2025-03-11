using UnityEngine;

public interface IDamageable
{
	int Health { get; set; } // Current health
	int MaxHealth { get; }   // Maximum health

	void TakeDamage(int damageAmount); // Method to handle taking damage
	void Heal(int healAmount);         // Method to handle healing
	void Die();                        // Method to handle death
}
