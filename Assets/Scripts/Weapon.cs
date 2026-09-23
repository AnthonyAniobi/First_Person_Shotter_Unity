using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 500f;
    [SerializeField] private float bulletLifeTime = 2f;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private int burstCount = 3;
    [SerializeField] private float spreadIntensity = 0.1f;
    [SerializeField] private bool allowResetShooting = true;

    private int currentBurstCount = 0;
    [SerializeField] private float shootDelay = 0.2f;
    [SerializeField] private float shootResetDelay = 2f;
    
    public ShootingMode currentShootingMode = ShootingMode.Single;
    public bool isShooting, readyToShoot = true;

    
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    void Start()
    {
        ResetShooting();
    }

    // Update is called once per frame
    void Update()
    {
        InputAction fireBullet = InputSystem.actions.FindAction("Attack");

        
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

        if (isShooting && readyToShoot)
        {
            currentBurstCount = burstCount;
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        readyToShoot = false;
        Vector3 shootingDirection = GetBulletDirection().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(RemoveBullet(bullet, bulletLifeTime));

        if (allowResetShooting)
        {
            Invoke("ResetShooting", shootResetDelay);
            allowResetShooting = false;
        }
        // Burst Mode
        if(currentShootingMode == ShootingMode.Burst && currentBurstCount > 1)
        {
            currentBurstCount--;
            Invoke("FireWeapon", shootDelay);
        }
    }

    private IEnumerator RemoveBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    private Vector3 GetBulletDirection()
    {   
        // raycast from the center of the camera
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        
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
}
