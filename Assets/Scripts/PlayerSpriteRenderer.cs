using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; } //we made it public
    private PlayerMovement movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run; //to make priority

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false; //disable run animation
    }

    private void LateUpdate()
    {
        run.enabled = movement.running; //if it is running
        if (movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if(!movement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }
}
