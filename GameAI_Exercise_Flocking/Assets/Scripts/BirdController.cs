using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Transform flockCenter; // Assign the flock center transform in the Inspector.
    public float flockRadius = 5.0f; // The radius within which birds start flocking.
    public float circleRadius = 3.0f; // The radius for circling the flock center.
    public float rotationSpeed = 5.0f; // The speed at which birds rotate to face their movement direction.
    [SerializeField]
    private AnimationClip flyingAnimation; // The flying animation clip to assign in the Inspector.

    private bool isFlocking = false;
    private Rigidbody rb;
    private Animator animator; // Reference to the Animator component.

    private Vector3 circleCenter;
    private float circleAngle = 0.0f;
    private float circleSpeed = 2.0f; // Adjust the speed of circling.

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        circleCenter = flockCenter.position;

        // Get a reference to the Animator component.
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isFlocking)
        {
            float distanceToCenter = Vector3.Distance(transform.position, flockCenter.position);

            if (distanceToCenter <= flockRadius)
            {
                StartFlocking();
            }
            else
            {
                // Calculate the direction to the flock center.
                Vector3 direction = (flockCenter.position - transform.position).normalized;

                // Set the velocity to move the bird towards the flock center.
                rb.velocity = direction * moveSpeed;

                // Rotate the bird to face the movement direction.
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            // Implement your flocking behavior here when isFlocking is true.

            // Move in a circular pattern around the flock center.
            circleAngle += circleSpeed * Time.deltaTime;
            Vector3 circlePosition = circleCenter + new Vector3(Mathf.Cos(circleAngle) * circleRadius, 0.0f, Mathf.Sin(circleAngle) * circleRadius);
            rb.velocity = (circlePosition - transform.position).normalized * moveSpeed;

            // Rotate the bird to face the circular movement direction.
            Vector3 circleDirection = (circlePosition - transform.position).normalized;
            Quaternion circleLookRotation = Quaternion.LookRotation(circleDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, circleLookRotation, Time.deltaTime * rotationSpeed);
        }

        // Update the Animator parameters.
        animator.SetBool("IsFlying", !isFlocking); // Play flying animation when not flocking.
        animator.SetBool("StartFlock", isFlocking); // Trigger flocking animation when flocking.
    }

    public void StartFlocking()
    {
        isFlocking = true;
        // Implement your flocking behavior when isFlocking is true.
    }
}
