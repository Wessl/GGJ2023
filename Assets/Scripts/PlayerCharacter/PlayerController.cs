using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Movement")]
    [SerializeField]
    private JumpSettings MyJumpSettings;
    [SerializeField]
    private MovementSettings MyMovementSettings;

    [SerializeField] 
    private float gravity = 3.0f * 9.82f;

    [SerializeField]
    private LayerMask onSandLayerMaskTest;

    [SerializeField]
    private LayerMask inAirLayerMaskTest;

    private Vector3 savedVelocity = Vector3.zero;

    private Animator anim;

    [SerializeField]
    private Transform swingPositionOffset;

    private SwingerVine swingParent = null;
    private Rigidbody rb;

    private float targetX = 0.0f;
    private float velocityX = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    public void PlayerHorizontalMovement(InputAction.CallbackContext context)
    {
        targetX = context.ReadValue<float>() * MyMovementSettings.MovementSpeed;
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.canceled) return;

        if (Manager.IsPaused) return;

        if (swingParent)
        {
            ExitSwing();
            return;
        }
        if (!InAir())
        {
            ApplyJumpForce();
        }
    }

    private void ApplyJumpForce()
    { 
        rb.velocity = new Vector3(rb.velocity.x, 0.4f * gravity, rb.velocity.z);
    }

    private void FixedUpdate()
    {
        if (Manager.IsPaused) return;

        float xSpeedMultiplier = 1.0f;
        if (IsOnSand())
        {
            xSpeedMultiplier = MyMovementSettings.SandBoostMultiplier;
        }

        float velocityY = rb.velocity.y;
        if (swingParent != null)
        {
            xSpeedMultiplier = 0.0f;
            UpdateSwingPosition(Time.fixedDeltaTime);
        }
        else
        {
            float currentGravity = rb.velocity.y > 0.0f ? gravity : gravity * 1.876f;
            velocityY -= currentGravity * Time.fixedDeltaTime;
        }

        velocityX = Mathf.Lerp(velocityX, targetX * xSpeedMultiplier, 10.0f * Time.fixedDeltaTime);
        rb.velocity = new Vector3(velocityX, velocityY, 0f);

        Debug.Log(rb.velocity);
        if (anim != null) anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x * 0.75f));
        if (anim != null) anim.SetFloat("VelocityY", rb.velocity.y);
    }

    private void UpdateSwingPosition(float delta)
    {
        transform.position = swingParent.GetSwingWorldPosition() - swingPositionOffset.localPosition;//Vector3.Lerp(transform.position, swingParent.GetSwingWorldPosition() - swingPositionOffset.localPosition, delta * 12.0f);
    }

    private void OnDrawGizmos()
    {
        Ray ray = new Ray(transform.position + (Vector3.down * transform.lossyScale.y * 1.84f), Vector3.down);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
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

        return false;
    }

    private bool InAir()
    {
        Ray ray = new Ray(transform.position + (Vector3.down * transform.lossyScale.y * 1.84f), Vector3.down); //very magical number :))
        RaycastHit hit;
        bool inAir = Physics.Raycast(ray, out hit, MyJumpSettings.InAirTestOffset, inAirLayerMaskTest);

        if (!hit.collider)
        {
            return true;
        }
        return false;
    }

    public void OnPause(bool pause)
    {
        if (pause)
        {
            if (rb.velocity != Vector3.zero)
            {
                savedVelocity = rb.velocity;
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            if (savedVelocity != Vector3.zero)
            {
                rb.velocity = savedVelocity;
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
            float exitBoost = 38f;
            if (targetX == 0.0)
            {
                velocityX = (swingParent.lerpFlipFlop ? 1f : -1f) * exitBoost;
            }
            else
            {
                velocityX = (targetX > 0 ? 1f : -1f) * exitBoost;
            }
            swingParent = null;
            ApplyJumpForce();
        }
    }

    [System.Serializable]
    struct JumpSettings
    {
        public AnimationCurve Jump;
        public float JumpHeight;
        //[HideInInspector]
        public float JumpT;
        public float InAirTestOffset;
    }

    [System.Serializable]
    struct MovementSettings
    {
        //public AnimationCurve Movement;
        public float MovementSpeed;
        //[HideInInspector]
        //public float MovementT;

        public float SandBoostMultiplier;
    }
}
