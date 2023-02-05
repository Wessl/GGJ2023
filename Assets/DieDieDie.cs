using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieDieDie : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Died");
        Destroy(GameObject.FindGameObjectWithTag("Manager"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
