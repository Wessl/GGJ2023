using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    AudioManager audioManager;
    AcornDisplay acornDisplay;

    bool dead = false;
    float DeathTime = 0.0f;
    float DeathDuration = 0.43f;

    Vector3 dieScale;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        acornDisplay = FindObjectOfType<AcornDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            transform.localScale = Vector3.Lerp(dieScale, dieScale * 0.2f, (Time.time - DeathTime) / DeathDuration);
            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0, -360f * 1.6f, (Time.time - DeathTime) / DeathDuration));
            if (DeathTime + DeathDuration <= Time.time)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Die()
    {
        dieScale = transform.localScale;
        DeathTime = Time.time;
        dead = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (dead) return;

        if (collision.CompareTag("Player"))
        {
            // You pick me up
            acornDisplay.UpdateDisplay(1);
            audioManager.PlaySound();
            Die();
        }
    }
}
