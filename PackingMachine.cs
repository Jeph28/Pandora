using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PackingMachine : MonoBehaviour
{
    private float Efficiency;
    private string EfficiencyStg;
    private float cost;
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
        if (GameManager.user_speed <= 30f)
        {
            GameManager.StdDevWeight = Random.Range(1, 3);
        }
        if (GameManager.user_speed > 30f && GameManager.user_speed <= 40f)
        {
            GameManager.StdDevWeight = Random.Range(2, 4);
        }
        if (GameManager.user_speed > 40f)
        {
            GameManager.StdDevWeight = Random.Range(3, 6);
        }

        GameManager.pastaStdDevWeightList.Add(GameManager.StdDevWeight);
    }

    public void EfficiencyMachine()
    {
        if (GameManager.user_speed <= 30f)
        {
            Efficiency = Random.Range(0.7f, 0.8f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.PackingMachineEfficiencyString = EfficiencyStg;

        }
        if (GameManager.user_speed > 30f && GameManager.user_speed <= 40)
        {
            Efficiency = Random.Range(0.6f, 0.7f);
            EfficiencyStg = Efficiency.ToString("F2");
            GameManager.PackingMachineEfficiencyString = EfficiencyStg;
        }
        if (GameManager.user_speed > 40f)
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

    public float speedPriceInquiry()
    {
        if (GameManager.user_speed <= 30f)
        {
            cost = 15f * GameManager.user_speed - 50f;
        }

        if (GameManager.user_speed > 30f && GameManager.user_speed <= 40f)
        {
            cost = 45f * GameManager.user_speed - 800f;
        }

        if (GameManager.user_speed > 40f )
        {
            cost = 50f * GameManager.user_speed - 1000f;
        }

        return cost;
    }
}
