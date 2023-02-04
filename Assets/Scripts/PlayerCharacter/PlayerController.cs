using Microsoft.Cci;
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
    private float currentGravity;
    [SerializeField]
    private LayerMask inAirLayerMaskTest;
    [SerializeField]
    private LayerMask onSandLayerMaskTest;
    [Header("Other")]
    [SerializeField]
    private Rigidbody Rb;
    private float Direction;

    private Vector3 savedVelocity = Vector3.zero;

    private SwingerVine swingParent = null;

    [SerializeField]
    private Transform swingPositionOffset;

    public void PlayerHorizontalMovement(InputAction.CallbackContext context)
    {
        if (Manager.IsPaused) return;

        Direction = context.ReadValue<float>();
        float xScale = Mathf.Abs(transform.localScale.x);
        if (Direction > 0)
        {
            transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }
        else if (Direction < 0)
        {
            transform.localScale = new Vector3(-xScale, transform.localScale.y, transform.localScale.z);

        }
    }
    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (Manager.IsPaused) return;

        if (swingParent)
        {
            ExitSwing();
            return;
        }

        if (InAir())
        {
            Rb.AddForce(new Vector3(0, JumpHeight, 0), ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        if (Manager.IsPaused) return;
        var tempMoveSpeed = MovementSpeed;
        if (IsOnSand())
        {
            Debug.Log("i am on sand");
            currentGravity = Gravity; // You can set this to like 60 in order to get stuck to the floor when doing it but it looks kind of hacky
            tempMoveSpeed *= 2f;
        }
        else
        {
            currentGravity = Gravity;
        }
        if (swingParent != null)
        {
            UpdateSwingPosition(Time.deltaTime);
        }
        else
        {
            Rb.velocity = new Vector3(Direction * tempMoveSpeed, Rb.velocity.y, 0);
            if (InAir())
            {
                Rb.AddForce(new Vector3(0, -currentGravity, 0));
            }
        }
       
        
 
        
    }

    private void UpdateSwingPosition(float delta)
    {
        transform.position = Vector3.Lerp(transform.position, swingParent.GetSwingWorldPosition() - swingPositionOffset.localPosition, delta * 12.0f);
    }

    private bool IsOnSand()
    {
        Ray ray = new Ray(transform.position, new Vector2(0, -0.55f * transform.lossyScale.y));
        
        RaycastHit hit;
        bool onSand = Physics.Raycast(ray, out hit, int.MaxValue, onSandLayerMaskTest);
        if (onSand)
        {
            return hit.distance > 0;
        }
        return true;
    }

        private bool InAir()
    {
        Ray ray = new Ray(transform.position, new Vector2(0, -0.55f * transform.lossyScale.y));
        RaycastHit hit;
        bool inAir = Physics.Raycast(ray, out hit, int.MaxValue, inAirLayerMaskTest);

        if (inAir)
        {
            return hit.distance > 0;
        }
        return true;
    }

    public void OnPause(bool pause)
    {
        if (pause)
        {
            if (Rb.velocity != Vector3.zero)
            {
                savedVelocity = Rb.velocity;
                Rb.velocity = Vector3.zero;
            }
        }
        else
        {
            if (savedVelocity != Vector3.zero)
            {
                Rb.velocity = savedVelocity;
                savedVelocity = Vector3.zero;
            }
        }
    }

    public void EnterSwing(SwingerVine swingParent)
    {
        this.swingParent = swingParent;
    }

    private void ExitSwing()
    {
        if (swingParent != null)
        {
            swingParent.Exit();
            swingParent = null;
        }
    }
}