using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    static public WeaponManager instance { get; private set; }
    [SerializeField] private int totalWeapons = 2;
    [SerializeField] private GameObject weaponHolder;
    internal Weapon CurrentWeapon => weapons[0];
    public Weapon[] weapons;

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

    void Start()
    {
        this.weapons = new Weapon[totalWeapons];
    }

    internal void AddWeapon(Weapon weapon)
    {
        if(weapons.Length == 0)
        {
            weapons[0] = weapon;
        }else if(weapons.Length < totalWeapons)
        {
            // move previous weapons down the line and add new weapon to the top of the stack
            Weapon tempWeapon = weapon;
            tempWeapon.animator.enabled = true;
            for(int i = 0; i < weapons.Length; i++)
            {
                Weapon currentWeapon = weapons[i];
                currentWeapon.gameObject.SetActive(false);
                currentWeapon.animator.enabled = false;
                weapons[i] = tempWeapon;
                tempWeapon = currentWeapon;
            }
            PlaceWeaponInWorld(weapon);
        }
        else
        {
            // weapons array is full, replace the current weapon with the new one
            Weapon currentWeapon = weapons[0];
            // put the current weapon in same position and rotation as the new weapon, then drop it in the world
            DropWeapon(currentWeapon, currentWeapon.transform.position, currentWeapon.transform.rotation);
            // add the new weapon to the top of the stack
            weapons[0] = weapon;
            PlaceWeaponInWorld(weapon);
        }
    }

    private void PlaceWeaponInWorld(Weapon weapon)
    {
        weapon.gameObject.transform.position = weapon.shootingPosition;
        weapon.gameObject.transform.rotation = Quaternion.Euler(weapon.shootingRotation);
        weapon.gameObject.transform.SetParent(weaponHolder.transform, false);
    }

    private void DropWeapon(Weapon weapon, Vector3 position, Quaternion rotation)
    {
        weapon.gameObject.transform.position = position;
        weapon.gameObject.transform.rotation = rotation;
        weapon.gameObject.SetActive(true);
        weapon.animator.enabled = false;
        weapon.transform.SetParent(null);
    }
}
