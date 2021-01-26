using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangerProjectile : MonoBehaviour
{
    private bool isCollided;
    public GameObject impactVFX;
    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Player")
        {
            isCollided = true;
            co.gameObject.SendMessage("Damage");
            var impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(impact, 2);
            Destroy(gameObject);
        }

        else if (co.gameObject.tag != "Stranger_Projectile" && co.gameObject.tag != "Stranger" && !isCollided)
        {
            isCollided = true; 
            var impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(impact, 2);
            Destroy(gameObject);
        }
    }
}
