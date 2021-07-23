using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    bool isGround;
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
        Animations();
        DetectInput();

    }
    public void RayForGround()
    {
        isGround = Physics.Raycast(tranformBody.position, Vector3.down, 3, 1 << 8);
        Debug.DrawRay(tranformBody.position, Vector3.down*3,Color.red);
    }

    public void Animations() {

        anim.SetFloat("SpeedY", rb.velocity.y);
        anim.SetFloat("SpeedXyZ", Mathf.Abs(inputVertical) + Mathf.Abs(inputHorizontal));
        anim.SetBool("IsGround", isGround);
       
    }

    public void DetectInput() {

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }


    private void FixedUpdate()
    {

        ChangeRotation();
        
        
    }

    private void ChangeRotation()
    {
        Vector3 direction = new Vector3(inputHorizontal, 0, inputVertical).normalized;

        if (direction.magnitude >= 0.1f) {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0);
        
        }
    }

    public void PlayerMovement() {

        rb.AddForce( new Vector3(inputHorizontal * 4,3,inputVertical*4),ForceMode.Impulse );
        

    }

    

    
   
}
