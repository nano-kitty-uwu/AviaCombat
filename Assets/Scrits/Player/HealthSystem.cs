using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
	public int Health { get; set; }
	public int MaxHealth { get;  set; } = 100;

	private void Start()
	{
		Health = MaxHealth; // Initialize health to max health
	}

	public void TakeDamage(int damageAmount)
	{
		Health -= damageAmount;
		Debug.Log($"Player took {damageAmount} damage. Current health: {Health}");

		if (Health <= 0)
		{
			Die();
		}
	}

	public void Heal(int healAmount)
	{
		Health += healAmount;
		Health = Mathf.Min(Health, MaxHealth); // Ensure health doesn't exceed max health
		Debug.Log($"Player healed for {healAmount}. Current health: {Health}");
	}

	public void Die()
	{
		Debug.Log("Player has died.");
		// Handle player death (e.g., respawn, game over, etc.)
	}
}