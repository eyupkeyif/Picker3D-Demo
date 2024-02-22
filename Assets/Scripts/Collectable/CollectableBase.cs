using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void PushCollectable()
    {
        rb.AddForce(Vector3.forward*8,ForceMode.Impulse);
    }
}
