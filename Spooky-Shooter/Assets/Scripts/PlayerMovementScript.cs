using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //This is the script we use in order to make the player move with the WASD keys and rotate depending on the mouse position

    public LayerMask raycastLayer;

    //WE'LL SPECIFY THE DESIRED MOVING SPEED IN THE EDITOR
    [Header("The Player Moving Speed")]
    [SerializeField] private float movingSpeed = 3;

    //AND WE'LL NEED TO STORE A FEW COMPONENTS FOR MOVING AND ANIMATING
    private Vector3 lookPos;
    private Animator anim;
    private Rigidbody rigidBody;

    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;


    //WE'LL FIRST NEED TO TO ACCESS THE ANIMATOR AND THE RIGIDBODY FOR THE PLAYER
    //IN ORDER TO MOVE IT AND TO TRIGGER THE ANIMATION WHILE DOING SO
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
    }


    //IN THE UPDATE FUNCTION WE'LL MAKE THE PLAYER TO HAVE THE DIRECTION BASED ON THE MOUSE POSITION
    //AND WE'LL DO THAT THROUGH A RAYCAST
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, raycastLayer))
        {
            lookPos = hit.point;

            lookPos.y = transform.position.y;
            transform.LookAt(lookPos, Vector3.up);
        }
    }


    //AND IN THE FIXEDUPDATE() WE'LL MAKE THE PLAYER MOVE AND TRIGGER THE ANIMATION WHILE DOING SO
    //OF COURSE WE'LL DO THAT ONLY WHILE WE PRESS THE
    //KEY INSTEAD OF USING EVERY FRAME LIKE THE UPDATE() FROM ABOVE
    void FixedUpdate()
    {
        Vector3 vel = new Vector3(0.0f, rigidBody.velocity.y, 0.0f);

        if(Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            vel.x = horizontalInput * movingSpeed;
            vel.z = verticalInput * movingSpeed;
            rigidBody.velocity = vel;
        }

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            anim.SetBool("Moving Player", true);
        }
        else
        {
            anim.SetBool("Moving Player", false);
        }
    }
}