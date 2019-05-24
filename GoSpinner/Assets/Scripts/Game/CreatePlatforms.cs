using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlatforms : MonoBehaviour
{
    [Header("Prefab parametrs")]
    [SerializeField] private GameObject[] ForestsPrefabs;
    [SerializeField] private int blocksCount = 7;
    [SerializeField] private int safeZone = 40;
    [Header("Position parametrs")]
    [SerializeField] private GameObject StartForest;
    [SerializeField] private Transform PlayerTransform;

    public bool CanSpawn = true;

    float blockXPos = 0;
    
    float blockLength = 0;

    List<GameObject> CurrentForests = new List<GameObject>();

    private void Start()
    {
        blockXPos = StartForest.transform.position.x;
        blockLength = StartForest.GetComponent<BoxCollider>().bounds.size.x;

        for (int i = 0; i < blocksCount; i++)
            SpawnForest();

        StartCoroutine(DestroyStartPlatform());
    }
    private void FixedUpdate()
    {
        if (CanSpawn)
            CheckSpawn();
    }

    private void CheckSpawn()
    {
        if (PlayerTransform.position.x - safeZone >= (blockXPos - blocksCount * blockLength))
        {
            SpawnForest();
            DestroyForest();
        }
    }
    private void SpawnForest()
    {
        GameObject block = Instantiate(ForestsPrefabs[Random.Range(0, ForestsPrefabs.Length)], transform);
        blockXPos += blockLength;
        block.transform.position = new Vector3(blockXPos, 0, 0);
        CurrentForests.Add(block);
    }
    private void DestroyForest()
    {
        Destroy(CurrentForests[0]);
        CurrentForests.RemoveAt(0);
    }

    private IEnumerator DestroyStartPlatform()
    {
        yield return new WaitForSeconds(10);
        Destroy(StartForest.gameObject);
    }
}
