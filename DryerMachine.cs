using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DryerMachine : MonoBehaviour
{
    [SerializeField, Tooltip("Temperatura entre 80 y 110 grados")][Range(80, 110)] private int user_temp;
    [SerializeField, Tooltip("Timepo entre 180 y 360 minutos")][Range(180, 360)] private int user_time;
    private float Efficiency;
    private string EfficiencyStg;
    [SerializeField] private GameObject pastaPenne;
    [SerializeField] private Material RawPasta;
    [SerializeField] private Material GoodPasta;
    [SerializeField] private Material SemiBurntPasta;
    [SerializeField] private Material BurntPasta;
    [SerializeField] private TMP_Text Moneytext;
    private static DryerMachine instance;
    public static DryerMachine Instance { get { return instance; } }
    
    //private float timeSwitch = 0f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Color()
    {
        float ColorValue = user_temp * user_time;
        //Physical appearance
        if ( 14400f <= ColorValue && ColorValue <= 20000f)
        {
            GameManager.pastaColor = 1;
            GameManager.pastaColorString = "Pasta blanca 10B";
        }
        if ( 20000f < ColorValue && ColorValue <= 25000f)
        {
            GameManager.pastaColor = 2;
            GameManager.pastaColorString = "Pasta amarilla pollito 25B";
        }
        if ( 25000f < ColorValue && ColorValue <= 28000f)
        {
            GameManager.pastaColor = 3;
            GameManager.pastaColorString = "Pasta buena 35B";
        }
        if ( 28000f < ColorValue && ColorValue <= 34000f)
        {
            GameManager.pastaColor = 4;
            GameManager.pastaColorString = "Pasta Marron";
        }
        if (ColorValue > 34000f)
        {
            GameManager.pastaColor = 5;
            GameManager.pastaColorString = "Pasta negra -10B ";
        }
    }

     public void Humidity()
    {
        //Humidity percentage
        float HumidityPercentage;
        HumidityPercentage = -5 * Mathf.Log(0.0014f * user_time , 1.5f);
        GameManager.pastaHumidityPercentageString = HumidityPercentage.ToString("F2") + "%";
        
        if ( HumidityPercentage > 13.5f)
        {
            GameManager.pastaHumidityString = "Alto nivel de humedad";
        }
        if ( HumidityPercentage <= 13.5f && HumidityPercentage >= 12.6)
        {
            GameManager.pastaHumidityString = "Nivel normal de humedad";
        }
        if (HumidityPercentage < 12.6f)
        {
            GameManager.pastaHumidityString = "Bajo nivel de humedad";
        }
    }
       public void Craking()
    {
        if (user_temp * user_time >= 32400f)
        {
            GameManager.Craking = true;
            GameManager.pastaCrakingString = "Si";
        }
        else
        {
            GameManager.Craking = false;
            GameManager.pastaCrakingString = "No";
        }
    }

    public void Microbiological()
    {
        if (user_temp <= 85f && user_time >= 350f)
        {
            GameManager.Microorganisms = true;
            GameManager.pastaMicroorganismsString = "Si";
        }
        else
        {
            GameManager.Microorganisms = false;
            GameManager.pastaMicroorganismsString = "No";
        }
    }

    public void EfficiencyMachine()
    {
        if (user_time <= 240f)
        {
            Efficiency = Random.Range(0.7f,0.8f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.DryerMachineEfficiencyString = EfficiencyStg;
        }
        if (user_time > 240f && user_time <= 300)
        {
            Efficiency = Random.Range(0.6f, 0.7f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.DryerMachineEfficiencyString = EfficiencyStg;
        }
        if (user_time > 300f)
        {
            Efficiency = Random.Range(0.4f, 0.6f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.DryerMachineEfficiencyString = EfficiencyStg;
        }
    }

    public void TemperaturePrice()
    {
        if (user_temp <= 90f)
        {
            float cost = 50f * user_temp - 3500f;
            GameManager.Money -= cost;
        }

        if (user_temp > 90f && user_temp <= 100)
        {
            float cost = 100f * user_temp - 8000f;
            GameManager.Money -= cost;
        }

        if (user_temp > 100f)
        {
            float cost = 125f * user_temp - 10500f;
            GameManager.Money -= cost;
        }
    }
    // This is called in the ProximityActivity for best performance
    // public void SwitchOn(InputAction.CallbackContext callbackContext)
    // {
    //     // Se tienen 3 condiciones:
    //     // 1) La primera es que para que se ejecute la accion debes estar viendo el anuncio del switch
    //     // 2) Haber precionado el boton X
    //     // 3) Que la maquina secadora este encendida
    //     // De tal forma que solo se ejecute en el caso en el que se haya precionado el boton y estes efectivamente en la palanca

    //     if (GameManager.activeStateDryer && callbackContext.performed && GameManager.DryerMachine)
    //     {
    //         Color();
    //         Humidity();
    //         Craking();
    //         Microbiological();
    //         EfficiencyMachine();
    //         TemperaturePrice();
    //     }
    // }
}
