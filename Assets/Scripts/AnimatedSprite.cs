using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() //Start Animation
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    private void OnDisable() //Finish Animation
    {
        CancelInvoke();
    }

    private void Animate() //Play Frames
    {
        frame++;

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        if(frame >= 0 && frame < sprites.Length) //bound control
        {
            spriteRenderer.sprite = sprites[frame];
        }
    }
}
