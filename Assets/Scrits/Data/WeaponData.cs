using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
	[Header("Base Stats")]
	public int baseDamage = 10; // Base damage of the weapon
	public float fireRate = 1f; // Shots per second
	public float projectileSpeed = 20f; // Speed of the projectiles

	[Header("Scaling")]
	public int damageIncreasePerLevel = 5; // Damage increase per upgrade level
	public float fireRateIncreasePerLevel = 0.1f; // Fire rate increase per upgrade level

	[Header("Behavior")]
	public float range = 15f; // Maximum range of the weapon
	public float spreadAngle = 5f; // Spread angle for projectiles (for shotguns, etc.)
}
