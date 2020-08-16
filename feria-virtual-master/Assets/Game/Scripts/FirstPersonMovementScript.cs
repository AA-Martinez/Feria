using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public float Speed = 6f;
    public Transform cam;
    public Animator animator;
    public bool IsRunning;
    public float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
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
            controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        }else
        {
            animator.SetBool("IsRunning", false);
            animator.SetFloat("Speed", 0);
        }

    }
}
