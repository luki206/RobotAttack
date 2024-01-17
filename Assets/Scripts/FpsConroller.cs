using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float camSensitivity = 20f;
    private float xRotation;
    private float yRotation;
    private float gravityMultiplayer = 9.81f;
    private float velocity;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Variables
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * camSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * camSensitivity;

        Vector3 movePos = new Vector3(horizontal,0,vertical);
        movePos = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * movePos;

        //Jumping
        if(characterController.isGrounded && velocity < 0f)
        {
            velocity = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            velocity += jumpForce;
        }
        velocity -= gravityMultiplayer * Time.deltaTime;

        //Movement
        movePos *= speed;
        movePos.y = velocity;

        characterController.Move(movePos * Time.deltaTime);
    }
}
