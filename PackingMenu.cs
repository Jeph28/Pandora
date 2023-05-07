using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject PackingMenu1;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BackToGame()
    {
        PackingMenu1.SetActive(false);
        GameManager.PackingMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
