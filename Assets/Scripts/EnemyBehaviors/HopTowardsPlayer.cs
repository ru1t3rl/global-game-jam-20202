using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class HopTowardsPlayer : MonoBehaviour
{
    [SerializeField]
    private float acquireRange, idleRange, hopAngle, hopForce, minCooldown, maxCooldown, rotateToPlayerSpeed;
    private float currentCooldown;
    private Vector3 idleOrigin;
    private Vector3 randomTargetInIdleRange;

    private Transform player;
    private Rigidbody rigidBody;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, acquireRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(idleOrigin, idleRange);
    }

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        rigidBody = GetComponent<Rigidbody>();
        currentCooldown = Random.Range(minCooldown, maxCooldown);
        idleOrigin = transform.position;
    }

    private void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (Vector3.Distance(player.position, transform.position) < acquireRange)
        {
            Quaternion lookRotation = Quaternion.LookRotation((new Vector3(player.position.x, 0, player.position.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateToPlayerSpeed * Time.deltaTime);
            idleOrigin = transform.position;
        }
        else
        {
            Quaternion lookRotation = Quaternion.LookRotation((new Vector3(randomTargetInIdleRange.x, 0, randomTargetInIdleRange.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotateToPlayerSpeed * Time.deltaTime);
        }
        DoHop();
    }

    private void DoHop()
    {
        if (currentCooldown <= 0)
        {
            currentCooldown += Random.Range(minCooldown, maxCooldown);
            Vector3 hopVector = Quaternion.AngleAxis(-hopAngle, transform.right) * transform.forward.normalized;
            rigidBody.velocity += hopVector * hopForce;
            randomTargetInIdleRange = idleOrigin + Quaternion.AngleAxis(Random.Range(0f, 360), Vector3.up) * Vector3.forward * Random.Range(0f, idleRange);
        }
    }
}
