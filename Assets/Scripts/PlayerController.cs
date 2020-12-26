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
    new private Collider playerCollider;
    private Vector3 inputVector;
    private Vector3 oldDirection;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        var vertical = Input.GetAxis("Vertical") * moveSpeed;

        inputVector = new Vector3(horizontal, rigidbody.velocity.y, vertical);

        Vector3 currDirection;

        if (horizontal == 0 && vertical == 0){
            currDirection = oldDirection;
        }
        else {
            currDirection = new Vector3(horizontal, 0.0f, vertical);
            oldDirection = currDirection;
        }

        transform.rotation = Quaternion.RotateTowards(
                                            transform.rotation,
                                            Quaternion.LookRotation(currDirection),
                                            rotateSpeed);



        rigidbody.velocity = inputVector;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Health part
        if (health > numOfHearts)
            health = numOfHearts;
        for (int i = 0; i < hearts.Length; i++)
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
            case ("PointE"):
                collider.gameObject.SetActive(false);
                Destroy(collider.gameObject);
                pointsE++;
                pointsEText.text = "x " + pointsE.ToString();
                break;
            case ("PointPi"):
                collider.gameObject.SetActive(false);
                Destroy(collider.gameObject);
                pointsPi++;
                pointsPiText.text = "x " + pointsPi.ToString();
                break;
            case ("PointFi"):
                collider.gameObject.SetActive(false);
                Destroy(collider.gameObject);
                pointsFi++;
                pointsFiText.text = "x " + pointsFi.ToString();
                break;
            case ("Obstacle"):
                Hurt();
                break;
            case ("Heart"):
                if (health < 5)
                {
                    collider.gameObject.SetActive(false);
                    health++;
                    Destroy(collider.gameObject);
                }
                break;
        }

        Debug.Log($"E: {pointsE}  Pi: {pointsPi}  Fi: {pointsFi} Hearts: {health}");
    }

    private void Hurt()
    {
        if (health > 0)
            health--;

        if (health == 0)
        {
            gameOverText.text = "GAME OVER"; 
            gameOverImage.rectTransform.sizeDelta = new Vector2(1066, 508);
        }
        Debug.Log("OBSTACLE!!!!!!!!!!!!!!!!!11ONEONE");
        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z - 1);
    }
}
