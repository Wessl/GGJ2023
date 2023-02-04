using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyBound : MonoBehaviour
{
    private Vector3 homePosition;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] SphereCollider sphereCollider;
    private float distToGround;
    [SerializeField] private float timeToJumpAgain;
    private float timeSinceLastJump;
    [SerializeField] Vector2 jumpPowerRange;
    [SerializeField] float jumpForceMultiplier;

    private enum ActionState
    {
        PassiveJumping,
        PassiveChill,
        AggresiveJumping,
        AggresiveChill
    }
    private ActionState actionState;
    void Start()
    {
        actionState = ActionState.PassiveChill;
        distToGround = sphereCollider.bounds.extents.y;
        homePosition = transform.position;
        timeSinceLastJump = Time.time;
    }

    void Update()
    {
        // If is grounded and passiveJumping become passiveChilling
        if (IsGrounded() && actionState == ActionState.PassiveJumping)
        {
            actionState=ActionState.PassiveChill;
            timeSinceLastJump = Time.time;
        }
        if (IsGrounded() && actionState == ActionState.PassiveChill && timeSinceLastJump + timeToJumpAgain < Time.time)
        {
            // Jump again
            // get random direction in half circle up
            var angle = Random.Range(Mathf.PI/4, Mathf.PI*3/4);

            Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
            Vector3 dist = dir * Random.Range(jumpPowerRange.x, jumpPowerRange.y);
            Vector3 weightedDir = homePosition - transform.position + dist;
            rigidbody.AddForce(weightedDir * jumpForceMultiplier);
            actionState = ActionState.PassiveJumping;

        }

        // If not, do another passiveJumping. Be mindful of how far you are, if too far, jump back
        // If the player, just jump towards it until it's impossible
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(200f, 200f, 200f, 200f), actionState.ToString() + "\n " + (timeSinceLastJump - Time.time));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(homePosition, jumpPowerRange.x);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(homePosition, jumpPowerRange.y);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    bool IsGrounded()
    {
        var rayCast = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
        if (rayCast)
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.green);

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red);
        }
        return rayCast;
    }



}
