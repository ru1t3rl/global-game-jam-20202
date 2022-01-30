using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Spells
{
    public class AOESpell : BaseSpell
    {
        [SerializeField]
        private float impactDelay;
        Vector3 targetPoint;

        protected override void BeginSpell(Transform origin)
        {
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Walls")))
            {
                targetPoint = hit.point;
                transform.position = targetPoint;
            }
            StartCoroutine(WaitForImpact());
        }

        private IEnumerator WaitForImpact()
        {
            while (currentLifetime < impactDelay)
            {
                yield return null;
            }
            DoImpact();
        }

        private void DoImpact()
        {
            onImpact?.Invoke();
            Collider[] hits = Physics.OverlapSphere(targetPoint, stats.Range);
            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out Enemy enemy))
                    enemy.ApplyDamage(new DamageData(stats.Damage, stats.Knockback, gameObject, targetPoint));
            }
        }
    }
}
