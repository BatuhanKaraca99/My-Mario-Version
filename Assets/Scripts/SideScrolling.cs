using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player; //Mario's transform

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

    }

    private void LateUpdate() //After everything rendered
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
        //For Left Movement
        /*
        cameraPosition.x = player.position.x;
        transform.position = cameraPosition;
        */
    }
}
