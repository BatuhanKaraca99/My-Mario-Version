using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody;
    private new Collider2D collider; //collider reference

    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f); //multiply divide with 2 because of parabolas
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f),2); 

    public bool grounded { get; private set; }
    public bool jumping { get;private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); //turning

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        camera = Camera.main;
    }

    private void OnEnable() //if mario is enabled
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }
    private void OnDisable() //if mario is disabled
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero; //make velocity 0
        jumping = false;
        
    }
    private void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.Raycast(Vector2.down); //(0,-1)

        if (grounded)
        {
            GroundedMovement();
        }

        if (rigidbody.Raycast(Vector2.right * velocity.x)) //are we hitting wall
        {
            velocity.x = 0f;
        } 

        ApplyGravity();
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x,inputAxis*moveSpeed,moveSpeed*Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump"); //are you pressing
        float multiplier = falling ? 2f : 1f; //extra gravity or not
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y=Mathf.Max(velocity.y,gravity/2f); //to prevent fall too fast
    }

    private void FixedUpdate() //For Physics
    {
        Vector2 position = rigidbody.position; //current position
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x=Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if mario bumps on enemy
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            //if colliding happens when going down
            if(transform.DotTest(collision.transform,Vector2.down))
            {
                velocity.y = jumpForce / 2f; //move up
                jumping = true;
            }
        }
        //up
        else if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")) //if not powerup
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
            //velocity.y = 0f;
        }
    }
}
