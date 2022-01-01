using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float movementSpeed;
    public float offset;

    public Transform weaponRotatePivot;
    public TestWeapon weapon;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer playerSR;

    private bool isMoving;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ProcessInput();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        ApplyMovementForces();
    }

    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        weaponRotatePivot.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        Vector3 aimLocalScale = Vector3.one;

        if(rotZ > 90 || rotZ < -90)
        {
            playerSR.flipX = true;
            aimLocalScale.y = -1f;        }
        else
        {
            playerSR.flipX = false;
            aimLocalScale.y = +1f;
        }

        weaponRotatePivot.localScale = aimLocalScale;

        if(moveDirection != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void ApplyMovementForces()
    {
        rb.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isMoving", isMoving);
    }
}
