using UnityEngine;

public class PlagueRemedy : MonoBehaviour
{
    [Header("Remedy Settings")]
    public float healthRestored = 20f;
    public float infectionReduced = 15f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Heal player health if available
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Soigner(healthRestored);
            }

            // Lower infection if available
            PlayerInfection infection = other.GetComponent<PlayerInfection>();
            if (infection != null)
            {
                infection.ReduireInfection(infectionReduced);
            }

            Destroy(gameObject);
        }
    }
}