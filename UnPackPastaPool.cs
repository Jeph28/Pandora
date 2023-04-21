using System.Collections.Generic;
using UnityEngine;

public class UnPackPastaPool : MonoBehaviour
{

    [SerializeField] private GameObject Pastaprefab;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> PastaList;

    private GameObject Pasta;

    private static UnPackPastaPool instance;
    public static UnPackPastaPool Instance { get { return instance; } }
   
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
    private void Start()
    {
        AddPastaToPool(poolSize);
    }
    private void AddPastaToPool (int amount)
    {
        
        for (int i = 0; i < amount; i++)
        {
            Pasta = Instantiate(Pastaprefab);
            Pasta.SetActive(false);
            PastaList.Add(Pasta);
            Pasta.transform.parent = transform;
        }
    }

    public GameObject RequestUnPackPasta()
    {
        for (int i = 0; i < PastaList.Count; i++)
        {
            if (!PastaList[i].activeSelf)
            {
                PastaList[i].SetActive(true);
                GameManager.UnpackOn++;
                return PastaList[i];
            }
        }
        return null;
    }
}