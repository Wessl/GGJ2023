using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingerVine : MonoBehaviour
{
    private float angle = 0.0f;
    private bool lerpFlipFlop = true;
    private float smoothSwingRange = 25.0f;


    [SerializeField]
    private Transform collisionPivot;

    [SerializeField]
    private float smoothSwingSpeedScale = 1.8f;

    [SerializeField]
    private float smoothSwingMaxSpeed = 2.3f;

    [SerializeField]
    private float flipFlopThreashold = 0.2f;


    [SerializeField]
    private CapsuleCollider collider;


    private float exitTime = -10.0f;
    private float exitDuration = 0.7f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {



        if (lerpFlipFlop)
        {
            angle = Mathf.Lerp(angle, Mathf.Min(angle + smoothSwingMaxSpeed, smoothSwingRange), Time.deltaTime * smoothSwingSpeedScale);
            if (smoothSwingRange - angle <= flipFlopThreashold)
            {
                lerpFlipFlop = !lerpFlipFlop;
            }
        }
        else
        {
            angle = Mathf.Lerp(angle, Mathf.Max(angle - smoothSwingMaxSpeed, -smoothSwingRange), Time.deltaTime * smoothSwingSpeedScale);
            if (-smoothSwingRange - angle >= -flipFlopThreashold)
            {
                lerpFlipFlop = !lerpFlipFlop;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);


        if (!collider.enabled && exitTime + exitDuration <= Time.time && exitTime > 0.0f)
        {
            collider.enabled = true;
            exitTime = -10.0f;
        }

    }


    public void Exit()
    {
        exitTime = Time.time;
    }

    public Vector3 GetSwingWorldPosition()
    {
        return collisionPivot.position;
    }

    private void StartSwing(PlayerController player)
    {
        collider.enabled = false;
        player.EnterSwing(this);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 p = collision.GetContact(0).point;

            Vector3 offset = p - transform.position;

            float dist = -Vector3.Dot(transform.rotation * Vector3.down, offset);

            collisionPivot.localPosition = new Vector3(collisionPivot.localPosition.x, dist, collisionPivot.localPosition.z);

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            StartSwing(player);
        }
    }
}
