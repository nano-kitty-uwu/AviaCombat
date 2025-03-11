using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
	[SerializeField] float speed = 10f; // Constant forward speed
	[SerializeField] float rotationSpeed = 10f; // Speed of rotation toward the joystick direction

	[SerializeField] Joystick joystick; // Reference to the joystick
	[SerializeField] Rigidbody2D rb; // Reference to the Rigidbody2D component

	private Vector2 moveDir; // Stores the joystick input direction

	private void FixedUpdate()
	{
		// Get the joystick direction
		moveDir = joystick.Direction;

		// Rotate the player toward the joystick direction
		if (moveDir.magnitude > 0.1f) // Deadzone to prevent tiny inputs
		{
			// Calculate the target rotation based on the joystick direction
			float targetAngle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 90f;
			Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

			// Smoothly rotate toward the target rotation
			rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime).eulerAngles.z;
		}

		// Move the player forward in its local forward direction
		Vector2 forwardDirection = transform.up; // Local forward direction in 2D
		rb.linearVelocity = forwardDirection * speed;
	}
}