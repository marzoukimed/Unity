using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerMouvement : MonoBehaviour
{


    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredLane= 1;  //0:left 1:middle 2:right
    public float laneDistance = 4; //distance between two lanes

    public float maxSpeed;
    public float jumpForce;
    public float Gravity = -20;

    public Animator animator;

    public Button tiltControlButton;
    public Color tiltControlEnabled;
    public Color tiltControlDisabled;
   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        tiltControlButton.GetComponent<Image>().color = tiltControlDisabled;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!PlayerManagement.isGameStarted)
            return;

        //increase speed
        if(forwardSpeed<maxSpeed)
        {
            forwardSpeed += 0.3f * Time.deltaTime;
        }

        direction.z = forwardSpeed;

        if (controller.isGrounded)
        {
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }

       

        if (SwipeManager.swipeDown)
        {
            StartCoroutine(Slide());
        }
        //the inputs on which lane we should be

        

        if (tiltControlButton.GetComponent<Image>().color == tiltControlEnabled)
        {
            UsingAccelerometer();
        }
        else if (tiltControlButton.GetComponent<Image>().color == tiltControlDisabled)
        {
            UsingSwipe();
        }


        //calculate where should we be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

       

        if (desiredLane ==0 )
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = targetPosition;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else controller.Move(diff);
    }

    private void UsingSwipe()
    {
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
    }


    private void UsingAccelerometer()
    {
        if (Input.acceleration.x > 0)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (Input.acceleration.x < 0)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
    }
    private void FixedUpdate()
    {
        if (!PlayerManagement.isGameStarted)
            return;
        controller.Move(direction * Time.deltaTime);
    }


    private void Jump()
    {
        direction.y = jumpForce;
    }

    //when the player hit an obstacle
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManagement.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        animator.SetBool("isSliding", true);

        yield return new WaitForSeconds(1.3f);

        animator.SetBool("isSliding", false);
    }

    public void OnTiltControl()
    {
       
        if (tiltControlButton.GetComponent<Image>().color == tiltControlEnabled)
        {
            tiltControlButton.GetComponent<Image>().color = tiltControlDisabled;
        }else if (tiltControlButton.GetComponent<Image>().color == tiltControlDisabled)
        {
            tiltControlButton.GetComponent<Image>().color = tiltControlEnabled;
        }
    }


}
