using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    static public AmmoManager instance { get; set; }

    [SerializeField] private Weapon[] weapons;
    Weapon currentWeapon;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
        currentWeapon = weapons[0];
        
    }

}
