using UnityEngine;

public class KillZoneHorizon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameSystemHorizon.Instance.LoseGame();
    }
}
