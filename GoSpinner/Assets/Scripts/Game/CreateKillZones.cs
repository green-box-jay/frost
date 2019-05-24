using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateKillZones : MonoBehaviour
{
    [Header("Prefab parametrs")]
    [SerializeField] private GameObject[] AllZonesPrefabs;
    [SerializeField] private Transform StartZone;
    [Header("Create parametrs")]
    [SerializeField] private int CountCreateZones = 7;
    public float OffsetNow = 5;
    [Header("Position parametrs")]
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private float DefaultY;
    [SerializeField] private float DefaultZ;
    [SerializeField] private int OffsetDestroy = 5;

    public bool CanSpawn = true;

    private Transform LastZone;
    private List<GameObject> CurrentZones = new List<GameObject>();

    CreateCoins CC;

    private void Start()
    {
        // Instalation
        CC = FindObjectOfType<CreateCoins>();

        LastZone = StartZone;

        for (int i = 0; i < CountCreateZones; i++)
        {
            CreateZone();
        }

        StartCoroutine(DestroyStartZone());
    }
    private void FixedUpdate()
    {
        if (CanSpawn)
            CheckSpawn();
    }
    
    private void CheckSpawn()
    {
        if (CurrentZones.Count != 0)
        {
            if (PlayerTransform.position.x >= CurrentZones[0].transform.position.x + OffsetDestroy)
                DestroyZone();
        }
    }
    private void CreateZone()
    {
        float StartCoinX = LastZone.transform.position.x;

        GameObject zone = Instantiate(AllZonesPrefabs[Random.Range(0, AllZonesPrefabs.Length)], transform);
        
        zone.transform.position = new Vector3(LastZone.position.x + OffsetNow, DefaultY, DefaultZ);

        float EndCoinX = zone.transform.position.x;

        CurrentZones.Add(zone);

        XCreateCoin xc = new XCreateCoin();

        xc.StartX = StartCoinX + 2;
        xc.EndX = EndCoinX - 2;

        CC.SpawnCoin(xc);

        LastZone = zone.transform;
    }
    private void DestroyZone()
    {
        Destroy(CurrentZones[0]);
        CurrentZones.RemoveAt(0);
        CreateZone();
    }
    
    private IEnumerator DestroyStartZone()
    {
        yield return new WaitForSeconds(10);
        Destroy(StartZone.gameObject);
    }
}
