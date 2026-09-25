using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            CreateBulletImpactEffect(collision);
            print("Hit Target ${collision.gameObject.name}");
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Target"))
        {
            print("Hit Target ${collision.gameObject.name}");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Bottle"))
        {
            print("Hit Bottle ${collision.gameObject.name}");
            collision.gameObject.GetComponent<Bottle>().Shatter();
            // let bullet pass through the bottle
        }
    }

    void CreateBulletImpactEffect(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts.First();
        
        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );
        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
