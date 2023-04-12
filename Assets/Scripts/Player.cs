using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer; //small sprite
    public PlayerSpriteRenderer bigRenderer; //big sprite

    private DeathAnimation deathAnimation; //death animation

    public bool big => bigRenderer.enabled; //big one is enabled
    public bool small => smallRenderer.enabled; //small one is enabled
    public bool dead => deathAnimation.enabled; //mario is dead

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit() //Mario hit
    {
        if (big)
        {
            Shrink();
        } else
        {
            Death();
        }

    }

    private void Shrink() //Big -> Small
    {
        //TODO
    }

    private void Death()
    {
        //We don't want to assign new sprites while this is happening
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;

        deathAnimation.enabled = true; //play animation

        GameManager.Instance.ResetLevel(3f); //after 3 second reset
    }
}
