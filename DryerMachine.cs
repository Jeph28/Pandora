using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DryerMachine : MonoBehaviour
{
    private float Efficiency;
    private string EfficiencyStg;
    [SerializeField] private GameObject pastaPenne;
    [SerializeField] private Material RawPasta;
    [SerializeField] private Material GoodPasta;
    [SerializeField] private Material SemiBurntPasta;
    [SerializeField] private Material BurntPasta;
    [SerializeField] private TMP_Text Moneytext;
    private float cost;
    private static DryerMachine instance;
    public static DryerMachine Instance { get { return instance; } }
    
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
        float ColorValue = GameManager.user_temperature * GameManager.user_time;

        if (GameManager.RawMaterial == 2)
        {
            ColorValue = ColorValue * 0.8f;
        }

        //Physical appearance
        if ( ColorValue <= 20000f)
        {
            GameManager.pastaColor = 1;
            GameManager.pastaColorString = " 10B";
        }
        else if (20000f < ColorValue && ColorValue <= 25000f)
        {
            GameManager.pastaColor = 2;
            GameManager.pastaColorString = " 25B";
        }
        else if (25000f < ColorValue && ColorValue <= 28000f)
        {
            GameManager.pastaColor = 3;
            GameManager.pastaColorString = " 35B";
        }
        else if ( 28000f < ColorValue && ColorValue <= 34000f)
        {
            GameManager.pastaColor = 4;
            GameManager.pastaColorString = " -5B";
        }
        else if (ColorValue > 34000f)
        {
            GameManager.pastaColor = 5;
            GameManager.pastaColorString = " -10B";
        }

        GameManager.pastaColorList.Add(GameManager.pastaColorString);
    }

     public void Humidity()
    {
        //Humidity percentage
        float HumidityPercentage;
        HumidityPercentage = -5 * Mathf.Log(0.0014f * GameManager.user_time , 1.5f);

        if (GameManager.RawMaterial == 2)
        {
            HumidityPercentage = HumidityPercentage * 0.9f;
        }

        GameManager.pastaHumidityPercentageString = HumidityPercentage.ToString("F2") + "%";
        GameManager.pastaHumidityList.Add(HumidityPercentage);
        
        if ( HumidityPercentage > 13.5f)
        {
            GameManager.StdDevHumidity = Random.Range(3,6);
        }
        else if ( HumidityPercentage <= 13.5f && HumidityPercentage >= 12.6)
        {
            GameManager.StdDevHumidity = Random.Range(2,4);
        }
        else if (HumidityPercentage < 12.6f)
        {
            GameManager.StdDevHumidity = Random.Range(1,3);
        }

        GameManager.pastaStdDevHumidity.Add(GameManager.StdDevHumidity);
    }
       public void Craking()
    {
        if (GameManager.user_temperature > 100f)
        {
            GameManager.Craking = true;
            GameManager.pastaCrakingString = "Si";
        }
        else if (GameManager.user_time < 220f)
        {
            GameManager.Craking = true;
            GameManager.pastaCrakingString = "Si";
        }
        else
        {
            GameManager.Craking = false;
            GameManager.pastaCrakingString = "No";
        }

        GameManager.pastaCrakingList.Add(GameManager.pastaCrakingString);
    }

    public void Microbiological()
    {
        if (GameManager.user_temperature <= 85f && GameManager.user_time >= 350f)
        {
            GameManager.Microorganisms = true;
            GameManager.pastaMicroorganismsString = "Si";
        }
        else
        {
            GameManager.Microorganisms = false;
            GameManager.pastaMicroorganismsString = "No";
        }

        GameManager.pastaMicroorganismsList.Add(GameManager.pastaMicroorganismsString);
    }

    public void EfficiencyMachine()
    {
        if (GameManager.user_time <= 240f)
        {
            Efficiency = Random.Range(0.7f,0.8f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.DryerMachineEfficiencyString = EfficiencyStg;
        }
        else if (GameManager.user_time > 240f && GameManager.user_time <= 300)
        {
            Efficiency = Random.Range(0.6f, 0.7f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.DryerMachineEfficiencyString = EfficiencyStg;
        }
        else if (GameManager.user_time > 300f)
        {
            Efficiency = Random.Range(0.4f, 0.6f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.DryerMachineEfficiencyString = EfficiencyStg;
        }

        GameManager.DryerMachineEfficiencyList.Add(Efficiency);
    }

    public void TemperaturePrice()
    {
        if (GameManager.user_temperature <= 90f)
        {
            float cost = 50f * GameManager.user_temperature - 3500f;
            GameManager.Money -= cost;
        }
        else if (GameManager.user_temperature > 90f && GameManager.user_temperature <= 100)
        {
            float cost = 100f * GameManager.user_temperature - 8000f;
            GameManager.Money -= cost;
        }
        else if (GameManager.user_temperature > 100f)
        {
            float cost = 125f * GameManager.user_temperature - 10500f;
            GameManager.Money -= cost;
        }
    }

    public float temperaturePriceInquiry()
    {
        if (GameManager.user_temperature <= 90f)
        {
            cost = 50f * GameManager.user_temperature - 3500f;
        }
        else if (GameManager.user_temperature > 90f && GameManager.user_temperature <= 100)
        {
            cost = 100f * GameManager.user_temperature - 8000f;
        }
        else if (GameManager.user_temperature > 100f)
        {
            cost = 125f * GameManager.user_temperature - 10500f;
        }

        return cost;
    }

    public void BatchSize( int previousUnpackPastaScore, int currentUnpackPastaScore)
    {
        int currentBatchSize = currentUnpackPastaScore - previousUnpackPastaScore;
        GameManager.batchSizeList.Add(currentBatchSize);
        Debug.Log(currentBatchSize);
        Debug.Log(currentUnpackPastaScore);
        Debug.Log(previousUnpackPastaScore);
    }
}
