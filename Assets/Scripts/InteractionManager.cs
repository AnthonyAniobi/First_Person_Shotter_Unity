using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    static public InteractionManager instance {get; private set;}
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
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if(Physics.Raycast(
            ray,
            out RaycastHit hit
             ))
        {
            GameObject hitObject = hit.transform.gameObject;
            Weapon weapon;
            if(hitObject.TryGetComponent(out weapon))
            {
                print($"Weapon Selected: {weapon.name}");
            }
        }

        Debug.DrawRay(
            ray.origin,
            ray.direction * 100f,
            Color.red
        );
    }
}
