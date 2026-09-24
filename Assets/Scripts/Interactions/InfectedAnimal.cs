using UnityEngine;

public class InfectedAnimal : MonoBehaviour
{
    [Header("Animal Stats")]
    public float maxHealth = 1f;
    private float currentHealth;

    [Header("Infection Status")]
    [Tooltip("Percentage chance this sheep is infected (0.5 = 50%)")]
    [Range(0f, 1f)]
    public float infectionChance = 0.5f;
    public bool isInfected;
    public bool hasBeenInspected = false;

    [Header("Loot Drop")]
    [Tooltip("Drag your Potion/Remedy Prefab here")]
    public GameObject remedyPrefab;

    [Header("UI Prompt (Optional)")]
    public GameObject inspectPromptUI;

    private bool playerInRange = false;

    private void Start()
    {
        currentHealth = maxHealth;
        // Randomly decide if this individual animal is infected
        isInfected = Random.value <= infectionChance;

        if (inspectPromptUI != null)
            inspectPromptUI.SetActive(false);
    }

    private void Update()
    {
        // Press 'E' to inspect when near the sheep
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InspectSheep();
        }
    }

    public void InspectSheep()
    {
        hasBeenInspected = true;

        if (isInfected)
        {
            Debug.Log("<color=purple>[INSPECTION]</color> This sheep shows clear signs of the Black Death!");
        }
        else
        {
            Debug.Log("<color=green>[INSPECTION]</color> This sheep is healthy.");
        }

        if (inspectPromptUI != null)
            inspectPromptUI.SetActive(false);
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
        // Only infected animals drop remedies when killed
        if (isInfected && remedyPrefab != null)
        {
            Instantiate(remedyPrefab, transform.position, Quaternion.identity);
            Debug.Log("Infected animal slain: Remedy harvested!");
        }
        else if (!isInfected)
        {
            Debug.Log("Healthy animal slain: No remedy found.");
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (inspectPromptUI != null && !hasBeenInspected)
                inspectPromptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (inspectPromptUI != null)
                inspectPromptUI.SetActive(false);
        }
    }
}