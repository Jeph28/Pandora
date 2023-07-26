using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RawMaterialMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject RawMaterialMenu1;
    [SerializeField] private TMP_Text TextPanelControlDryer;
    [SerializeField] private TMP_Text TextPanelControlPicking;
    public Money money;
    

    public void CanadianWheat()
    {
        if (Settings.Instance.IsGamePaused())
            return;

        GameManager.RawMaterial = 1;
        GameManager.Money -= 2200f;
        money.ChangeMoneyValue();
        GameManager.Acidity = 0.080f;
        GameManager.Ash = 1f;
        GameManager.Protein = 14f;

        TextPanelControlDryer.text = "\n" + "Dirígete a la línea de producción y configura la secadora y empaquetadora, luego enciéndelas";
        BackToGame();
    }
    public void MexicanWheat()
    {
        if (Settings.Instance.IsGamePaused())
            return;
            
        GameManager.RawMaterial = 2;
        GameManager.Money -= 1800f;
        money.ChangeMoneyValue();
        GameManager.Acidity = 0.070f;
        GameManager.Ash = 0.8f;
        GameManager.Protein = 12.8f;

        TextPanelControlDryer.text = "\n" + "\n" + "Dirígete a la línea de producción y configura la secadora y empaquetadora, luego enciéndelas";
        BackToGame();
    }
    public void BackToGame()
    {
        RawMaterialMenu1.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.RawMaterialMenu = false;
    }
}
