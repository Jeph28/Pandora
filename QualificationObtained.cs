using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class QualificationObtained : MonoBehaviour
{
    [SerializeField] private TMP_Text paragraph;
    public DryerMachine dryerMachine;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Money >= 0)
        {
            paragraph.text = "Ya visualizaste tus resultados obtenidos por cada lote, tu calificación de desempeño es " + dryerMachine.userResult().ToString("F0") + " , esta contempla los lotes que pasaron la evaluación de calidad de todas las propiedades nombradas en la simulación y es una escala del 0 al 100. Además, tienes un saldo disponible de " + GameManager.Money.ToString() + "$." + "\n" + "\n" + "La evaluación de la calidad se utiliza para desarrollar estándares de aprendizaje y evaluaciones que ayudan a garantizar que estén aprendiendo los conocimientos y habilidades que necesitan para tener éxito, es por ello que, si tu calificación fue baja puedes iterar nuevamente para arreglar los aspectos negativos y obtener una mayor calificación.";
        }
        else
        {
            paragraph.text = "Ya visualizaste tus resultados obtenidos por cada lote, tu calificación de desempeño es " + dryerMachine.userResult().ToString("F0") + " , esta contempla los lotes que pasaron la evaluación de calidad de todas las propiedades nombradas en la simulación y es una escala del 0 al 100. Además, utilizaste totalmente el presupuesto" + "\n" + "\n" + "La evaluación de la calidad se utiliza para desarrollar estándares de aprendizaje y evaluaciones que ayudan a garantizar que estén aprendiendo los conocimientos y habilidades que necesitan para tener éxito, es por ello que, si tu calificación fue baja puedes iterar nuevamente para arreglar los aspectos negativos y obtener una mayor calificación.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
