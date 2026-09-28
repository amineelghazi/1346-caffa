using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class NextLevel : MonoBehaviour
{
    [Header("Destination")]
    [Tooltip("Nom exact de la scène du niveau 2 (doit être dans les Build Settings).")]
    [SerializeField] private string nomSceneSuivante = "Niveau2";

    [Header("Activation")]
    [Tooltip("Si coché, le joueur doit appuyer sur la touche pour entrer. Sinon, il suffit de toucher la porte.")]
    [SerializeField] private bool toucheRequise = true;
    [SerializeField] private KeyCode toucheInteraction = KeyCode.E;

    private bool joueurDansLaZone;
    private bool chargementEnCours;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (!toucheRequise || !joueurDansLaZone || chargementEnCours) return;

        if (Input.GetKeyDown(toucheInteraction))
        {
            ChargerNiveau();
        }
    }

    private void OnTriggerEnter2D(Collider2D autre)
    {
        if (!autre.CompareTag("Player")) return;

        joueurDansLaZone = true;

        if (!toucheRequise)
        {
            ChargerNiveau();
        }
    }

    private void OnTriggerExit2D(Collider2D autre)
    {
        if (autre.CompareTag("Player"))
        {
            joueurDansLaZone = false;
        }
    }

    private void ChargerNiveau()
    {
        chargementEnCours = true;
        SceneManager.LoadScene(nomSceneSuivante);
    }
}