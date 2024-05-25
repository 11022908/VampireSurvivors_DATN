using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    Transform player;
    public bool roaming = true;
    public AIDestinationSetter aIDestinationSetter;
    public Seeker seeker;
    public AIPath aIPath;
    public float moveSpeed;
    public SpriteRenderer enemySprite;
    public float endReached;

    //private bool reachDestination = false;
    //private bool updateContinuesPath;
    //public float nextWPDistance;
    //public float distance;
    //Path path;

    //Coroutine moveCoroutine;

    //shoot
    public bool isShootable = false;
    public GameObject bullet;
    public float bulletSpeed;
    public float timeBtwFire;
    private float fireCoolDown;
    //attack
    public Animator animator;


    private void Start()
    {
        //InvokeRepeating("CalculatePath", 0f, 0.5f);
        //reachDestination = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(transform.gameObject.name);
        animator = transform.Find("Sprite").GetComponent<Animator>();
        if (animator == null) Debug.Log("animation null");
        aIDestinationSetter.target = player;
        aIPath = GetComponent<AIPath>();
        aIPath.maxSpeed = moveSpeed;
        aIPath.endReachedDistance = endReached;
    }
    private void Update()
    {
        if(player.transform.position.x < transform.position.x)
        {
            enemySprite.transform.localScale = new Vector3(-1, 1, 0);
        }
        else
        {
            enemySprite.transform.localScale = new Vector3(1, 1, 0);
        }
        fireCoolDown -= Time.deltaTime;
        if (Vector2.Distance(transform.position, player.position) - aIPath.endReachedDistance <= 0)
        {
            if (roaming)
            {
                if (fireCoolDown < 0)
                {
                    fireCoolDown = timeBtwFire;
                    EnemyFireBullet();
                }
            }
            else
            {
                EnemyAttack();
            }
        }
        else
        {
            if (!roaming)
            {
                animator.SetTrigger("m_walk");
            }
        }

    }
    void EnemyFireBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        Vector3 playerPos = FindObjectOfType<PlayerCtrl>().gameObject.transform.position;
        Vector3 direction = playerPos - transform.position;

        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
    }
    void EnemyAttack()
    {

        animator.SetTrigger("m_attack");

    }

    //void CalculatePath()
    //{
    //    Vector2 target = FindTaregt();
    //    if (seeker.IsDone() && (reachDestination || updateContinuesPath))
    //    {
    //        seeker.StartPath(transform.position, target, OnPathCallback);
    //    }
    //}
    //Vector2 FindTaregt()
    //{
    //    Vector3 playerPos = FindObjectOfType<PlayerCtrl>().transform.position;
    //    if (roaming == true)
    //    {
    //        return (Vector2)playerPos + 
    //            (Random.Range(10f, 30f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
    //    }
    //    else return playerPos;
    //}
    //void OnPathCallback(Path p)
    //{
    //    if (p.error) return;
    //    path = p;

    //    //Move to target
    //    MoveToTarget();
    //}

    //void MoveToTarget()
    //{
    //    if (moveCoroutine != null) StopCoroutine(moveCoroutine);
    //    if(Vector2.Distance(transform.position, FindTaregt()) > distance)
    //        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    //}

    //IEnumerator MoveToTargetCoroutine()
    //{
    //    int currentWP = 0;
    //    reachDestination = false;
    //    while(currentWP < path.vectorPath.Count)
    //    {
    //        Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;

    //        Vector3 force = direction * moveSpeed * Time.deltaTime;
    //        transform.position += force;

            

    //        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
    //        if(distance < nextWPDistance)
    //        {
    //            currentWP++;
    //        }
    //        if(force.x != 0)
    //        {
    //            if(force.x < 0)
    //            {
    //                enemySprite.transform.localScale = new Vector3(-1, 1, 0);
    //            }
    //            else
    //            {
    //                enemySprite.transform.localScale = new Vector3(1, 1, 0);
    //            }
    //        }

    //        //yield return new WaitForSeconds(2f);//update sau moi 2s
    //        yield return null; //update sau moi 1s nhu ham update
    //    }
    //    reachDestination = true;
    //}

    //{
    //float dis = Vector2.Distance(transform.position, FindTaregt());
        //fireCoolDown -= Time.deltaTime;

        //if(dis <= distance)
        //{
        //    if (roaming)
        //    {
        //        if(fireCoolDown < 0)
        //        {
        //            fireCoolDown = timeBtwFire;
        //            EnemyFireBullet();
        //        }
        //    }
        //    else
        //    {
        //        EnemyAttack();
        //    }
        //}
        //else
        //{
        //    if (!roaming)
        //    {
        //        animator.SetTrigger("m_walk");
        //        Debug.Log("minataur walk");
        //    }
        //}
    //}

}
