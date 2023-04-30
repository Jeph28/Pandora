using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PackingMachine : MonoBehaviour
{
    [SerializeField, Tooltip("Velocidad entre 20 a 50 bpm")] private int user_speed;
    private float Efficiency;
    private string EfficiencyStg;
    private static PackingMachine instance;
    public static PackingMachine Instance { get { return instance; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
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

    public void Weight()
    {
        if (user_speed <= 30f)
        {
            GameManager.user_speed = user_speed; 
            GameManager.WeightDeviation = -1;
            GameManager.WeightDeviationString = "1% Menos";
        }
        if (user_speed > 30f && user_speed <= 40f)
        {
            GameManager.user_speed = user_speed;
            GameManager.WeightDeviation = 1;
            GameManager.WeightDeviationString = "1% Mas";
        }
        if (user_speed > 40f)
        {
            GameManager.user_speed = user_speed;
            GameManager.WeightDeviation = 2;
            GameManager.WeightDeviationString = "2% Mas";
        }

        GameManager.pastaWeightList.Add(GameManager.WeightDeviation);
    }

    public void EfficiencyMachine()
    {
        if (user_speed <= 30f)
        {
            Efficiency = Random.Range(0.7f, 0.8f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.PackingMachineEfficiencyString = EfficiencyStg;

        }
        if (user_speed > 30f && user_speed <= 40)
        {
            Efficiency = Random.Range(0.6f, 0.7f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.PackingMachineEfficiencyString = EfficiencyStg;
        }
        if (user_speed > 40f)
        {
            Efficiency = Random.Range(0.4f, 0.6f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.PackingMachineEfficiencyString = EfficiencyStg;
        }

        GameManager.PackingMachineEfficiencyList.Add(Efficiency);
    }

    public void SpeedPrice()
    {
        if (GameManager.user_speed <= 30f)
        {
            float cost = 15f * GameManager.user_speed - 50f;
            GameManager.Money -= cost;
        }

        if (GameManager.user_speed > 30f && GameManager.user_speed <= 40f)
        {
            float cost = 45f * GameManager.user_speed - 800f;
            GameManager.Money -= cost;
        }

        if (GameManager.user_speed > 40f )
        {
            float cost = 50f * GameManager.user_speed - 1000f;
            GameManager.Money -= cost;
        }
    }
}
