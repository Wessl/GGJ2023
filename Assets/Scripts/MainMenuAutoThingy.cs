using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAutoThingy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Clamp01(Mathf.Sin(Time.time * 2)) * 3 + Mathf.Clamp01(Mathf.Sin(Time.time * 2 + Mathf.PI)) * 3 - 4, transform.position.z);
    }
}
