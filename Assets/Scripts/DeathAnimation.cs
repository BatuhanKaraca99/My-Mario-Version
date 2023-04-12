using UnityEngine;
using System.Collections; //for coroutines

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; //the sprite we want to update
    public Sprite deadSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() //when death animation is enabled
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10; //top of everything

        if(deadSprite != null) //make sure it is not null
        {
            spriteRenderer.sprite = deadSprite;
        }
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>(); //Gather all colliders

        foreach(Collider2D collider in colliders)
        {
            collider.enabled = false; //objects can fall so this is why
        }

        GetComponent<Rigidbody2D>().isKinematic = true; //we make sure physics engine is not effecting

        //Disable custom movements scripts, make sure they are not null
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if(playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if(entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }

    private IEnumerator Animate() //needs yield,fixed sequence
    {
        //Animate needs coroutine,so we can animate this over time(not only one time)
        //without using Update function
        float elapsed = 0f; //start time
        float duration = 3f;//duration time

        //we will make dead guy jump up and fall down
        float jumpVelocity = 10f;
        float gravity = -36; //our random numbers can customized

        Vector3 velocity = Vector3.up * jumpVelocity;

        while(elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime; //character position
            velocity.y += gravity * Time.deltaTime; //character's vertical speed(gravity s^2)
            elapsed += Time.deltaTime; //how much time passed
            yield return null; //yield every frame and wait until next frame continue, for every frame
        }
    }
}
