using UnityEngine;
using System.Collections.Generic;

public class ProceduralTerrain : MonoBehaviour
{
	[Header("Terrain Settings")]
	public int textureWidth = 256;
	public int textureHeight = 256;
	public float scale = 20f;
	public float offsetX = 100f;
	public float offsetY = 100f;

	[Header("Color Settings")]
	public Color waterColor = new Color(0, 0, 1);
	public Color grassColor = new Color(0, 1, 0);
	public Color mountainColor = new Color(0.5f, 0.35f, 0.05f);
	public Color snowColor = new Color(1, 1, 1);

	[Header("Height Thresholds")]
	public float waterLevel = 0.3f;
	public float grassLevel = 0.5f;
	public float mountainLevel = 0.8f;

	[Header("Cloud Settings")]
	public bool generateClouds = true;
	public float cloudScale = 5f;
	public float cloudThreshold = 0.6f;
	public Color cloudColor = new Color(1, 1, 1, 0.8f);
	public float cloudOffsetX = 50f;
	public float cloudOffsetY = 50f;
	[Range(0, 1)] public float cloudCoverage = 0.3f;

	private Texture2D terrainTexture;
	private Texture2D cloudTexture;

	void Start()
	{
		// Generate the terrain texture
		terrainTexture = GenerateTexture();

		// Generate cloud texture if enabled
		if (generateClouds)
		{
			cloudTexture = GenerateCloudTexture();
		}

		// Apply the textures to a Quad
		ApplyTexturesToQuad();
	}

	Texture2D GenerateTexture()
	{
		Texture2D texture = new Texture2D(textureWidth, textureHeight);

		for (int x = 0; x < textureWidth; x++)
		{
			for (int y = 0; y < textureHeight; y++)
			{
				float xCoord = (float)x / textureWidth * scale + offsetX;
				float yCoord = (float)y / textureHeight * scale + offsetY;
				float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

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

				texture.SetPixel(x, y, pixelColor);
			}
		}

		texture.Apply();
		return texture;
	}

	Texture2D GenerateCloudTexture()
	{
		Texture2D texture = new Texture2D(textureWidth, textureHeight);

		for (int x = 0; x < textureWidth; x++)
		{
			for (int y = 0; y < textureHeight; y++)
			{
				// Multi-layer noise for more interesting shapes
				float noise1 = Mathf.PerlinNoise(
					x * 0.01f + cloudOffsetX,
					y * 0.01f + cloudOffsetY);

				float noise2 = Mathf.PerlinNoise(
					x * 0.05f + cloudOffsetX + 100f,
					y * 0.05f + cloudOffsetY + 100f);

				float noise3 = Mathf.PerlinNoise(
					x * 0.02f + cloudOffsetX + 200f,
					y * 0.02f + cloudOffsetY + 200f);

				// Combine noises with different weights
				float combinedNoise = noise1 * 0.5f + noise2 * 0.35f + noise3 * 0.15f;

				// Apply threshold and boost visibility
				float cloudValue = Mathf.Pow(Mathf.Clamp01((combinedNoise - 0.4f) * 2.5f), 1.5f);

				// Add edge darkening for more definition
				float edgeFactor = Mathf.Min(
					Mathf.Min(x, textureWidth - x),
					Mathf.Min(y, textureHeight - y)) / (textureWidth * 0.1f);
				edgeFactor = Mathf.Clamp01(edgeFactor);
				cloudValue *= edgeFactor;

				// Set pixel with enhanced contrast
				Color cloudPixel = new Color(
					cloudColor.r,
					cloudColor.g,
					cloudColor.b,
					cloudValue * 0.9f); // Slightly reduced alpha for natural look

				texture.SetPixel(x, y, cloudPixel);
			}
		}

		texture.Apply();
		return texture;
	}

	void ApplyTexturesToQuad()
	{
		GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
		quad.transform.position = new Vector3(0, 0, 10);
		quad.transform.localScale = new Vector3(textureWidth / 10f, textureHeight / 10f, 1);

		Material material = new Material(Shader.Find("Unlit/Transparent"));
		material.mainTexture = terrainTexture;

		if (generateClouds)
		{
			// Create a second quad for clouds
			GameObject cloudQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			cloudQuad.transform.position = new Vector3(0, 0, 9); // Slightly in front of terrain
			cloudQuad.transform.localScale = new Vector3(textureWidth / 10f, textureHeight / 10f, 1);

			Material cloudMaterial = new Material(Shader.Find("Unlit/Transparent"));
			cloudMaterial.mainTexture = cloudTexture;
			cloudQuad.GetComponent<Renderer>().material = cloudMaterial;
		}

		quad.GetComponent<Renderer>().material = material;
	}
}