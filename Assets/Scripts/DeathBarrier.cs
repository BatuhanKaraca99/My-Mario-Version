using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false); //disable player
            GameManager.Instance.ResetLevel(3f); //reset level with delay
        }
        else //if not character destroy 
        {
            Destroy(other.gameObject);
        }
    }
}
