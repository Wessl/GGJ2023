using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other && other.gameObject.layer == LayerMask.NameToLayer("Player")) 
        {
            Destroy(GameObject.FindGameObjectWithTag("Manager"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
