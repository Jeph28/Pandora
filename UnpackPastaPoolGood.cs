using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpackPastaPoolGood : MonoBehaviour
{
    [SerializeField] private GameObject Pastaprefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> PastaList;
    private GameObject Pasta;
    private static UnpackPastaPoolGood instance;
    public static UnpackPastaPoolGood Instance { get { return instance; } }
   
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

    public GameObject RequestUnpackPastaGood()
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
        AddPastaToPool(1);
        PastaList[PastaList.Count- 1].SetActive(true);
        GameManager.UnpackOn++;
        return PastaList[PastaList.Count- 1];
    }
    

}