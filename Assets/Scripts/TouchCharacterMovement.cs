using System;
using UnityEngine;
using UnityEngine.AI;

public class TouchCharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    private int _leftFingerID;
    private int _rightFingerID;
    private float _halfScreenWidth;

    private Vector2 moveInput;
    private Vector2 moveTouchPosition;

    private Vector2 lookInput;
    public float cameraSensivity;
    private float cameraPitch;

    public Transform characterCamera;

    private void Start()
    {
        _leftFingerID = -1;
        _rightFingerID = -1;
    }

    void Update()
    {
        GetTouchInput();
        if (_leftFingerID != -1)
        {
            Move();
        }

        if (_rightFingerID != -1)
        {
            LookAround();
        }
    }

    void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began)
                {
                    if (t.position.x < _halfScreenWidth && _leftFingerID == -1)
                    {
                        _leftFingerID = t.fingerId;
                        moveTouchPosition = t.position;
                    }else if (t.position.x > _halfScreenWidth && _rightFingerID == -1)
                    {
                        _rightFingerID = t.fingerId;
                    }
                }

                if (t.phase == TouchPhase.Canceled)
                {
                    
                }
                
                if (t.phase == TouchPhase.Moved)
                {
                    if (_leftFingerID == t.fingerId)
                    {
                        moveInput = t.position - moveTouchPosition;
                    }else if (_rightFingerID == t.fingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensivity * Time.deltaTime;
                    }
                }
                
                if (t.phase == TouchPhase.Stationary)
                {
                    if (t.fingerId == _rightFingerID)
                    {
                        lookInput=Vector2.zero;
                    }
                }
                
                if (t.phase == TouchPhase.Ended)
                {
                    if (_leftFingerID == t.fingerId)
                    {
                        _leftFingerID = -1;
                    }

                    if (_rightFingerID == t.fingerId)
                    {
                        _leftFingerID = -1;
                    }
                }
            }

        }
    }

    void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Vector3 move = transform.right * moveInput.normalized.x + transform.forward * moveInput.normalized.x;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LookAround()
    {
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);
        characterCamera.localRotation=Quaternion.Euler(cameraPitch,0,0);
        
        transform.Rotate(transform.up,lookInput.x);
    }
}