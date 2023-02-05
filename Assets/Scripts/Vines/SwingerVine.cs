using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingerVine : MonoBehaviour
{
    private float angle = 0.0f;
    [HideInInspector]
    public bool lerpFlipFlop = true;

    [SerializeField]
    private float smoothSwingRangeStart = 25.0f;

    [SerializeField]
    private float smoothSwingRangeHoldingPlayer = 50.0f;



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
        smoothSwingRange = smoothSwingRangeStart;
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

        transform.localRotation = Quaternion.Euler(0, 0, angle);


        if (!collider.enabled && exitTime + exitDuration <= Time.time && exitTime > 0.0f)
        {
            collider.enabled = true;
            smoothSwingRange = smoothSwingRangeStart;
            smoothSwingSpeedScale = 3.2f;
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
        smoothSwingRange = smoothSwingRangeHoldingPlayer;
        smoothSwingSpeedScale = 6;
        player.EnterSwing(this);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 p = collision.GetContact(0).point;

            Vector3 offset = p - transform.position;

            float dist = Mathf.Min(-Vector3.Dot(transform.rotation * Vector3.down, offset), -2.8f);

            collisionPivot.localPosition = new Vector3(collisionPivot.localPosition.x, dist, collisionPivot.localPosition.z);

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            StartSwing(player);
        }
    }
}
