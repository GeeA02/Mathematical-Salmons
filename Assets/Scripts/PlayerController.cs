using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 5.0f;
    [SerializeField]
    private float moveSpeed = 5.0f;

    uint pointsPi = 0;
    uint pointsFi = 0;
    uint pointsE = 0;

    private Rigidbody playerBody;
    private Vector3 inputVector;
    new private Collider collider;
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        var vertical = Input.GetAxis("Vertical") * moveSpeed;
        inputVector = new Vector3(horizontal, playerBody.velocity.y, vertical);
        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
        playerBody.velocity = inputVector;

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    => Physics.Raycast(transform.position, Vector3.down, collider.bounds.size.y);

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
