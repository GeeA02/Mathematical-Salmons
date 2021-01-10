using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControllerFish : MonoBehaviour
{
    public float lookRadius = 10f;

    public Transform target;
    NavMeshAgent agent;

    new private Collider playerCollider;
    // v   ANIMATIONS   v
    static Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        playerCollider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            anim.SetBool("isRunning", true);
            anim.SetBool("isIddle", false);
            if(distance <= agent.stoppingDistance)
            {
                //Attack target
                FaceTarget();
                anim.SetBool("isRunning", false);
                anim.SetBool("isIddle", false);
                anim.SetTrigger("isAttacking");
            }
        }
        else 
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIddle", true);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}