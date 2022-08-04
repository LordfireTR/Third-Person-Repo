using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    private float moveSpeed = 20.0f;
    [SerializeField] private Transform mainCam;

    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

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
        float linearSpeed = Time.deltaTime * moveSpeed;
        Vector3 vertical = Vector3.forward * Input.GetAxisRaw("Vertical");
        Vector3 horizontal = Vector3.right * Input.GetAxisRaw("Horizontal");
        Vector3 direction = (vertical + horizontal).normalized;

        float bodyAngle;
        if (direction != Vector3.zero)
        {
            bodyAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        }
        else
        {
            bodyAngle = 0;
        }

        float targetAngle = bodyAngle + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0, angle, 0);
        
        Vector3 moveDirection =  Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        if (direction != Vector3.zero)
        {
            controller.Move(moveDirection.normalized * linearSpeed);
        }
    }
}
