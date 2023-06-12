using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float damping = 5f; // Deceleration factor
    private Rigidbody rb;
    private Transform bodyTransform;
    public Transform headTransform; // Reference to the camera object
    private Vector3 currentVelocity;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float strafeSpeed = 1f;
    public float gravityScale = 5;

    public float jumpAmount = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true; // Disable rotation caused by physics forces
    }
    private void FixedUpdate()
    {
        rb.AddForce((gravityScale - 1) * rb.mass * Physics.gravity);
    }

    void Update()
    {

        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
            moveHorizontal = -strafeSpeed;
        else if (Input.GetKey(KeyCode.D))
            moveHorizontal = strafeSpeed;

        if (Input.GetKey(KeyCode.W))
            if (Input.GetKey(KeyCode.LeftShift))
                moveVertical = runSpeed;
            else
                moveVertical = walkSpeed;
        else if (Input.GetKey(KeyCode.S))
            moveVertical = -strafeSpeed;


        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        movement = Quaternion.Euler(0f, headTransform.eulerAngles.y, 0f) * movement;

        rb.velocity = movement * moveSpeed;


        //rb.AddForce(movement * moveSpeed);
        //currentVelocity = movement * moveSpeed;


        if (movement.magnitude > Vector3.zero.magnitude)
        {
            rb.AddForce(-currentVelocity * damping);
        }

        // Keep the player upright
        Quaternion uprightRotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
        rb.MoveRotation(uprightRotation);

        // Lock player's Y rotation to the camera's rotation
        // transform.rotation = Quaternion.Euler(0f, headTransform.eulerAngles.y, 0f);
    }
}
