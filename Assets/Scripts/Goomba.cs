using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>(); //our player

            //if the player hits to goomba while moving down 
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten(); //goomba flatten
            } else
            {
                player.Hit(); //player gets hit
            }

        }
    }

    private void Flatten()
    {
        //Disable everything after flat
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f); //Destroy after 0.5 second
    }
}
