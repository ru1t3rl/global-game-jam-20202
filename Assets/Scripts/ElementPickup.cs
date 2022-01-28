using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Storage
{
    [RequireComponent(typeof(BaseInventoryItem))]
    public class ElementPickup : MonoBehaviour
    {
        private BaseInventoryItem inventoryElement;
        private Rigidbody rigidBody;
        private const float FOLLOW_SPEED = 1;

        private void Awake()
        {
            inventoryElement = GetComponent<BaseInventoryItem>();
            rigidBody = GetComponent<Rigidbody>();
        }

        private void OnTriggerStay(Collider other)
        {
            Vector3 directionVector = other.transform.position - transform.position;
            rigidBody.velocity += new Vector3(directionVector.x, 0, directionVector.z) * FOLLOW_SPEED;
        }

        private void OnCollisionEnter(Collision collision)
        {
            SimpleInventory.Instance.AddItem(inventoryElement);
            gameObject.SetActive(false);
        }
    }
}
