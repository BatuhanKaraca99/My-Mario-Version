using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item; //item spawn
    public Sprite emptyBlock; //after hit
    public int maxHits = -1; //block hit count

    private bool animating;

    private void OnCollisionEnter2D(Collision2D collision) //detect when something hit block
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))//if mario hits to block
            {
                Hit();
            } 
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true; //especially for hidden mystery boxes

        maxHits--; //decrement value

        if(maxHits == 0) //become empty block
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if(item!= null)
        {
            Instantiate(item, transform.position, Quaternion.identity); //spawn item
        }

        StartCoroutine(Animate()); //start animate ienumerator
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition; //block's first position
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f; //move up little

        yield return Move(restingPosition, animatedPosition); //move up
        yield return Move(animatedPosition, restingPosition); //move down

        animating = false;
    }

    private IEnumerator Move(Vector3 from,Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t); //Linear Interpolation
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to; //make sure final position is to
    }
}
