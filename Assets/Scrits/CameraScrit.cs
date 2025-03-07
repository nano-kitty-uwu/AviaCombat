using UnityEngine;

public class CameraScrit : MonoBehaviour
{
	public Transform player; // Reference to the player's transform
	public Vector2 offset = new Vector2(0f, 0f); // Offset from the player's position
	public float minSmoothSpeed = 0.1f; // Minimum smooth speed
	public float maxSmoothSpeed = 0.5f; // Maximum smooth speed
	public float speedMultiplier = 0.1f; // How much the player's speed affects the smooth speed
	public float deadzoneRadius = 1f; // Distance from the center where the camera won't move

	private Vector3 velocity = Vector3.zero;
	private Rigidbody2D playerRb; // Reference to the player's Rigidbody2D

	void Start()
	{
		if (player == null)
		{
			Debug.LogWarning("Player reference is missing!");
			return;
		}

		// Get the player's Rigidbody2D component
		playerRb = player.GetComponent<Rigidbody2D>();
		if (playerRb == null)
		{
			Debug.LogWarning("Player does not have a Rigidbody2D component!");
		}
	}

	void LateUpdate()
	{
		if (player == null || playerRb == null)
		{
			return;
		}

		// Calculate the desired position with offset
		Vector3 desiredPosition = new Vector3(
			player.position.x + offset.x,
			player.position.y + offset.y,
			transform.position.z // Keep the camera's original Z position
		);

		// Calculate the distance between the camera and the desired position
		float distanceToPlayer = Vector3.Distance(transform.position, desiredPosition);

		// Only move the camera if the player is outside the deadzone
		if (distanceToPlayer > deadzoneRadius)
		{
			// Calculate the player's speed
			float playerSpeed = playerRb.linearVelocity.magnitude;

			// Adjust the smooth speed based on the player's speed
			float smoothSpeed = Mathf.Lerp(minSmoothSpeed, maxSmoothSpeed, playerSpeed * speedMultiplier);

			// Smoothly interpolate between the current position and the desired position
			Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

			// Update the camera's position
			transform.position = smoothedPosition;
		}
	}

	// Optional: Visualize the deadzone in the editor
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, deadzoneRadius);
	}
}