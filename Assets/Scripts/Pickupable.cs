using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    AudioManager audioManager;
    AcornDisplay acornDisplay;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        acornDisplay = FindObjectOfType<AcornDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            // You pick me up
            acornDisplay.UpdateDisplay(1);
            audioManager.PlaySound();
            Destroy(gameObject);
        }
    }
}
