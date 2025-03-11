using UnityEngine;

public class Fighter : Enemy
{
	[Header("Fighter Settings")]
	[SerializeField] private float chaseSpeed = 5f; // Speed at which the fighter chases the player
	[SerializeField] private float attackRange = 10f; // Range at which the fighter starts shooting

	private Transform player;

	private void Start()
	{
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
	}

	private void ChasePlayer()
	{
		Vector3 direction = (player.position - transform.position).normalized;
		transform.position += direction * chaseSpeed * Time.deltaTime;
	}
}