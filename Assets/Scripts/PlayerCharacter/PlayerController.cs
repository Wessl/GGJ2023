using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float MovementSpeed;
    [SerializeField]
    private float JumpHeight;
    [SerializeField]
    private float Gravity;
    [SerializeField]
    private LayerMask inAirLayerMaskTest;
    [Header("Other")]
    [SerializeField]
    private Rigidbody Rb;
    private float Direction;
    public void PlayerHorizontalMovement(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<float>();
        float xScale = Mathf.Abs(transform.localScale.x);
        if (Direction > 0)
        {
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }
        else if(Direction < 0)
        {
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);

        }
    }
    public void PlayerJump(InputAction.CallbackContext context)
    {
        if(InAir())
        {
            Rb.AddForce(new Vector3(0,JumpHeight,0),ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        Rb.velocity = new Vector3(Direction * MovementSpeed, Rb.velocity.y, 0);
        if (InAir())
        {
            Rb.AddForce(new Vector3(0, -Gravity, 0)); 
        }
    }
    private bool InAir()
    {
        Ray ray = new Ray(transform.position, new Vector2(0, -0.55f * transform.lossyScale.y));
        RaycastHit hit;
        bool inAir = Physics.Raycast(ray,out hit,int.MaxValue,inAirLayerMaskTest);
        return inAir;
    }
}
