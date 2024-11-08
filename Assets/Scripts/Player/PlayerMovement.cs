using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed = new Vector2(7, 5);
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1, 1);
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 stopClamp = new Vector2(2.5f, 2.5f);
    [SerializeField] [Range(0, 50)] private float padding = 2f; // New padding variable

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move()
    {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection != Vector2.zero)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, moveDirection * maxSpeed, moveVelocity.magnitude * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, stopFriction.magnitude * Time.fixedDeltaTime);

            if (rb.velocity.magnitude < stopClamp.magnitude)
            {
                rb.velocity = Vector2.zero;
            }
        }

        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        // Apply boundary restriction
        MoveBound();
    }

    public void MoveBound()
    {
        // Calculate screen boundaries with padding in world coordinates
        Vector2 minBound = Camera.main.ViewportToWorldPoint(new Vector2(padding / 260f, padding / 280f));
        Vector2 maxBound = Camera.main.ViewportToWorldPoint(new Vector2(1 - padding / 250f, 1 - padding / 85f));

        // Clamp the player's position within these bounds
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(transform.position.x, minBound.x, maxBound.x),
            Mathf.Clamp(transform.position.y, minBound.y, maxBound.y)
        );

        // Apply the clamped position back to the transform
        transform.position = clampedPosition;
    }

    public Vector2 GetFriction()
    {
        return (moveDirection != Vector2.zero) ? moveFriction : stopFriction;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.01f;
    }
}
