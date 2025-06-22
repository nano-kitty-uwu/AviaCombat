using UnityEngine;

public class CloudMovement : MonoBehaviour
{
	public float scrollSpeedX = 0.1f;
	public float scrollSpeedY = 0.05f;
	private Material cloudMaterial;
	private Vector2 offset;

	void Start()
	{
		cloudMaterial = GetComponent<Renderer>().material;
		offset = Vector2.zero;
	}

	void Update()
	{
		offset.x += scrollSpeedX * Time.deltaTime;
		offset.y += scrollSpeedY * Time.deltaTime;
		cloudMaterial.mainTextureOffset = offset;
	}
}