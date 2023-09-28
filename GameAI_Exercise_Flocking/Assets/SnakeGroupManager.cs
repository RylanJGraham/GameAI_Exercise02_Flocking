using UnityEngine;
using UnityEngine.AI;

public class SnakeGroupManager : MonoBehaviour
{
    public float flockSpeed = 2.0f;
    public float separationDistance = 1.0f;
    public float chaseDistance = 5.0f;
    public float attackDistance = 2.0f;
    public bool chaseRat = false; // Toggle to chase the rat
    public Transform target; // The target object to chase (e.g., the rat)

    public AnimationClip walkAnimationClip;
    public AnimationClip attackAnimationClip;

    private NavMeshAgent[] snakes;
    private Animator[] animators;

    private void Start()
    {
        // Find all NavMeshAgent components in child snakes
        snakes = GetComponentsInChildren<NavMeshAgent>();
        animators = GetComponentsInChildren<Animator>();

        // Set the target for all snakes
        foreach (NavMeshAgent snake in snakes)
        {
            snake.speed = flockSpeed;
        }
    }

    private void Update()
    {
    // Calculate the distance between the group and the target
    float distanceToTarget = Vector3.Distance(transform.position, target.position);

    // Flocking behavior
    if (distanceToTarget > chaseDistance)
    {
        // Apply flocking behavior (separation, alignment, cohesion) for all snakes
        foreach (NavMeshAgent snake in snakes)
        {
            SetSnakeAnimation(snake, true);
            // Rest of the flocking logic...
        }
    }
    else
    {
        // Chase the target (rat) or attack when close
        foreach (NavMeshAgent snake in snakes)
        {
            if (chaseRat)
            {
                snake.destination = target.position;
            }
            else
            {
                // Calculate desired position within the group
                int snakeIndex = System.Array.IndexOf(snakes, snake);
                Vector3 desiredPosition = CalculateEvenSpacingPosition(snakeIndex);
                snake.destination = desiredPosition;
            }

            // Check attack distance
            if (distanceToTarget <= attackDistance)
            {
                SetSnakeAnimation(snake, false); // Stop walking animation
                PlayAttackAnimation(snake); // Play attack animation
            }
            else
            {
                SetSnakeAnimation(snake, true); // Start or continue walking animation
            }
        }
    }
    }

    private Vector3 CalculateEvenSpacingPosition(int snakeIndex)
    {
        // Calculate the desired position within the group based on the snake's index
        Vector3 spacingDirection = Quaternion.Euler(0, 30 * snakeIndex, 0) * Vector3.forward; // Adjust rotation angle as needed
        Vector3 desiredPosition = transform.position + spacingDirection * separationDistance;
        return desiredPosition;
    }

    private void SetSnakeAnimation(NavMeshAgent snake, bool isWalking)
    {
    // Set the snake's animation state
    if (animators != null)
    {
        foreach (Animator animator in animators)
        {
            animator.SetBool("IsWalking", isWalking);

            // Use the assigned walkAnimationClip if available
            if (isWalking && walkAnimationClip != null)
            {
                animator.CrossFade(walkAnimationClip.name, 0.2f);
            }
        }
    }
    }

    private void PlayAttackAnimation(NavMeshAgent snake)
    {
    if (animators != null && attackAnimationClip != null)
    {
        foreach (Animator animator in animators)
        {
            // Play the attack animation
            animator.CrossFade(attackAnimationClip.name, 0.2f);
        }
    }
    }
}
