using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankeBubbla : MonoBehaviour
{
    [SerializeField] GameObject[] tankeBubblor;
    [SerializeField] GameObject fotboll;
    [SerializeField] float delayBetweenAppearance;
    [SerializeField] TextMeshProUGUI tmpro;
    private string tmpro_text;

    private void Start()
    {
        tmpro_text = tmpro.text;
        tmpro.text = "";
        StartCoroutine(ShowTankeBubblor());   
    }

    IEnumerator ShowTankeBubblor()
    {
        yield return new WaitForSeconds(delayBetweenAppearance);
        for(int i = 0; i < tankeBubblor.Length; i++)
        {
            tankeBubblor[i].SetActive(true);
            yield return new WaitForSeconds(delayBetweenAppearance);
        }
        
        var letters = tmpro_text.ToCharArray();
        for (int i = 0; i < letters.Length; i++)
        {
            tmpro.text += tmpro_text[i];
            yield return new WaitForSeconds(delayBetweenAppearance/10f);
        }

        yield return new WaitForSeconds(1f);
        fotboll.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
