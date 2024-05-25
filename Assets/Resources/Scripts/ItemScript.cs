using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Healing, Coin
}

public class ItemScript : MonoBehaviour
{
    public ItemType itemType;
    PlayerCtrl player;
    private int min = 5;
    private int max = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerCtrl>();
            if (itemType == ItemType.Healing)
                HealingPlayer();
            else
                InLevelManager.Instance.UpdateCoinInGame(1);
            Destroy(this.gameObject);

        }

    }
    void HealingPlayer()
    {
        int damage = Random.Range(min, max);
        player.Health(damage);
    }
    
}
