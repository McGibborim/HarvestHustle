using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    float rotation;
    public float moveSpeed;
    public Rigidbody rb;
    private Vector3 moveVelocity;
  
    void Update()
    {
        moveVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        rb.AddForce(moveVelocity * moveSpeed);



        if(moveVelocity.magnitude >= 0.1f)
        {
            float Angle = Mathf.Atan2(moveVelocity.x, moveVelocity.z) * Mathf.Rad2Deg;
            float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref rotation, 0.1f);
            transform.rotation = Quaternion.Euler(0, Smooth, 0);
        }
    }
}

