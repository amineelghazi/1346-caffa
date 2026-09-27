using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Configuration")]
    public Transform weaponHolder; // Glisse le WeaponHolder ici depuis l'inspecteur
    public GameObject startingWeaponPrefab; // Glisse le Prefab de ton arme ici

    private GameObject currentWeapon;

    void Start()
    {
        // On équipe l'arme dès le début du jeu
        EquipWeapon(startingWeaponPrefab);
    }

    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        // 1. Détruire l'arme actuelle s'il y en a une
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        // 2. Si on a bien une arme à équiper
        if (newWeaponPrefab != null)
        {
            // Instancier la nouvelle arme à la position et rotation du holder
            currentWeapon = Instantiate(newWeaponPrefab, weaponHolder.position, weaponHolder.rotation);

            // Attacher l'arme au holder pour qu'elle suive le joueur
            currentWeapon.transform.SetParent(weaponHolder);

            // S'assurer que l'échelle est correcte (parfois modifiée lors de l'instanciation)
            currentWeapon.transform.localScale = Vector3.one;
        }
    }
}