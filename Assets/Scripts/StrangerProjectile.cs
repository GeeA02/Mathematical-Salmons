using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerProjectile : MonoBehaviour
{
    private bool isCollided;
    void OnCollisionEnter(Collision co)
    {
        // if (co.gameObject.tag != "Stranger_Projectile" && co.gameObject.tag != "Stranger" && !isCollided)
        // {
        //     isCollided = true;
        //     Destroy(gameObject);
        // }
    }
}
