using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Pulpit : MonoBehaviour
{
    private float destroyTime;

    public void Initialize(float minDestroyTime, float maxDestroyTime)
    {
        // Set the destroy time randomly within the range
        destroyTime = Random.Range(minDestroyTime, maxDestroyTime);
        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        // Wait for the destroy time, then destroy this pulpit
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);

        // Notify the PulpitSpawner that this pulpit is destroyed and ready for a new one
        PulpitSpawner.Instance.OnPulpitDestroyed();
    }
}

