using UnityEngine;

public class CreateCoins : MonoBehaviour
{
    [Header("Prefab parametrs")]
    [SerializeField] private GameObject[] PrefabsCoins;
    [Header("Position parametrs")]
    [SerializeField] private XCreateCoin[] PosesRandom;
    [SerializeField] private float MaxDistanceIcon = 2;

    private Transform LastIcon;

    public void SpawnCoin(XCreateCoin poses)
    {
        int HowManyCreate = Random.Range(1, 3);

        for (int i = 0; i < HowManyCreate; i++)
        {
            if (LastIcon != null)
                poses.StartX = LastIcon.position.x;

            if (poses.EndX - poses.StartX > MaxDistanceIcon)
                CreateOneCoin(poses);
        }
    }
    private void CreateOneCoin(XCreateCoin poses)
    {
        GameObject PrefabCoin = PrefabsCoins[Random.Range(0, PrefabsCoins.Length)];

        GameObject ObjCoin = Instantiate(PrefabCoin, transform);
        Transform TransformCoin = ObjCoin.transform;

        float rightX = Random.Range(poses.StartX, poses.EndX);

        XCreateCoin RandomYZ = PosesRandom[Random.Range(0, PosesRandom.Length)];

        TransformCoin.position = new Vector3(rightX, RandomYZ.StartX, RandomYZ.EndX);

        LastIcon = TransformCoin;
    }
}
[System.Serializable]
public class XCreateCoin
{
    public float StartX;
    public float EndX;
}