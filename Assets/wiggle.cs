using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiggle : MonoBehaviour
{
    private float angle = 0.0f;
    private bool flipflop = false;

    [SerializeField]
    private float smoothSwingRange = 3.5f;

    [SerializeField]
    private float smoothSwingSpeedScale = 1.8f;

    [SerializeField]
    private float smoothSwingMaxSpeed = 2.3f;

    [SerializeField]
    private float flipFlopThreashold = 1.0f;

    void Update()
    {
        if (flipflop)
        {
            angle = Mathf.Lerp(angle, Mathf.Min(angle + smoothSwingMaxSpeed, smoothSwingRange), Time.deltaTime * smoothSwingSpeedScale);
            if (smoothSwingRange - angle <= flipFlopThreashold)
            {
                flipflop = !flipflop;
            }
        }
        else
        {
            angle = Mathf.Lerp(angle, Mathf.Max(angle - smoothSwingMaxSpeed, -smoothSwingRange), Time.deltaTime * smoothSwingSpeedScale);
            if (-smoothSwingRange - angle >= -flipFlopThreashold)
            {
                flipflop = !flipflop;
            }
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
