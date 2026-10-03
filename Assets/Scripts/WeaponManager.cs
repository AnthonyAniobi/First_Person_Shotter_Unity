using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    static public WeaponManager instance { get; private set; }

    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Weapon currentWeapon;

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
        currentWeapon = weapons[0];
    }

    // // Update is called once per frame
    // void Update()
    // {
    // }
}
