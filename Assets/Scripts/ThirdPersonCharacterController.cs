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
            Vector3 moveDirection = Quaternion.Euler(0f, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y, 0f) * Vector3.forward;
            rigidBody.velocity += moveDirection.normalized * speed * Time.deltaTime;
        }
        RotatePlayer();
    }

    private void OnDrawGizmos()
    {
    }

    private void RotatePlayer()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Walls")))
        {
            Vector3 direction = hit.point - transform.position;
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
