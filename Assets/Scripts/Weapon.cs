using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletLifeTime = 2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputAction fireBullet = InputSystem.actions.FindAction("Attack");

        if (fireBullet.WasPressedThisFrame())
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpeed * transform.forward.normalized, ForceMode.Impulse);
        StartCoroutine(RemoveBullet(bullet, bulletLifeTime));
    }

    private IEnumerator RemoveBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
