using UnityEngine;

public class Fighter : Enemy
{
	[Header("Fighter Settings")]
	[SerializeField] private float chaseSpeed = 5f; // Speed at which the fighter chases the player
	[SerializeField] private float attackRange = 10f; // Range at which the fighter starts shooting
	[SerializeField] private float rotationSpeed = 5f;
	Rigidbody2D rb;
	Vector2 direction;

	private Transform player;

	private void Start()
	{
		rb=GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
	}

	private void Update()
	{
		if (player == null) return;
		// Chase the player if within attack range
		float distanceToPlayer = Vector3.Distance(transform.position, player.position);
		if (distanceToPlayer <= attackRange)
		{
			ChasePlayer();
		}
		else Fly();
	}

	private void ChasePlayer()
	{
		if (player == null) return;

		// Calculate the direction to the player
		Vector2 direction = (player.position - transform.position).normalized;

		// Calculate the angle to rotate towards the player
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Subtract 90 degrees for proper alignment in 2D

		// Create the target rotation
		Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

		// Smoothly rotate towards the player
		rb.rotation = Quaternion.Slerp(Quaternion.Euler(0, 0, rb.rotation), targetRotation, rotationSpeed * Time.fixedDeltaTime).eulerAngles.z;

		// Move the enemy towards the player
		rb.linearVelocity = direction * chaseSpeed;
	}
	private void Fly()
	{
		rb.linearVelocity = transform.up * chaseSpeed;

	}
}