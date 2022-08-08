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
    [SerializeField] private bool isJumping = false;

    //Handling Player Direction
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    float bodyAngle;
    float targetAngle;
    public float angle;

    //Hangling the Gun
    Transform gunPoint;
    [SerializeField] GameObject bullet;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gunPoint = transform.GetChild(4);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        FireBullet();
    }

    void PlayerMovement()
    {
        //Handling Movement Speed
        moveSpeed = 15.0f;

        //Getting Inputs as Vector3
        verticalVector = Vector3.forward * Input.GetAxisRaw("Vertical");
        horizontalVector = Vector3.right * Input.GetAxisRaw("Horizontal");
        directionVector = (verticalVector + horizontalVector).normalized;
        
        //Handling Jump
        gravityBase = -9.81f;
        jumpHeight = 2.0f;

        //Handling Player Direction
        bodyAngle = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg;
        
        targetAngle = bodyAngle + mainCam.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0, mainCam.eulerAngles.y, 0);
        
        moveDirection =  Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        //Handling Movement
        if (directionVector == Vector3.zero)
        {
            moveSpeed = 0;
        }

        if (!controller.isGrounded)
        {
            jumpVector += gravityBase * Vector3.up * Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpVector = Vector3.zero;
        }

        if (isJumping)
        {
            moveSpeed *= .8f;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVector = Vector3.up * Mathf.Sqrt(jumpHeight * -3.0f * gravityBase);
                Debug.Log("SPACE");
                isJumping = true;
            }
        }

        speedVector = moveDirection.normalized * moveSpeed + jumpVector;
        controller.Move(speedVector * Time.deltaTime);
    }

    void FireBullet()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, gunPoint.position, Quaternion.Euler(90, angle, 0));
        }
    }
}
