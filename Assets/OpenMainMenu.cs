using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMainMenu : MonoBehaviour
{

    float elapsed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        elapsed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsed + 15.0f <= Time.time)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }





}
