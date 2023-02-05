using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayerController : MonoBehaviour
{

    private Rigidbody rb;

    private float gravity = 3.0f * 9.82f;
    private float xSpeed = 7.0f;
    private float targetX = 0.0f;
    private float velocityX = 0.0f;
    private Animator anim;

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
        rb.velocity = new Vector3(rb.velocity.x, 0.28f * gravity, rb.velocity.z);
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

}
