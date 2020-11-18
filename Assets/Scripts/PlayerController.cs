using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 5.0f;
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float rotateSpeed = 1.5f;

    uint pointsPi = 0;
    uint pointsFi = 0;
    uint pointsE = 0;

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
    }

    private bool IsGrounded()
    => Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.size.y);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PointE"))
        {
            collider.gameObject.SetActive(false);
            Destroy(collider.gameObject);
            pointsE++;
        }
        else if (collider.gameObject.CompareTag("PointPi"))
        {
            collider.gameObject.SetActive(false);
            Destroy(collider.gameObject);
            pointsPi++;
        }
        else if (collider.gameObject.CompareTag("PointFi"))
        {
            collider.gameObject.SetActive(false);
            Destroy(collider.gameObject);
            pointsFi++;
        }

        Debug.Log($"E: {pointsE}  Pi: {pointsPi}  Fi: {pointsFi}");
    }
}
