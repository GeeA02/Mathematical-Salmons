using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityController : MonoBehaviour
{
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        anim.SetTrigger("isBouncing");
    }
}
