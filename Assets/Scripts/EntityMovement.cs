using UnityEngine;
//this code can be disabled, it will be enable for us itself
public class EntityMovement : MonoBehaviour //we will use this for enemies and mushroom too
{
    public float speed = 1f; //speed
    public Vector2 direction = Vector2.left; //(-1,0) //direction

    private new Rigidbody2D rigidbody;
    private Vector2 velocity; //velocity

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false; //first make sure it is no enable
    }

    private void OnBecameVisible() //Visible
    {
        enabled = true; //Enable
    }

    private void OnBecameInvisible() //Invisible
    {
        enabled = false; //Disable
    }

    private void OnEnable() //Enable -> Activate
    {
        rigidbody.WakeUp(); //activate rigidbody
    }

    private void OnDisable() //Disable -> What will happen
    {
        rigidbody.velocity = Vector2.zero; //Clear current velocity
        rigidbody.Sleep();
    }

    private void FixedUpdate() 
    {
        velocity.x = direction.x * speed; //x-axis movement = direction * speed
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime; //Unity's gravity (changes depends on time)
        //gravity metres per second per second (second=Time.fixedDeltaTime)
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime); //rigidbody movement

        if (rigidbody.Raycast(direction)) //if the rigidbody that raycast direction is currently moving
        {
            direction = -direction; //reverse direction
        }

        if (rigidbody.Raycast(Vector2.down)) //if the rigidbody that raycast direction is going down(0,-1)
        {
            velocity.y = Mathf.Max(velocity.y, 0f); //the dropping velocity can either be 0 or positive value
            //to prevent large number
        }
    }
}
