using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;

    // Noms des paramètres exacts tels qu'ils sont écrits dans l'Animator
    private string hasGunParameter = "HasGun";
    private string shootParameter = "Shoot";

    void Start()
    {
        // On récupère automatiquement l'Animator attaché au personnage
        animator = GetComponent<Animator>();

        // Optionnel : On peut décider d'équiper l'arme dès le début
        // EquipGun(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleGun();
        }

        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }

    public void EquipGun(bool isEquipped)
    {
        animator.SetBool(hasGunParameter, isEquipped);
    }

    private void ToggleGun()
    {
        bool isCurrentlyArmed = animator.GetBool(hasGunParameter);
        EquipGun(!isCurrentlyArmed);
    }

    public void FireWeapon()
    {
        if (animator.GetBool(hasGunParameter) == true)
        {
            animator.SetTrigger(shootParameter);
        }
    }
}