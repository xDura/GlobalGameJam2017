using UnityEngine;
using System.Collections;

public class LunkController : MonoBehaviour {

    Rigidbody2D rigidBody;
    Animator animator;
    bool canMove;
    bool moving;

    public Vector2 currentSpeed;
    public float maxSpeed;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Init()
    {
        currentSpeed = Vector2.zero;
        canMove = true;
        moving = false;
    }

	void Start ()
    {
        Init();
	}
	
	void Update ()
    {
        UpdateInput();
	}

    public void UpdateInput()
    {
        if (canMove)
        {
            //float lastHorizontal = animator.GetFloat("dirX");
            //float lastVertical = animator.GetFloat("dirY");
            float horizontal = 0.0f;
            float vertical = 0.0f;

            if (Input.GetKey(KeyCode.W))
                vertical = 1;
            else if (Input.GetKey(KeyCode.S))
                vertical = -1;

            if (Input.GetKey(KeyCode.D))
                horizontal = 1;
            else if (Input.GetKey(KeyCode.A))
                horizontal = -1;

            moving = (vertical != 0) || (horizontal != 0);
            animator.SetBool("walking", moving);
            if (!moving)
            {
                currentSpeed.x = 0.0f;
                currentSpeed.y = 0.0f;
                return;
            }

            currentSpeed.x = horizontal;
            currentSpeed.y = vertical;
            currentSpeed = (currentSpeed.normalized * maxSpeed);
            animator.SetFloat("dirX", horizontal);
            animator.SetFloat("dirY", vertical);
        }
    }

    void FixedUpdate()
    {
        rigidBody.velocity = currentSpeed;
    }
}
