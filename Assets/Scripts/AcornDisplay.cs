using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AcornDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpro;
    private int acorns;
    public void UpdateDisplay(int amount)
    {
        tmpro.text = (acorns += amount).ToString();
    }
}
