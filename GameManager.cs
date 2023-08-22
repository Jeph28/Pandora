 using System.Collections.Generic;

public static class GameManager 
{
    public static float Money = 45000f;
    public static bool DryerMachine = false;
    public static bool PackingMachine = false;
    public static int timeBetweenMaintenanceDryer = 150;
    public static int timeBetweenMaintenancePacking = 120;
    public static int MaintenanceExpirationDryer = 60;
    public static int MaintenanceExpirationPacking = 60;
    public static int PastaScore = 0;
    public static int previousUnpackPastaScore = 0;
    public static int Batch = 0;
    public static bool activeStatePacking = false;
    public static bool activeStateDryer = false;
    public static bool activeStateManager1 = false;
    public static bool activeStateManager2 = false;
    public static int UnpackOn = 0;
    public static bool hasCollidedPackingMachine;
    public static bool RequestUnpack = false;
    public static int pastaColor;
    public static List<string> pastaColorList = new List<string>();
    public static string pastaColorString;
    public static int pastaHumidity;
    public static List<float> pastaHumidityList = new List<float>();
    public static string pastaHumidityPercentageString;
    // public static string pastaHumidityString;
    public static List<float> pastaStdDevWeightList = new List<float>();
    public static List<float> pastaWeightList = new List<float>();
    public static List<float> pastaStdDevHumidity = new List<float>();
    public static List<string> resultTable = new List<string>();
    public static float user_previousTemperature = 80;
    public static float user_temperature = 80;
    public static float user_time = 180;
    public static float user_previousTime = 180;
    public static float user_speed = 20;
    public static float user_previousSpeed = 20;
    public static bool OffBottonCancelDryer = false;
    public static bool OffBottonCancelPacking = false;
    public static bool ChangeValueDryer = false;
    public static bool ChangeValuePacking = false;
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
    public static bool PanelControlState1 = false;
    public static bool PanelControlState2 = true;
    public static bool PanelControlState3 = false;
    public static bool CountDownActivateDryer = false;
    public static bool CountDownMaintenanceDryer = false;
    public static bool CountDownMaintenancePacking = false;
    public static bool CountDownActivatePacking = false;
    public static bool NeedsMaintenanceDryer = false;
    public static bool NeedsMaintenancePacking = false;
    public static bool DryerMenu;
    public static bool MaintenanceDryerMenu = false;
    public static bool MaintenancePackingMenu = false;
    public static bool PackingMenu;
    public static int MessageDryer = 1;
    public static int MessagePacking = 1;
    public static int RawMaterial = 0;
    public static float Acidity;
    public static float Protein;
    public static float Ash;
    public static bool FailureDryer = false;
    public static bool FailurePacking = false;
    public static bool changePrincipalText1CheckPoint1 = true;
    public static bool changePrincipalText2CheckPoint1 = false;
    public static bool changePrincipalText3CheckPoint1 = true;
    public static bool changePrincipalText1CheckPoint2 = true;
    public static bool changePrincipalText2CheckPoint2 = false;
    public static bool changePrincipalText3CheckPoint2 = true;
    public static float timeWaitCheckPoint1 = 10;
    public static float timeWaitCheckPoint2 = 10;
    public static float timeCheckPoint1;
    public static float timeCheckPoint2;
    public static bool ContextCheckPoint1;
    public static bool ContextCheckPoint2;
    public static bool MaintenanceDryer = false;
    public static bool MaintenancePacking = false;
    public static int MaintenanceCounterDryer = 0;
    public static int MaintenanceCounterPacking = 0;
    public static bool FailureRestartDryer = false;
    public static float CountDownMaintenanceTimeDryer = timeBetweenMaintenanceDryer;
    public static float CountDownMaintenanceTimePacking = timeBetweenMaintenancePacking;
    public static float MaintenanceTimeDryer = 10;
    public static float MaintenanceTimePacking = 10;
    public static int MaintenanceCostDryer;
    public static int MaintenanceCostPacking;
    public static bool ReadyMaintenanceDryer = false;
    public static bool ReadyMaintenancePacking = false;
    public static bool changeMessageMaintenanceDryer = true;
    public static bool changeMessageMaintenancePacking = true;
    public static float OpportunityMaintenanceDryer;
    public static float OpportunityMaintenancePacking;
    public static float StdDevHumidity;
    public static float StdDevWeight;
    public static int UnpackPastaScore = 0;
    public static bool RawMaterialMenu = false;
    public static float ScaleFailureDryer = 2000000f;
    public static float ScaleFailurePacking = 12000000f;
    public static string currentControl;
    public static bool InitialDryer = false;
    public static bool InitialPacking = false;
    public static List<float> batchSizeList = new List<float>();
    public static List<int> resultTableBinaryList = new List<int>();
    public static float DryerconveyorSpeed = 0;
    public static float PackingconveyorSpeed = 0;
}
