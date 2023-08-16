using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text Timertext;
    [SerializeField, Tooltip("Tiempo en sg")] private float timeTimer;
    [SerializeField] GameObject gameOver;
    [SerializeField] private GameObject eventSystem;
    private float minutes, seconds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        timeTimer -= Time.deltaTime;

        minutes = (int)(timeTimer / 60f);
        seconds = (int)(timeTimer - minutes * 60f);

        Timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeTimer <= 0)
        {
            Destroy(this);
            gameOver.SetActive(true);
            eventSystem.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
