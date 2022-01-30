using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContactDamage : MonoBehaviour
{
    [SerializeField]
    float damage, knockback;

    public UnityEvent onContact;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.ApplyDamage(new DamageData(damage, knockback, gameObject, collision.GetContact(0).point));
            onContact?.Invoke();
        }
    }
}
