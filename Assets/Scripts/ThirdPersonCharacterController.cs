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
    [SerializeField] float jumpForce = 10;
    [SerializeField] float jumpGravityMultiplier = 5;
    [SerializeField] float fallGravityMultiplier = 10;
    private float turnSmoothVelocity;
    private Camera cam;
    private CapsuleCollider capsuleCollider;

    private bool IsGrounded
    {
        get
        {
            return Physics.SphereCast(new Ray(transform.position + capsuleCollider.center, Vector3.down), capsuleCollider.radius, capsuleCollider.center.y);
        }
    }

    private void Awake()
    {
        //characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        rigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + capsuleCollider.center, capsuleCollider.radius);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position + capsuleCollider.center, Vector3.down * (transform.position.y + capsuleCollider.center.y + 0.1f)));
    }

    private void Update()
    {
        MovePlayer();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void MovePlayer()
    {
        bool jump = Input.GetButtonDown("Jump");
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 moveDirection = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            moveDirection = Quaternion.Euler(0f, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y, 0f) * Vector3.forward;
        }
        Vector3 moveVelocity = (moveDirection.normalized * speed * Time.deltaTime);
        moveVelocity += (jump && IsGrounded) ? Vector3.up * jumpForce : Vector3.zero;
        if (rigidBody.velocity.y <= 0)
            moveVelocity += Physics.gravity * (fallGravityMultiplier - 1) * Time.deltaTime;
        else
            moveVelocity += Physics.gravity * (jumpGravityMultiplier - 1) * Time.deltaTime;

        rigidBody.velocity += moveVelocity;

        RotatePlayer();
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
