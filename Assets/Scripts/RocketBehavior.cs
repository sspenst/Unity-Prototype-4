using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    private Transform target;
    private float speed = 10.0f;

    private float rocketStrength = 30.0f;
    private float aliveTimer = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    public void Fire(Transform homingTarget)
    {
        target = homingTarget;
        Destroy(gameObject, aliveTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidBody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.GetContact(0).normal;
                targetRigidBody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
