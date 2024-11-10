using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class RPGController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Animator animator;

    public LayerMask whatStopsMovement; // Layers that block movement (e.g. walls, obstacles)
    public LayerMask objectLayer; // Layer for the movable objects (boxes, crates, etc.)

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, movePoint.position) <= .01f)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Update animator parameters
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);

            // Check horizontal movement first
            if(Mathf.Abs(horizontalInput) == 1f)
            {
                Vector3 targetPosition = movePoint.position + new Vector3(horizontalInput, 0f, 0f);
                if (!Physics2D.OverlapCircle(targetPosition, .2f, whatStopsMovement))  // Can we move to the target position?
                {
                    // Check if the next space is blocked by an object
                    RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.right * Mathf.Sign(horizontalInput), .2f, objectLayer);
                    if (hit.collider != null)  // There is an object in the way
                    {
                        // Try to push the object
                        Vector3 objectTargetPosition = targetPosition + new Vector3(horizontalInput, 0f, 0f);
                        if (!Physics2D.OverlapCircle(objectTargetPosition, .2f, whatStopsMovement)) // Can the object move?
                        {
                            hit.collider.transform.position = objectTargetPosition; // Push the object
                            movePoint.position = targetPosition; // Move the player
                        }
                    }
                    else
                    {
                        // No object in the way, just move the player
                        movePoint.position += new Vector3(horizontalInput, 0f, 0f);
                    }
                }
            }

            // Check vertical movement
            if(Mathf.Abs(verticalInput) == 1f)
            {
                Vector3 targetPosition = movePoint.position + new Vector3(0f, verticalInput, 0f);
                if (!Physics2D.OverlapCircle(targetPosition, .2f, whatStopsMovement)) // Can we move to the target position?
                {
                    // Check if the next space is blocked by an object
                    RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.up * Mathf.Sign(verticalInput), .2f, objectLayer);
                    if (hit.collider != null)  // There is an object in the way
                    {
                        // Try to push the object
                        Vector3 objectTargetPosition = targetPosition + new Vector3(0f, verticalInput, 0f);
                        if (!Physics2D.OverlapCircle(objectTargetPosition, .2f, whatStopsMovement)) // Can the object move?
                        {
                            hit.collider.transform.position = objectTargetPosition; // Push the object
                            movePoint.position = targetPosition; // Move the player
                        }
                    }
                    else
                    {
                        // No object in the way, just move the player
                        movePoint.position += new Vector3(0f, verticalInput, 0f);
                    }
                }
            }
        }
    }
}