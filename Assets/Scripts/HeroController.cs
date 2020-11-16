using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeroController : MonoBehaviour
{
    private readonly float movSpeed = 2;
    private readonly float rotSpeed = 4;
    private Vector3 oldPos = Vector3.zero;
    private readonly Dictionary<KeyCode, bool> isDirectionKeyPressed = new Dictionary<KeyCode, bool>();
    private CharacterController controller;
    private Animator anim;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        var newPos = new Vector3(horizontal, 0f, vertical).normalized;

        controller.Move(newPos * movSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.W)) WalkAnimation(KeyCode.W, true);
        if (Input.GetKeyDown(KeyCode.S)) WalkAnimation(KeyCode.S, true);
        if (Input.GetKeyDown(KeyCode.A)) WalkAnimation(KeyCode.A, true);
        if (Input.GetKeyDown(KeyCode.D)) WalkAnimation(KeyCode.D, true);
        if (Input.GetKeyUp(KeyCode.W)) WalkAnimation(KeyCode.W, false);
        if (Input.GetKeyUp(KeyCode.S)) WalkAnimation(KeyCode.S, false);
        if (Input.GetKeyUp(KeyCode.A)) WalkAnimation(KeyCode.A, false);
        if (Input.GetKeyUp(KeyCode.D)) WalkAnimation(KeyCode.D, false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDirectionKeyPressed.Clear();

            anim.SetTrigger("IsJumping");
        }

        // while idle look at old "position"
        if (newPos == Vector3.zero)
            newPos = oldPos;
        else
            oldPos = newPos;

        Quaternion rotation = Quaternion.LookRotation(newPos);

        transform.rotation = Quaternion.Lerp(transform.rotation,
                                            rotation,
                                            rotSpeed * Time.deltaTime);
    }

    private void WalkAnimation(KeyCode key, bool isGonnaWalk)
    {
        if (isGonnaWalk)
        {
            // when any direction ket is not pressed
            if (!isDirectionKeyPressed.Any(x => x.Value))
            {
                // enable walking animation
                anim.SetBool("IsIdle", false);
                anim.SetBool("IsWalking", true);
            }

            isDirectionKeyPressed[key] = true;
        }
        else
        {
            isDirectionKeyPressed[key] = false;

            if (!isDirectionKeyPressed.Any(x => x.Value))
            {
                anim.SetBool("IsIdle", true);
                anim.SetBool("IsWalking", false);
            }
        }
    }
}
