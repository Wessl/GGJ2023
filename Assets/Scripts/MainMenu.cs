using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditsPanel;
    public void ClickPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void ClickCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
    }

    public void ClosePanel()
    {
        creditsPanel.SetActive(false);
    }
}
