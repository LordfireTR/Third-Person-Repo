using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] private Transform mainCam;

    //Handling Movement Speed
    private float moveSpeed;
    float linearSpeed;

    //Getting Inputs as Vector3
    Vector3 verticalVector;
    Vector3 horizontalVector;
    Vector3 directionVector;
    Vector3 moveDirection;
    Vector3 speedVector;
    
    //Handling Jump
    float gravityBase;
    float jumpHeight;
    Vector3 jumpVector;

    //Handling Player Direction
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    float bodyAngle;
    float targetAngle;
    float angle;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        //Handling Movement Speed
        moveSpeed = 20.0f;

        //Getting Inputs as Vector3
        verticalVector = Vector3.forward * Input.GetAxisRaw("Vertical");
        horizontalVector = Vector3.right * Input.GetAxisRaw("Horizontal");
        directionVector = (verticalVector + horizontalVector).normalized;
        
        //Handling Jump
        gravityBase = -30.0f;
        jumpHeight = 10.0f;

        //Handling Player Direction
        bodyAngle = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg;
        
        targetAngle = bodyAngle + mainCam.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0, angle, 0);
        
        moveDirection =  Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        //Handling Movement
        if (directionVector == Vector3.zero)
        {
            moveSpeed = 0;
        }

        if (!controller.isGrounded)
        {
            moveSpeed /= 2;
            jumpVector += gravityBase * Vector3.up * Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVector += Vector3.up * Mathf.Sqrt(jumpHeight * -3.0f * gravityBase);
                Debug.Log("SPACE");
            }
            if (jumpVector.y < 0)
            {
                //jumpVector = Vector3.zero;
            }
        }

        speedVector = moveDirection.normalized * moveSpeed + jumpVector;
        controller.Move(speedVector * Time.deltaTime);
    }
}
