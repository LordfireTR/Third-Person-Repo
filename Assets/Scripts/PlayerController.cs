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

    //Handling Gun Controls
    Transform gunPoint;
    [SerializeField] GameObject bullet;

    //Handle Pause Game
    public GameObject pauseScreen;
    public bool isPaused;

    //Handle Game Over
    public GameObject gameOverScreen;
    public bool isGameOver;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gunPoint = transform.GetChild(4);
        //playerBody = transform.GetChild(2);
        //defaultHeight = controller.height;

        pauseScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        isGameOver = false;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(!isPaused && !isGameOver)
            {
                FireBullet();
            }
        }
        
        PauseGame();

        if (isGameOver)
        {
            GameOver();
        }
        //Crouch();
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
        float offsetAxisX = 12;
        offsetAxisX = 90 - offsetAxisX;
        gunPoint.rotation = mainCam.transform.rotation;
        gunPoint.Rotate(offsetAxisX, 0, 0, Space.Self);
        Instantiate(bullet, gunPoint.position, gunPoint.rotation);
    }

    void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver)
            {
                Time.timeScale = 1 - Time.timeScale;
                isPaused = !isPaused;
                pauseScreen.gameObject.SetActive(isPaused);
            }
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    // void Crouch()
    // {
    //     float crouchScale = 0.5f;

    //     if(Input.GetKeyDown(KeyCode.LeftControl))
    //     {
    //         isCrouching = !isCrouching;
    //     }

    //     if(isCrouching)
    //     {
    //         playerBody.transform.localScale = new Vector3(1, crouchScale, 1);
    //         controller.height = defaultHeight * crouchScale;
    //     }
    //     else
    //     {
    //         playerBody.transform.localScale = Vector3.one;
    //         controller.height = defaultHeight;
    //     }
    // }
}
