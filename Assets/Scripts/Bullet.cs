using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            print("Hit Target ${collision.gameObject.name}");
            Destroy(gameObject);
        }
    }
}
