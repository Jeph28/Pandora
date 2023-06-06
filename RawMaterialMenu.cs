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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CanadianWheat()
    {
        GameManager.RawMaterial = 1;
        GameManager.Money -= 2200f;
        GameManager.Acidity = 0.080f;
        GameManager.Ash = 1f;
        GameManager.Protein = 14f;

        TextPanelControlDryer.text = "\n" + "Dirigete a la linea de produccion y configura la secadora y empaquetadora";
        // TextPanelControlPicking.text = "Mantenimiento preventivo de la empaquetadora: No disponible";
        BackToGame();
    }
    public void MexicanWheat()
    {
        GameManager.RawMaterial = 2;
        GameManager.Money -= 1800f;
        GameManager.Acidity = 0.070f;
        GameManager.Ash = 0.8f;
        GameManager.Protein = 12.8f;

        TextPanelControlDryer.text = "\n" + "\n" + "Dirigete a la linea de produccion y configura la secadora y empaquetadora";
        // TextPanelControlPicking.text = "Mantenimiento preventivo de la empaquetadora: No disponible";
        BackToGame();
    }
    public void BackToGame()
    {
        RawMaterialMenu1.SetActive(false);
        GameManager.RawMaterialMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
