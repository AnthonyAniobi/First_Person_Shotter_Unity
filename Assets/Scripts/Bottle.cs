using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    
    [SerializeField] private List<Rigidbody> fragments;

    public void Shatter()
    {
        for(int i= 0; i<fragments.Count; i++)
        {
            fragments[i].isKinematic = false;
        }
    }
}
