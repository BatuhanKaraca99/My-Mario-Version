using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type //what type of powerup
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other) //trigger
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject); //mario
        }
    }

    private void Collect(GameObject player) //player gameobject
    {
        switch (type)
        {
            case Type.Coin:
                GameManager.Instance.AddCoin();
                break;

            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                break;

            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;

            case Type.Starpower:
                player.GetComponent<Player>().Starpower();
                break;
        }

        Destroy(gameObject); //destroy gameobject
    }
}
