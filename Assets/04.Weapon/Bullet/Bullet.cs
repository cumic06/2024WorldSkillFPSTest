using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed;
    public int bulletDamage;
    protected Vector3 startPos;
    public float DestroyRange;

    private void OnEnable()
    {
        startPos = transform.position;
    }

    protected void FixedUpdate()
    {
        Move();
        DestroyBullet();
    }

    protected void Move()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
    }

    protected void DestroyBullet()
    {
        if (Vector3.Distance(startPos, transform.position) > DestroyRange * 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable unit))
        {
            unit.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}