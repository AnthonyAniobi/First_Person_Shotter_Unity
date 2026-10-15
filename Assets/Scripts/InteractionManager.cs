using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    static public InteractionManager instance {get; private set;}

    Weapon currentWeapon;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        LookAtWeapon();
        if(CustomInputManager.instance.PickUpWeaponInput())
        {
            Debug.Log("PickUpWeaponInput() detected in InteractionManager");
            PickupWeapon();
        }
    }

    void LookAtWeapon()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(
            ray,
            out RaycastHit hit
             ))
        {
            GameObject hitObject = hit.transform.gameObject;
            Weapon hitWeapon;
            if(hitObject.TryGetComponent(out hitWeapon))
            {
                currentWeapon = hitWeapon;
                currentWeapon.GetComponent<Outline>().enabled = true;
            }
            else
            {
                if(currentWeapon != null)
                {
                    currentWeapon.GetComponent<Outline>().enabled = false;
                    currentWeapon = null;
                }
            }
        }

        Debug.DrawRay(
            ray.origin,
            ray.direction * 100f,
            Color.red
        );
    }


    void PickupWeapon()
    {
        Debug.Log("PickupWeapon() called");
        if(currentWeapon != null)
        {
            Debug.Log($"Picked up {currentWeapon.name}");
            currentWeapon.GetComponent<Outline>().enabled = false;
            WeaponManager.instance.AddWeapon(currentWeapon);
        }
    }
}
