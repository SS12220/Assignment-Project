using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0f; // Speed of the character movement
    public Animator animator;
    private CharacterController controller; // Reference to the CharacterController component
    private bool rotatedTowardsObject = false; // Flag to track if player has rotated towards object

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

        animator.SetFloat("Speed", (moveDirection * speed).magnitude);

        // Check if player should rotate towards object with tag "Respawn"
        if ((horizontal == 0 && vertical == 0))
        {
            // Raycast to check if mouse is hovering over object with tag "Respawn"
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Respawn") && !rotatedTowardsObject)
                {
                    Vector3 lookDirection = hit.point - transform.position;
                    lookDirection.y = 0f; // Ensure the player doesn't tilt up or down
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), 0.5f);
                    rotatedTowardsObject = true; // Set flag to true to indicate rotation has occurred
                }
                else
                {
                    rotatedTowardsObject = false; // Reset flag to false if raycast hits object without "Respawn" tag
                }
            }
        }
    }
}
