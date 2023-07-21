using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpackPastaPoolSemiBernt : MonoBehaviour
{
    [SerializeField] private GameObject Pastaprefab;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> PastaList;
    private GameObject Pasta;
    private static UnpackPastaPoolSemiBernt instance;
    public static UnpackPastaPoolSemiBernt Instance { get { return instance; } }
   
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

    public GameObject RequestUnpackPastaSemiBernt()
    {
        for (int i = 0; i < PastaList.Count; i++)
        {
            if (!PastaList[i].activeSelf)
            {
                PastaList[i].SetActive(true);
                DeleteUnpack deleteUnpack1 = PastaList[i].GetComponent<DeleteUnpack>();
                deleteUnpack1.status = true;
                GameManager.UnpackOn++;
                GameManager.UnpackPastaScore++;
                return PastaList[i];
            }
        }
        AddPastaToPool(1);
        PastaList[PastaList.Count- 1].SetActive(true);
        DeleteUnpack deleteUnpack2 = PastaList[PastaList.Count- 1].GetComponent<DeleteUnpack>();
        deleteUnpack2.status = true;
        GameManager.UnpackOn++;
        GameManager.UnpackPastaScore++;
        return PastaList[PastaList.Count- 1];
    }
    

}
