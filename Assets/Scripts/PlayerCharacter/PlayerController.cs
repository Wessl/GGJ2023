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
    private float Gravity;
    private float currentGravity;
    [SerializeField]
    private LayerMask onSandLayerMaskTest;

    [SerializeField]
    private LayerMask inAirLayerMaskTest;
    
    private float Direction;
    private bool PressingJump;
    private bool PressingMovement;
    private Rigidbody RB;

    //private Vector3 savedVelocity = Vector3.zero;

    private SwingerVine swingParent = null;

    private Animator anim;

    [SerializeField]
    private Transform swingPositionOffset;

    private Rigidbody rb;

    private float gravity = 3.0f * 9.82f;
    private float xSpeed = 7.0f;
    private float targetX = 0.0f;
    private float velocityX = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    public void PlayerHorizontalMovement(InputAction.CallbackContext context)
    {

        targetX = context.ReadValue<float>() * xSpeed;
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        if(!InAir())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.4f * gravity, rb.velocity.z);
        }
    }

    private void FixedUpdate()
    {
        velocityX = Mathf.Lerp(velocityX, targetX, 13.0f * Time.fixedDeltaTime);
        //targetX = Mathf.Lerp(targetX, 0.0f, 0.1f);
        float g = rb.velocity.y > 0.0f ? gravity : gravity * 1.876f;
        rb.velocity = new Vector3(velocityX, rb.velocity.y - g * Time.fixedDeltaTime, rb.velocity.z);


        if (anim != null) anim.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        if (anim != null) anim.SetFloat("VelocityY", rb.velocity.y);


    }

    //private void Start()
    //{
    //    anim = GetComponentInChildren<Animator>();
    //    RB = GetComponent<Rigidbody>();
    //}

    //public void PlayerHorizontalMovement(InputAction.CallbackContext context)
    //{
    //    if (Manager.IsPaused) return;

    //    Direction = context.ReadValue<float>();

    //    if(Direction != 0)
    //    {
    //        PressingMovement = true;
    //    }
    //    else
    //    {
    //        PressingMovement = false;
    //    }

    //}
    //public void PlayerJump(InputAction.CallbackContext context)
    //{
    //    if (Manager.IsPaused) return;

    //    if (swingParent)
    //    {
    //        ExitSwing();
    //        return;
    //    }
    //    if(!context.ReadValueAsButton())
    //    {
    //        PressingJump = false;
    //        return;
    //    }
    //    if(!InAir())
    //    {
    //        PressingJump = true;
    //        RB.useGravity = false;
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if (Manager.IsPaused) return;
    //    var tempMoveSpeed = MyMovementSettings.MovementSpeed;
    //    if (IsOnSand())
    //    {
    //        Debug.Log("i am on sand");
    //        //currentGravity = Gravity; // You can set this to like 60 in order to get stuck to the floor when doing it but it looks kind of hacky
    //        tempMoveSpeed *= 2f;
    //    }
    //    // else
    //    // {
    //    //     currentGravity = Gravity;
    //    // }
    //    if (swingParent != null)
    //    {
    //        UpdateSwingPosition(Time.fixedDeltaTime);
    //        RB.useGravity = false;
    //    }

    //    float movement = 0;
    //    if (PressingMovement)
    //    {
    //        MyMovementSettings.MovementT = Mathf.Min(MyMovementSettings.MovementT + Time.deltaTime, 1);
    //    }
    //    else
    //    {
    //        MyMovementSettings.MovementT = Mathf.Max(MyMovementSettings.MovementT - (Time.deltaTime * 4), 0);
    //    }

    //    movement = Direction * MyMovementSettings.Movement.Evaluate(MyMovementSettings.MovementT) * tempMoveSpeed;

    //    float jump = 0;
    //    float jumpDirection = 0;
    //    if(MyJumpSettings.JumpT == 1 && !swingParent)
    //    {
    //        PressingJump = false;
    //        MyJumpSettings.JumpT = 0;
    //        RB.useGravity = true;
    //    }
    //    if (PressingJump)
    //    {
    //        MyJumpSettings.JumpT = Mathf.Min(MyJumpSettings.JumpT + Time.deltaTime * 2, 1);
    //        if (MyJumpSettings.JumpT <= 0.5f)
    //        {
    //            jumpDirection = 1;
    //        }
    //        else
    //        {
    //            jumpDirection = -1;
    //        }
    //    }
    //    else
    //    {
    //        MyJumpSettings.JumpT = Mathf.Max(MyJumpSettings.JumpT - (Time.deltaTime * 4), 0);
    //        jumpDirection = -1;
    //        if (MyJumpSettings.JumpT == 0 && !swingParent)
    //        {
    //            RB.useGravity = true;
    //        }
    //    }

    //    if(anim != null) anim.SetFloat("VelocityX", Mathf.Abs(RB.velocity.x));
    //    if(anim != null) anim.SetFloat("VelocityY", RB.velocity.y);

    //    jump = jumpDirection * MyJumpSettings.Jump.Evaluate(MyJumpSettings.JumpT) * MyJumpSettings.JumpHeight;

    //    if(jump > 0)
    //    {
    //        RB.AddForce(new Vector3(movement - RB.velocity.x, jump - RB.velocity.y, 0));
    //    }
    //    else
    //    {
    //        RB.AddForce(new Vector3(movement - RB.velocity.x, 0, 0));
    //    }

    //}

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
        return true;
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
            //if (Rb.velocity != Vector3.zero)
            //{
            //    savedVelocity = Rb.velocity;
            //    Rb.velocity = Vector3.zero;
            //}
        }
        else
        {
            //if (savedVelocity != Vector3.zero)
            //{
            //    Rb.velocity = savedVelocity;
            //    savedVelocity = Vector3.zero;
            //}
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
            RB.velocity = new Vector3(RB.velocity.x, 0, RB.velocity.z);
            RB.AddForce(swingParent.lerpFlipFlop ? Vector3.right : Vector3.left);
            swingParent = null;
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
        public AnimationCurve Movement;
        public float MovementSpeed;
        //[HideInInspector]
        public float MovementT;
    }
}
