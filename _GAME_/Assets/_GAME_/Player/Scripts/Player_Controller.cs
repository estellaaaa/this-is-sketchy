using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]

public class Player_Controller : MonoBehaviour
{
    #region Enums

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    #endregion

    #region  Editor Data

    [Header("Movement Attributes")]
    [SerializeField] private float moveSpeed = 50f;

    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    #endregion


    #region Internal Data

    private Vector2 moveDirection = Vector2.zero;
    private Direction currentDirection = Direction.Right;

    private readonly int animationMoveRight = Animator.StringToHash("MoveRight");

    #endregion

    #region  Tick

    private void Update()
    {
        getInput();
        calculateCurrentDirection();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }

    #endregion


    #region Input Logic

    private void getInput()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        print(moveDirection);
    }

    #endregion

    #region Movement Logic

    private void MovementUpdate()
    {
        playerRigidbody.velocity = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
    }

    #endregion

    #region Animation Logic

    private void calculateCurrentDirection()
    {
        if (moveDirection.x != 0){
            if (moveDirection.x > 0)
            {
                currentDirection = Direction.Right;
            }
            else if (moveDirection.x < 0)
            {
                currentDirection = Direction.Left;
            }
        }

        Debug.Log(currentDirection);
    }

    private void UpdateAnimation()
    {
        if (currentDirection == Direction.Left)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (currentDirection == Direction.Right)
        {
            playerSpriteRenderer.flipX = false;
        }

        if (moveDirection.SqrMagnitude() > 0)
        {
            playerAnimator.CrossFade(animationMoveRight, 0);
        }
    }
    #endregion
}
