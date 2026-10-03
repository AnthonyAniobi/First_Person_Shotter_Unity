using TMPro;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    static public AmmoManager instance { get; set; }

    public TextMeshProUGUI ammoText;
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
