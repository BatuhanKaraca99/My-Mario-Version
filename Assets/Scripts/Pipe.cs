using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public AudioSource source;
    public AudioClip pipe; //pipe sound

    public Transform connection; //where will pipe connect/lead
    public KeyCode enterKeyCode = KeyCode.S; //default key (S for now)
    public Vector3 enterDirection = Vector3.down; //enter direction
    public Vector3 exitDirection = Vector3.zero; //exit direction (default zero)  

    //is mario standing
    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode))
            {
                //animate mario
                StartCoroutine(Enter(other.transform)); //passing mario's transform
            }
        }
    }

    private IEnumerator Enter(Transform player) //mario's transform
    {
        source.PlayOneShot(pipe);
        //disable mario's movement
        player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection; //pipe position + enterDirection(out to in)
        Vector3 enteredScale = Vector3.one * 0.5f;//to make sure mario fits in pipe

        yield return Move(player, enteredPosition, enteredScale); //start mario animation
        yield return new WaitForSeconds(1f); //pause 1 second

        bool underground = connection.position.y < 0f; //if y position below to 0
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground);

        if (exitDirection != Vector3.zero) //if exiting point is not zero
        {
            player.position = connection.position - exitDirection; //in to out
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else //no animation needed
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }
        source.PlayOneShot(pipe);
        player.GetComponent<PlayerMovement>().enabled = true; 
    }

    private IEnumerator Move(Transform player,Vector3 endPosition,Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t); //linearly mario is moving with percentage
            player.localScale = Vector3.Lerp(startScale, endScale, t); //scaling
            elapsed += Time.deltaTime;

            yield return null; //to continue to next frame 
        }
        //for guarantee
        player.position = endPosition;
        player.localScale = endScale;
    }
}
