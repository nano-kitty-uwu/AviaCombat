using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
	[Header("Terrain Settings")]
	public int textureWidth = 256; // Width of the texture
	public int textureHeight = 256; // Height of the texture
	public float scale = 20f; // Scale of the Perlin Noise
	public float offsetX = 100f; // X offset for Perlin Noise
	public float offsetY = 100f; // Y offset for Perlin Noise

	[Header("Color Settings")]
	public Color waterColor = new Color(0, 0, 1); // Blue for water
	public Color grassColor = new Color(0, 1, 0); // Green for grass
	public Color mountainColor = new Color(0.5f, 0.35f, 0.05f); // Brown for mountains
	public Color snowColor = new Color(1, 1, 1); // White for snow

	[Header("Height Thresholds")]
	public float waterLevel = 0.3f; // Height threshold for water
	public float grassLevel = 0.5f; // Height threshold for grass
	public float mountainLevel = 0.8f; // Height threshold for mountains

	private Texture2D terrainTexture;

	void Start()
	{
		// Generate the terrain texture
		terrainTexture = GenerateTexture();

		// Apply the texture to a Quad
		ApplyTextureToQuad();
	}

	Texture2D GenerateTexture()
	{
		// Create a new texture
		Texture2D texture = new Texture2D(textureWidth, textureHeight);

		// Generate the texture pixels
		for (int x = 0; x < textureWidth; x++)
		{
			for (int y = 0; y < textureHeight; y++)
			{
				// Calculate Perlin Noise value for this position
				float xCoord = (float)x / textureWidth * scale + offsetX;
				float yCoord = (float)y / textureHeight * scale + offsetY;
				float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

				// Determine the color based on the noise value
				Color pixelColor;
				if (noiseValue < waterLevel)
				{
					pixelColor = waterColor;
				}
				else if (noiseValue < grassLevel)
				{
					pixelColor = grassColor;
				}
				else if (noiseValue < mountainLevel)
				{
					pixelColor = mountainColor;
				}
				else
				{
					pixelColor = snowColor;
				}

				// Set the pixel color
				texture.SetPixel(x, y, pixelColor);
			}
		}

		// Apply the changes to the texture
		texture.Apply();
		return texture;
	}

	void ApplyTextureToQuad()
	{
		// Create a Quad
		GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
		quad.transform.position = new Vector3(0, 0, 10); // Position the Quad below the plane
		quad.transform.localScale = new Vector3(textureWidth / 10f, textureHeight / 10f, 1); // Scale the Quad

		// Apply the texture to the Quad's material
		Renderer quadRenderer = quad.GetComponent<Renderer>();
		quadRenderer.material = new Material(Shader.Find("Unlit/Texture"));
		quadRenderer.material.mainTexture = terrainTexture;
	}
}