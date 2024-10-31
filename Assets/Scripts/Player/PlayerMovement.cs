using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed = new Vector2(7, 5);
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1, 1);
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 stopClamp = new Vector2(2.5f, 2.5f);

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
    }

    public Vector2 GetFriction()
    {
        return (moveDirection != Vector2.zero) ? moveFriction : stopFriction;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.01f;
    }

    public void MoveBound()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -9f, 9f); 
        pos.y = Mathf.Clamp(pos.y, -5f, 5f); 
        transform.position = pos;
    }
}