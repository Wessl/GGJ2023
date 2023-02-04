using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ClickPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void ClickCredits()
    {
        Debug.Log("todo");
    }
}
