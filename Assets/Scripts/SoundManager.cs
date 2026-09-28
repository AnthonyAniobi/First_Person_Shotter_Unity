using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundEffectsSource;
    [SerializeField] private AudioClip pistol1911ShotSound;
    [SerializeField] private AudioClip AK47ShotSound;
    [SerializeField] private AudioClip emptyMagazineSound;
    // [SerializeField] private AudioClip bulletImpactSound;
    [SerializeField] private AudioClip pistol1911ReloadSound;
    [SerializeField] private AudioClip AK47ReloadSound;

    void Awake()
    {
        if (instance != null && instance != this)
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

    public void PlayShootingSound(Weapon.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case Weapon.WeaponType.Pistol1911:
                soundEffectsSource.PlayOneShot(pistol1911ShotSound);
                break;
            case Weapon.WeaponType.AK47:
                soundEffectsSource.PlayOneShot(AK47ShotSound);
                break;
        }
    }

    public void PlayReloadSound(Weapon.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case Weapon.WeaponType.Pistol1911:
                soundEffectsSource.PlayOneShot(pistol1911ReloadSound);
                break;
            case Weapon.WeaponType.AK47:
                soundEffectsSource.PlayOneShot(AK47ReloadSound);
                break;
        }
    }

    public void PlayEmptyMagazineSound(Weapon.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case Weapon.WeaponType.Pistol1911:
                soundEffectsSource.PlayOneShot(emptyMagazineSound);
                break;
            case Weapon.WeaponType.AK47:
                soundEffectsSource.PlayOneShot(emptyMagazineSound);
                break;
        }
    }

}
