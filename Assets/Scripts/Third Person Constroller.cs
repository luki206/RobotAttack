using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonConstroller : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cam;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSmoothingTime = 0.1f;
    private float turnSmoothvolocity;
    private float velocity;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField]private float gravityMultiplayer = 4f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movePos = new Vector3(horizontal,0f,vertical).normalized;

        if(characterController.isGrounded && velocity < 0f)
        {
            velocity = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            velocity += jumpForce;
        }

        velocity -= gravityMultiplayer * Time.deltaTime;
        movePos.y = velocity;       
        
        if(movePos.magnitude >= 0.1f)
        {
            if(horizontal != 0 || vertical != 0)
            {
                float targetAngle = Mathf.Atan2(movePos.x, movePos.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvolocity, turnSmoothingTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
            movePos = Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0) * movePos;
            characterController.Move(movePos * speed * Time.deltaTime);
        }
    }
}
