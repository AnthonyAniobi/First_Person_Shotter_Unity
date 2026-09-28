using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 500f;

    [SerializeField] private int burstCount = 3;
    [SerializeField] private float spreadIntensity = 0.1f;
    [SerializeField] private bool allowResetShooting = true;

    private int currentBurstCount = 0;
    [SerializeField] private float burstDelay = 0.2f;
    [SerializeField] private float shootResetDelay = 2f;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private int magazineSize;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool isReloading = false;


    private Animator animator;
    
    public ShootingMode currentShootingMode = ShootingMode.Single;
    public bool isShooting, readyToShoot = true;
    public WeaponType currentWeaponType = WeaponType.Pistol1911;

    
    public enum WeaponType
    {
        Pistol1911,
        AK47
    }
    
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    void Start()
    {
        ResetShooting();
        currentAmmo = magazineSize;
        animator = GetComponent<Animator>();
        SetAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        InputAction fireBullet = InputSystem.actions.FindAction("Attack");

        InputAction reloadMagazine = InputSystem.actions.FindAction("Reload");

        
        if(currentShootingMode == ShootingMode.Single)
        {
            // tap to shoot
            isShooting = fireBullet.WasPressedThisFrame();
        }else if(currentShootingMode == ShootingMode.Burst)
        {
            // tap to shoot
            isShooting = fireBullet.WasPressedThisFrame();
        }else if(currentShootingMode == ShootingMode.Auto)
        {
            // Hold down to shoot
            isShooting = fireBullet.IsPressed();
        }

        if(currentAmmo <= 0  && isReloading == false){
            SoundManager.instance.PlayEmptyMagazineSound(currentWeaponType);
        }else if (isShooting && readyToShoot && currentAmmo > 0)
        {
            currentBurstCount = burstCount;
            FireWeapon();
        }

        if(reloadMagazine.WasPressedThisFrame() && isReloading == false && currentAmmo <= magazineSize)
        {
            Reload();
        }
        // Reload automatically when ammo is empty
        if(currentAmmo <= 0 && isReloading == false)
        {
            Reload();
        }
    }

    private void FireWeapon()
    {
        currentAmmo--;

        SetAmmoText();

        readyToShoot = false;
        //
        animator.SetTrigger("RECOIL");
        muzzleFlash.GetComponent<ParticleSystem>().Play();
        SoundManager.instance.PlayShootingSound(currentWeaponType);

        Vector3 shootingDirection = GetBulletDirection().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);
        
        if (allowResetShooting)
        {
            Invoke("ResetShooting", shootResetDelay);
            allowResetShooting = false;
        }
        // Burst Mode
        if(currentShootingMode == ShootingMode.Burst && currentBurstCount > 1)
        {
            currentBurstCount--;
            Invoke("FireWeapon", burstDelay);
        }
    }

    private void Reload()
    {
        SoundManager.instance.PlayReloadSound(currentWeaponType);
        isReloading = true;
        Debug.Log("Reloading...");
        animator.SetTrigger("RELOAD");
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        currentAmmo = magazineSize;
        Debug.Log("Reload Complete");
        isReloading = false;
        SetAmmoText();
    }

   

    private Vector3 GetBulletDirection()
    {   
        // raycast from the center of the camera
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }

        Vector3 direction = targetPoint - bulletSpawnPoint.position;
        float x = Random.Range(-spreadIntensity, spreadIntensity);
        float y = Random.Range(-spreadIntensity, spreadIntensity);
        direction += new Vector3(x, y, 0f);
        return direction;
    }

    private void ResetShooting()
    {
        readyToShoot = true;
        allowResetShooting = true;
    }

    private void SetAmmoText()
    {
        if(AmmoManager.instance.ammoText != null)
        {
            AmmoManager.instance.ammoText.text = $"{currentAmmo} / {magazineSize}";
        }
    }


    
}
