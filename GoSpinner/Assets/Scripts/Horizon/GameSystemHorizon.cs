using UnityEngine;

public class GameSystemHorizon : MonoBehaviour
{
    public float MinX, MaxX;

    public static GameSystemHorizon Instance;

    private void Start()
    {
        Instance = this;
    }

    public void LoseGame()
    {
        print("Lose game :: GSH");
    }
}
