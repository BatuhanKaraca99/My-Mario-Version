using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    //references
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f; //consistent
    public int nextWorld = 1;
    public int nextStage = 2;

    private void OnTriggerEnter2D(Collider2D other) //mario touches to flag
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position)); //flag animation
            StartCoroutine(LevelCompleteSequence(other.transform)); //mario animation
        }
    }

    //mario animation
    private IEnumerator LevelCompleteSequence(Transform player)
    {
        //disable mario's movement
        player.GetComponent<PlayerMovement>().enabled = false;

        yield return MoveTo(player, poleBottom.position); //1)Down mario
        yield return MoveTo(player, player.position + Vector3.right); //2)Go 1 unit right Mario
        //mario hits bottom block
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down); //3)Go 1 unit right and 1 unit down
        yield return MoveTo(player, castle.position); //4)Go to castle

        player.gameObject.SetActive(false);//after going castle deactivate mario

        //wait 2 seconds
        yield return new WaitForSeconds(2f);

        GameManager.Instance.LoadLevel(nextWorld,nextStage);
    }

    //flag or mario animation
    private IEnumerator MoveTo(Transform subject,Vector3 destination)
    {
        while(Vector3.Distance(subject.position,destination) > 0.125f) //while this animate
        {
            subject.position = Vector3.MoveTowards(subject.position,destination,speed * Time.deltaTime); //move over time
            yield return null; 
        }

        //after reaching almost to destination
        subject.position = destination; //guarantee
    }
}
