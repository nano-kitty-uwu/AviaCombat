using UnityEngine;

public class CombatSystem : MonoBehaviour
{
	[SerializeField]ParticleSystem m_ParticleSystem;
	private void Awake()
	{
		m_ParticleSystem.Stop();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		m_ParticleSystem.Play();
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		m_ParticleSystem.Stop();
	}

}
