using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool shelled; //are we in shell state
    private bool pushed; //is shell moving


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag("Player")) //if koopa is not in shell state
        {
            Player player = collision.gameObject.GetComponent<Player>(); //our player

            //if the player hits to koopa while moving down 
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell(); //koopa enters to shell
            }
            else
            {
                player.Hit(); //player gets hit
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) //for shell movement with trigger
    {
        if(shelled && other.CompareTag("Player")) //if player moving shell
        {
            if (!pushed) //if shell is not moving
            {
                Vector2 direction = new Vector2(transform.position.x-other.transform.position.x,0f); //direction calculate,we only care about x axis
                PushShell(direction); //push shell
            }
            else //if shell is moving 
            {
                Player player = other.GetComponent<Player>();
                player.Hit(); //if we touch to moving shell we die
            } 
        }
        else if(!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell")) //koopa hit(maybe other shell)
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        //Disable some of them after shell mode
        //shell can need physics so this is why we dont disable
        shelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;
        //re-enable rigidbody2d
        GetComponent<Rigidbody2D>().isKinematic = false; //physic engine handle

        EntityMovement movement = GetComponent<EntityMovement>(); //re-enable custom entity movement
        movement.direction = direction.normalized; //we don't need magnitude in here,we need direction
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell"); //we gave shell tag
    }

    private void Hit() //koopa hit(maybe shell)
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed) //if it leaves screen, it will get destroyed
        {
            Destroy(gameObject);
        }
    }
}
