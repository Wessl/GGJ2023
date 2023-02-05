using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundsToPlayOnHit;
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(soundsToPlayOnHit.Length > 0)
            {
                audioSource.clip = soundsToPlayOnHit[Random.Range(0, soundsToPlayOnHit.Length)];
                audioSource.Play();
            }
            Animator animator = other.gameObject.GetComponentInChildren<Animator>();
            if(!animator.GetBool("Dead"))
            {
                other.gameObject.GetComponentInChildren<Animator>().SetTrigger("Died");
                other.gameObject.GetComponentInChildren<Animator>().SetBool("Dead", true);
            }
        }
    }
}
