using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    PlayerCtrl player;
    public int minDamage;
    public int maxDamage;
    
    public GameObject[] itemObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerCtrl>();
            InvokeRepeating("DamagePlayer", 0, 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
            CancelInvoke();
        }
    }

    void DamagePlayer()
    {
        int damage = Random.Range(minDamage, maxDamage);
        player.TakeDamage(damage);
    }

    public Health health;
    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        InLevelManager.Instance.CreatePopup(transform.position, damage);
    }

    public void UpdateQuantityDie()
    {
        InLevelManager.Instance.enemyDeathCount += 1;
        InLevelManager.Instance.EnemyDestroyed();
    }
    public void OnRandomHealing()
    {
        if(Random.Range(0, 3) == 1)
        {
            int index = Random.Range(0, itemObject.Length);
            Instantiate(itemObject[index], transform.position, Quaternion.identity);
        }
    }
}
