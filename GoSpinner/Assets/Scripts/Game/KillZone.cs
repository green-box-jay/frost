using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            FindObjectOfType<ScoreSystem>().LoseGame();
            print("lose");
        }
    }
}
