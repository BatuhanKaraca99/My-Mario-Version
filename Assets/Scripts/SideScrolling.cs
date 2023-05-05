using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player; //Mario's transform

    public float height = 7; //camera normal height
    public float undergroundHeight = -9f; //camera underground height

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

    public void SetUnderground(bool underground) //if we go to underground
    {
        Vector3 cameraPosition = transform.position; //get camera's current position
        //we will change only y if we are in underground
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
