using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0f; // Speed of the character movement
    public Animator animator;
    private CharacterController controller; // Reference to the CharacterController component

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Calculate movement direction based on input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical);

        // Normalize movement vector to avoid faster diagonal movement
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        // Rotate player towards movement direction
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.5f);
        }

        // Move the character
        controller.Move(moveDirection * speed * Time.deltaTime);

        animator.SetFloat( "Speed",(moveDirection * speed).magnitude);
    }
}
