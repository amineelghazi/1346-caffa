using UnityEngine;

public class InfectedAnimal : MonoBehaviour
{
    [Header("Animal Stats")]
    public float maxHealth = 1f;
    private float currentHealth;

    [Header("Loot Drop")]
    [Tooltip("Drag your Potion/Remedy Prefab here")]
    public GameObject remedyPrefab;

    [Tooltip("Chance to drop a remedy when killed (1.0 = 100%)")]
    [Range(0f, 1f)]
    public float dropChance = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Drop the remedy item if a prefab is assigned
        if (remedyPrefab != null && Random.value <= dropChance)
        {
            Instantiate(remedyPrefab, transform.position, Quaternion.identity);
        }

        Debug.Log(gameObject.name + " was killed by the Doctor!");
        Destroy(gameObject);
    }
}