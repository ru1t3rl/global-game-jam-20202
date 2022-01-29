using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody rigidBody;

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f, targetAngle, angle;
    private float turnSmoothVelocity;
    private Camera cam;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!characterController.enabled) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = Quaternion.Euler(0f, RotatePlayer(direction), 0f) * Vector3.forward;
            moveDirection += Physics.gravity;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    public void OnKnockback()
    {
        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine()
    {
        rigidBody.isKinematic = false;
        characterController.enabled = false;
        yield return null; //Wait a frame for knockback to apply

        while (rigidBody.velocity.magnitude > 0.1)
        {
            yield return null;
        }
        rigidBody.isKinematic = true;
        characterController.enabled = true;
    }

    private float RotatePlayer(Vector3 direction)
    {
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
        return targetAngle;
    }
}
