using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minataur_hitbox : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D polygonCollider;
    void Update()
    {
        Vector2[] spriteShape = spriteRenderer.sprite.vertices;

        polygonCollider.SetPath(0, spriteShape);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCtrl player = collision.gameObject.GetComponent<PlayerCtrl>();
        if(player){
            polygonCollider.isTrigger = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerCtrl player = collision.gameObject.GetComponent<PlayerCtrl>();
        if (player)
        {
            polygonCollider.isTrigger = false;
        }
    }
}
