 using System.Collections.Generic;

public static class GameManager 
{
    public static float Money = 10000f;
    public static bool DryerMachine = false;
    public static bool PackingMachine = false;
    public static int PastaScore = 0;
    public static int Batch = 0;
    public static bool activeStatePacking = false;
    public static bool activeStateDryer = false;
    public static int UnpackOn = 0;
    public static int pastaColor;
    public static List<int> pastaColorList = new List<int>();
    public static string pastaColorString;
    public static int pastaHumidity;
    public static List<float> pastaHumidityList = new List<float>();
    public static string pastaHumidityPercentageString;
    public static string pastaHumidityString;
    public static int WeightDeviation;
    public static List<float> pastaWeightList = new List<float>();
    public static int user_temperature;
    public static int user_time;
    public static int user_speed;
    public static string PastaState;
    public static bool Craking = false;
    public static List<string> pastaCrakingList = new List<string>();
    public static string pastaCrakingString;
    public static bool Microorganisms = false;
    public static List<string> pastaMicroorganismsList = new List<string>();
    public static string pastaMicroorganismsString;
    public static string DryerMachineEfficiencyString;
    public static List<float> DryerMachineEfficiencyList = new List<float>();
    public static string PackingMachineEfficiencyString;
    public static List<float> PackingMachineEfficiencyList = new List<float>();
    public static string WeightDeviationString;
    public static bool PanelControlState1 = true;
    public static bool PanelControlState2 = false;
    public static bool PanelControlState3 = false;
    public static bool CountDownActivateDryer = false;
    public static bool CountDownActivatePacking = false;
    public static bool NeedsMaintenanceDryer = false;
    public static int RawMaterial = 2;
    public static float Acidity;
    public static float Protein;
    public static float Ash;
    public static bool FailureDryer = false;
    public static bool FailurePacking = false;
}
