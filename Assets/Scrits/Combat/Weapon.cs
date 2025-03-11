using Mono.Cecil;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] int damageAmount;
	private void OnParticleCollision(GameObject other)
	{
		IDamageable damageable= other.GetComponent<IDamageable>();
		if (damageable != null)
		{
			damageable.TakeDamage(damageAmount); // Apply damage
		}
	}
}
