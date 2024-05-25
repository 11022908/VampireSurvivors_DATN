using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeBullet
{
    normal, shotgun
}
public class BulletType : MonoBehaviour
{
    public TypeBullet type;
    public float pushForce;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Boss boss = collision.gameObject.GetComponent<Boss>();
        EnemyCtrl enemy = collision.gameObject.GetComponent<EnemyCtrl>();

        if (boss == null && enemy != null && type == TypeBullet.shotgun)
        {
            Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (enemyRigidbody != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
