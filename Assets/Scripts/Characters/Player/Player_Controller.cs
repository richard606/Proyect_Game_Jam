using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    bool isGround;
    bool isJump,canJump;
    public Transform tranformBody;
    float inputHorizontal, inputVertical;


    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RayForGround();
        DetectInput();
        Animations();
        

    }
    public void RayForGround()
    {
        isGround = Physics.Raycast(tranformBody.position, Vector3.down, 0.7f, 1 << 8);
        Debug.DrawRay(tranformBody.position, Vector3.down*0.7f,Color.red);
    }

    public void DetectInput()
    {

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && isGround)
            canJump = true;

    }

    public void Animations() {
        
        anim.SetFloat("SpeedY", rb.velocity.y);        
        anim.SetFloat("SpeedXyZ", Mathf.Abs(inputVertical = inputHorizontal));               
        anim.SetBool("IsGround", isGround);
        
    }

    


    private void FixedUpdate()
    {

        ChangeRotation();
        JumpMovement();
        
    }

    private void ChangeRotation()
    {
        Vector3 direction = new Vector3(inputHorizontal, 0, inputVertical).normalized;

        if (direction.magnitude >= 0.1f) {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0);
        
        }
    }

    public void JumpMovement() {
        if (canJump) {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector3(inputHorizontal * 4, 10, inputVertical * 4), ForceMode.Impulse);
            canJump = false;
        }
    }

    public void PlayerMovement() {
        if (!canJump) {

            rb.AddForce(new Vector3(inputHorizontal * 5, 5, inputVertical * 5), ForceMode.Impulse);

        }

    }

    

    
   
}
