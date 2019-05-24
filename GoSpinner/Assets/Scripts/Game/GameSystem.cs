using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [Header("Position parametrs")]
    [SerializeField] private Transform PlayerTransform;
    [Header("Data parametrs")]
    [SerializeField] private GameSettingsData Settings;

    private void Start()
    {
        SetParametrsSpiner();
    }

    private void SetParametrsSpiner()
    {
        MeshFilter MF = PlayerTransform.GetComponent<MeshFilter>();

        MF.mesh = Settings.NowChoosedSpiner.MeshSpiner;
    }
}
