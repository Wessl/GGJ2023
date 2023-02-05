using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumping : MonoBehaviour
{
    [SerializeField] Vector3 limitMovementLeftSide;
    [SerializeField] Vector3 limitMovementRightSide;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Collider collider;
    private ActionState actionState;
    [SerializeField] private float timeToJumpAgain;
    private float timeSinceLastJump;
    private Vector3 jumpDirection = Vector3.zero;
    [SerializeField] float jumpForceMultiplier;
    [SerializeField] float jumpHeight;
    private float distToGround;
    [SerializeField] private Sprite onGroundSprite;
    [SerializeField] private Sprite inAirSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float spriteChangeCooldown;
    private float timeAtSpriteChange;
    private enum ActionState
    {
        PassiveJumping,
        PassiveChill,
        AggresiveJumping,
        AggresiveChill
    }
    private void Start()
    {
        distToGround = collider.bounds.extents.y;
        jumpDirection = new Vector3(1, jumpHeight, 0);
    }
    private void Update()
    {
        // If is grounded and passiveJumping become passiveChilling
        if (actionState == ActionState.PassiveJumping)
        {
            
            if (IsGrounded())
            {               
                actionState = ActionState.PassiveChill;
                timeSinceLastJump = Time.time;
            }
            
        }
        else if (IsGrounded() && actionState == ActionState.PassiveChill && timeSinceLastJump + timeToJumpAgain < Time.time)
        {

            if (transform.position.x > limitMovementRightSide.x)
            {
                jumpDirection = new Vector3(-1, jumpHeight, 0);
            }
            if (transform.position.x < limitMovementLeftSide.x)
            {
                jumpDirection = new Vector3(1, jumpHeight, 0);
            }
            rigidbody.AddForce(jumpDirection * jumpForceMultiplier);
            actionState = ActionState.PassiveJumping;
            
            
            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMovementLeftSide, limitMovementLeftSide + Vector3.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMovementRightSide, limitMovementRightSide + Vector3.up);
    }
    bool IsGrounded()
    {
        var rayCast = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
        if (rayCast)
        {
            spriteRenderer.sprite = inAirSprite;
            Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.green);
        }
        else
        {
            spriteRenderer.sprite = onGroundSprite;
            Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red);
        }
        return rayCast;
    }
}
