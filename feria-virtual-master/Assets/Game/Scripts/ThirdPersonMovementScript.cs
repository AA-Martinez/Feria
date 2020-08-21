using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public float Speed = 6f;
    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;
    public bool IsRunning;

    public bool Immobile;
    public GameObject quiz;
    void Start()
    {
        Immobile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Immobile)
        {
            Movement();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.X))
        {
            quiz.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = true;
            Speed = 15f;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
            Speed = 6f;
        }

        float tarAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tarAngle, ref TurnSmoothVelocity, TurnSmoothTime);
        if (!Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, -transform.eulerAngles.y, 0f);
        }
        Vector3 moveDir = Quaternion.Euler(0f, tarAngle, 0f) * Vector3.forward;

        if (dir.magnitude >= 0.1f)
        {
            animator.SetFloat("Speed",Mathf.Abs(dir.magnitude));
            if (IsRunning)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                controller.Move(moveDir.normalized * Speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, -transform.eulerAngles.y, 0f);
            }
            else
            {
                controller.Move(moveDir.normalized * Speed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetFloat("Speed", 0);
        }
    }

    
}
