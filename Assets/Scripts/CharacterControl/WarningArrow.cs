using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningArrow : MonoBehaviour
{
    Transform lookTarget;
    public void Init(Transform bullet)
    {
        lookTarget = bullet;
        StartCoroutine(LookCoroutine());
    }
    IEnumerator LookCoroutine()
    {
        while (lookTarget != null)
        {
            var dir = (lookTarget.position - transform.position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            yield return null;
        }
        Destroy(gameObject);
    }
}
