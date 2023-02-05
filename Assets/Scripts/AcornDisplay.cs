using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AcornDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpro;
    private int acorns;

    private Animator anim;

    private void Awake()
    {
        Debug.Log("not destroying what ? ");
        Debug.Log(this.transform.parent.name);
        DontDestroyOnLoad(this.transform.parent.parent);
    }

    public void Start()
    {
        
        anim = GetComponent<Animator>();
    }

    public void UpdateDisplay(int amount)
    {
        tmpro.text = (acorns += amount).ToString();
        anim.SetTrigger("GainScore");
    }
}
