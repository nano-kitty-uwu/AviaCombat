using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[Header("Pool Settings")]
	[SerializeField] private GameObject prefab; // Prefab to pool
	[SerializeField] private int poolSize = 10; // Initial size of the pool

	private Queue<GameObject> pool = new Queue<GameObject>();

	private void Start()
	{
		InitializePool();
	}

	// Initialize the pool with objects
	private void InitializePool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			GameObject obj = Instantiate(prefab, transform);
			obj.SetActive(false);
			pool.Enqueue(obj);
		}
	}

	// Get an object from the pool
	public GameObject GetObject()
	{
		if (pool.Count == 0)
		{
			// Expand the pool if empty
			GameObject obj = Instantiate(prefab, transform);
			return obj;
		}

		GameObject pooledObject = pool.Dequeue();
		pooledObject.SetActive(true);
		return pooledObject;
	}

	// Return an object to the pool
	public void ReturnObject(GameObject obj)
	{
		obj.SetActive(false);
		pool.Enqueue(obj);
	}
}