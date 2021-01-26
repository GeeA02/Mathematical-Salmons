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
    
    public GameObject player;

    private uint health = 2;

    public GameObject deathParticles;

    // v   ANIMATIONS   v
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.enabled = !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            FaceTarget();
            if (agent.enabled)
                agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance && !anim.GetBool("isAttacking"))
            {
                //Attack target
                FaceTarget();
                anim.SetBool("isRunning", false);
                anim.SetBool("isIddle", false);
                anim.SetBool("isAttacking", true);
            }
            else
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isIddle", false);
                anim.SetBool("isAttacking", false);
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
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void EndAttack()
    {
        var dist = Vector3.Distance(target.position, transform.position);

        if (dist < 3.0f)
            player.SendMessage("Damage");
    }

    void Damage()
    {
        if (health > 0)
            health--;

        if (health == 0)
        {
            var particlePos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            var death = Instantiate(deathParticles, particlePos, Quaternion.identity);
            Destroy(gameObject);
            Destroy(death, 5);
        }
    }
}