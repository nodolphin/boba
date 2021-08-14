using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Storage
{
    //DEFINITELY SHOULD CLEAN UP THIS CODE SOMEDAY
    private const float WALK_SPEED = 20f;
    private Animator animator;
    private Rigidbody2D rb2D;

    [SerializeField] private Vector3 pointerPosition;
    [SerializeField] private Vector3 pointerOffset;

    protected override void Start()
    {
        //PLAYER COMPONENTS
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected override void FixedUpdate()
    {   
        base.FixedUpdate();
        
        int moveX = (int) Input.GetAxisRaw("Horizontal");
        int moveY = (int) Input.GetAxisRaw("Vertical");

        bool pressedQ = Input.GetKeyDown(KeyCode.Q);
        bool pressedK = Input.GetKeyDown(KeyCode.K);

        bool isMoving = moveX != 0 || moveY != 0;
        bool isCarrying = storedItem.itemType != ItemType.NONE;

        Vector2 moveDir = new Vector2(moveX, moveY);
        moveDir = Vector2.ClampMagnitude(moveDir, 1);

        Vector2 displacement = moveDir * WALK_SPEED * Time.deltaTime;
        Vector2 newPosition = (Vector2) transform.position + displacement;

        rb2D.MovePosition(newPosition);

        if (isMoving)
        {
            if (moveX != 0) moveY = 0;

            //PLAYER ANIMATION
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveY);

            //DELETE
            pointerPosition = transform.position + new Vector3(moveX, moveY, 0) * 8 + pointerOffset;
        }
        
        //DELETE
        if (pressedQ)
            WithdrawItem(moveX, moveY);

        //DELETE

        //PLAYER ANIMATION
        animator.SetBool("Carrying", isCarrying);
        animator.SetBool("Moving", isMoving);
    }

    //DELETE
    private void WithdrawItem(int moveX, int moveY)
    {
        Storage storage = Globals.CheckCollision<Storage>(pointerPosition);
        if (storage != null)
        {
            if (storedItem.itemType == ItemType.NONE) storage.WithdrawItem(ref storedItem);
            else storage.StoreItem(ref storedItem);
        }
    }

    private void RevertToRegularSpeed()
    {

    }
}
