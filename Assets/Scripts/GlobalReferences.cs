using UnityEngine;

public class GlobalReferences : MonoBehaviour
{

    public static GlobalReferences Instance  {set; get;}

    public GameObject bulletImpactEffectPrefab;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
