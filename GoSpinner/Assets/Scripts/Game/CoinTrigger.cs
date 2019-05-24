using UnityEngine;

public class CoinTrigger : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ScoreSystem>().AddCoin();
            Destroy(gameObject);
        }
    }
}
