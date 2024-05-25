using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;

    public bool playerBullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerBullet)
        {
            int damage = Random.Range(minDamage, maxDamage);
            collision.gameObject.GetComponent<PlayerCtrl>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && playerBullet)
        {
            int damage = Random.Range(minDamage, maxDamage);
            collision.gameObject.GetComponent<EnemyCtrl>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
    

}
