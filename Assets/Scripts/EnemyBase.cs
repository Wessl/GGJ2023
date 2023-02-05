using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundsToPlayOnHit;
    [SerializeField] private AudioSource audioSource;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.clip = soundsToPlayOnHit[Random.Range(0, soundsToPlayOnHit.Length)];
            audioSource.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
