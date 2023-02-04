using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float Length, StartPos;
    [SerializeField]
    private float ParallaxFactor;
    [SerializeField]
    private GameObject Cam;

    private void Start()
    {
        StartPos = transform.position.x;
        Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Update()
    {
        float temp = Cam.transform.position.x * (1 - ParallaxFactor);
        float distance = Cam.transform.position.x * ParallaxFactor;
        Vector3 newPosition = new Vector3(StartPos + distance, transform.position.y, transform.position.z);
        transform.position = newPosition;

        if(temp > StartPos + (Length / 2))
        {
            StartPos += Length;
        }
        else if(temp > StartPos - (Length / 2))
        {
            StartPos -= Length;
        }
    }
}
