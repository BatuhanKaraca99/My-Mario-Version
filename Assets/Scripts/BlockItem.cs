using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Animate()); 
    }

    private IEnumerator Animate() //Coroutine function
    {
        //while animating disable rigidbody etc.
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true; //we can't disable rigidbody but we can make sure physics don't have effect
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f); //wait little

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;

        while (elapsed < duration)
        {
            float t = elapsed / duration; //percentage of animation

            transform.localPosition = Vector3.Lerp(startPosition,endPosition,t);
            elapsed += Time.deltaTime; //how much time elapsed between frames

            yield return null;
        }
        transform.localPosition = endPosition; //for guarantee

        rigidbody.isKinematic = false; //activate them back
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }
}
