using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public TMP_Text Moneytext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMoneyValue()
    {
        Moneytext.text = GameManager.Money.ToString() + "$";
    }
}
