using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 5.0f;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float rotateSpeed = 1.5f;

    //  v   POINTS  v
    uint pointsPi = 0;
    uint pointsFi = 0;
    uint pointsE = 0;
    public Text pointsPiText;
    public Text pointsFiText;
    public Text pointsEText;

    // v    GAMEOVER SCREEN v
    public RawImage gameOverImage;
    public Text gameOverText;

    //  v   HEALTH  v
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    new private Rigidbody rigidbody;
    private Collider playerCollider;

    // v   ANIMATIONS   v
    static Animator anim;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var translation = Input.GetAxis("Vertical") * moveSpeed;

        //  v   moving  v
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
        {
            var rotation = Input.GetAxis("Horizontal") * rotateSpeed;
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);
        }


        if (translation > 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIddle", false);
        }
        else if (translation < 0)
        {
            anim.SetBool("isRunningBackward", true);
            anim.SetBool("isIddle", false);
        }
        else
        {
            anim.SetBool("isRunningBackward", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isIddle", true);
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isJumping");
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("isAttacking");
        }

        // Health part
        if (health > numOfHearts)
            health = numOfHearts;
        for (var i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < numOfHearts)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }

    }

    private bool IsGrounded()
    => Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.size.y);

    private void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "PointE":
                collider.gameObject.SetActive(false);
                Destroy(collider.gameObject);
                pointsE++;
                pointsEText.text = $"x {pointsE}";
                break;
            case "PointPi":
                collider.gameObject.SetActive(false);
                Destroy(collider.gameObject);
                pointsPi++;
                pointsPiText.text = $"x {pointsPi}";
                break;
            case "PointFi":
                collider.gameObject.SetActive(false);
                Destroy(collider.gameObject);
                pointsFi++;
                pointsFiText.text = $"x {pointsFi}";
                break;
            case "Obstacle":
                Hurt();
                anim.SetTrigger("isHurted");
                break;
            case "Heart":
                if (health < 5)
                {
                    collider.gameObject.SetActive(false);
                    health++;
                    Destroy(collider.gameObject);
                }
                break;
            case "Fishman":
            case "Stranger":
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
                    collider.gameObject.SendMessage("Damage");
                break;
        }
    }

    public void Hurt()
    {
        if (health > 0)
            health--;

        if (health == 0)
        {
            gameOverText.text = "GAME OVER";
            gameOverImage.rectTransform.sizeDelta = new Vector2(1066, 508);
        }

        rigidbody.AddForce(new Vector3(0,4,-2), ForceMode.Impulse);
    }

    void Damage()
    {
        Hurt();
    }
}
