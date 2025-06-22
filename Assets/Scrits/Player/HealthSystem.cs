using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
	public int health { get; set; }
	public int maxHealth { get;  set; } = 100;

	private void Start()
	{
		health = maxHealth; // Initialize health to max health
	}

	public void TakeDamage(int damageAmount)
	{
		health -= damageAmount;
		Debug.Log($"Player took {damageAmount} damage. Current health: {health}");

		if (health <= 0)
		{
			Die();
		}
	}

	/*public void Heal(int healAmount)
	{
		health += healAmount;
		health = Mathf.Min(health, maxHealth); // Ensure health doesn't exceed max health
		Debug.Log($"Player healed for {healAmount}. Current health: {health}");
	}
	*/
	public void Die()
	{
		Debug.Log("Player has died.");
		Destroy(gameObject);
	}
}