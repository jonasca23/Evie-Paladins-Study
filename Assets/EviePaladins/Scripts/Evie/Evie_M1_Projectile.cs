using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evie_M1_Projectile : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float floatSpeed;
    Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = direction * floatSpeed;
    }

    public void SetDirection(Vector3 _dir)
    {
        direction = _dir;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
