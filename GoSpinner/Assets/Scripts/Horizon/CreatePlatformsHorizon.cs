using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatformsHorizon : MonoBehaviour
{
    [Header("Main parametrs")]
    [SerializeField] private GameObject[] AllPlatforms;
    [SerializeField] private int CountPlatforms = 5;
    [SerializeField] private int SafeZone = 40;

    [Header("Position parametrs")]
    [SerializeField] private GameObject StartPlatform;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform AllPlatformsObj;

    private float PlatformZPos;
    private float PlatformLength;

    List<GameObject> CurrentPlaforms = new List<GameObject>();

    private void Start()
    {
        PlatformZPos = StartPlatform.transform.position.z;
        PlatformLength = StartPlatform.transform.GetChild(0).GetComponent<BoxCollider>().bounds.size.z;

        for (int i = 0; i < CountPlatforms; i++)
            CreatePlatform();
    }
    private void FixedUpdate()
    {
        CheckSpawn();
    }

    private void CheckSpawn()
    {
        if (Player.position.z - SafeZone >= (PlatformZPos - CountPlatforms * PlatformLength))
        {
            CreatePlatform();
            DestroyPlatform();
        }
    }
    private void CreatePlatform()
    {
        GameObject platform = Instantiate(AllPlatforms[Random.Range(0, AllPlatforms.Length)], AllPlatformsObj);

        PlatformZPos += PlatformLength;
        platform.transform.position = new Vector3(0, 0, PlatformZPos);
        CurrentPlaforms.Add(platform);
    }
    private void DestroyPlatform()
    {
        Destroy(CurrentPlaforms[0]);
        CurrentPlaforms.RemoveAt(0);
    }
}
