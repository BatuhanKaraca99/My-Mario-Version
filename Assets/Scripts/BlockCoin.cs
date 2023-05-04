using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour //Handle coin animation from box
{
    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.AddCoin();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition; //coin's first position
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f; //move up

        yield return Move(restingPosition, animatedPosition); //move up
        yield return Move(animatedPosition, restingPosition); //move down

        Destroy(gameObject); //destroy coin after that
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.25f; //longer duration

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
