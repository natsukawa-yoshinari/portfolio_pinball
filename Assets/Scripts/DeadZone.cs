using System;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Ballが一番下まで行ったときに削除する

    public Action onDead;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Destroy(other.gameObject);

            onDead.Invoke();
        }
    }
}
