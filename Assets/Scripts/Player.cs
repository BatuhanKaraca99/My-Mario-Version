using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer; //small sprite
    public PlayerSpriteRenderer bigRenderer; //big sprite
    private PlayerSpriteRenderer activeRenderer; //which one is active


    private DeathAnimation deathAnimation; //death animation
    private CapsuleCollider2D capsuleCollider; //we will change collider when big mario comes

    public bool big => bigRenderer.enabled; //big one is enabled
    public bool small => smallRenderer.enabled; //small one is enabled
    public bool dead => deathAnimation.enabled; //mario is dead
    public bool starpower { get; private set; } //are we in starpower

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallRenderer; 
    }

    public void Hit() //Mario hit
    {
        if (!dead && !starpower)
        {
            if (big)
            {
                Shrink();
            }
            else
            {
                Death();
            }
        }     
    }

    private void Death()
    {
        //We don't want to assign new sprites while this is happening
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;

        deathAnimation.enabled = true; //play animation

        GameManager.Instance.ResetLevel(3f); //after 3 second reset
    }

    public void Grow()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = true;
        activeRenderer = bigRenderer;

        capsuleCollider.size = new Vector2(1f, 2f); //change collider size
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    public void Shrink() //big->small
    {
        smallRenderer.enabled = true;
        bigRenderer.enabled = false;
        activeRenderer = smallRenderer;

        capsuleCollider.size = new Vector2(1f, 1f); //change collider size
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) //every 4 frames switch effect
            {
                smallRenderer.enabled = !smallRenderer.enabled; //if small make it big
                bigRenderer.enabled = !smallRenderer.enabled;  //if big make it small
            }

            yield return null;
        }

        smallRenderer.enabled = false; //for guarantee
        bigRenderer.enabled = false;
        activeRenderer.enabled = true;
    }

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 10f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) //we will change mario's colors every 4 frame
            {
                //We made sprite renderer public in its own class
                //We will change only hue, not saturation 
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f,1f,1f,1f,1f,1f); 
                
            }
            yield return null; //after if statement
        }

        activeRenderer.spriteRenderer.color = Color.white; //default sprite color
        starpower = false;
    }
}
