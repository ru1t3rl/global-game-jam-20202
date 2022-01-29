using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    //CharacterController characterController;
    Rigidbody rigidBody;

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f, targetAngle, angle;
    private float turnSmoothVelocity;
    private Camera cam;

    private void Awake()
    {
        //characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDiretion = Quaternion.Euler(0f, RotatePlayer(direction), 0f) * Vector3.forward;
            rigidBody.MovePosition(moveDiretion.normalized * speed * Time.deltaTime + transform.position);
        }        
    }

    private float RotatePlayer(Vector3 direction)
    {
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        return targetAngle;
    }
}
