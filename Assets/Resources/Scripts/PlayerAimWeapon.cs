using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerAimWeapon : MonoBehaviour
{
    
    private Transform aimTransform;


    public List<GameObject> listBulletPrefab;
    private GameObject currentBullet;
    private Transform firePos;
    public List<float> timeBtwFire;
    public float bulletForce;

    public GameObject muzzle;

    private float _timeBtwFire;

    private List<Transform> weapons = new List<Transform>();
    private Transform currentWeapon;
    private int currentIndex = 0;

    public LineLazer lineLazer;

    public List<ParticleSystem> bulletShell;
    private ParticleSystem currentParticle;

    public GameObject bulletShellPrefab;


    

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        foreach(Transform item in aimTransform)
        {
            weapons.Add(item);
        }
        for(int i =0; i < weapons.Count; i++)
        {
            if (i == currentIndex)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }
        firePos = weapons[currentIndex].transform.Find("FirePos").transform;
        currentParticle = bulletShell[currentIndex];
        currentBullet = listBulletPrefab[currentIndex];
        _timeBtwFire = timeBtwFire[currentIndex];
    }
    
    private void Update()
    {
        HandleAiming();
        HandleConvertWeapon();
        _timeBtwFire -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && _timeBtwFire < 0)
        {
            HandleShooting();
        }
        
    }
    private void HandleConvertWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }
    }
    private void SwitchWeapon(int newIndex)
    {
        if (newIndex < 0 || newIndex > weapons.Count)
            return;
        weapons[currentIndex].gameObject.SetActive(false);
        weapons[newIndex].gameObject.SetActive(true);
        firePos = weapons[newIndex].transform.Find("FirePos").transform;
        currentParticle = bulletShell[newIndex];
        currentBullet = listBulletPrefab[newIndex];
        _timeBtwFire = timeBtwFire[newIndex];
        currentIndex = newIndex;
    }
    private void HandleAiming()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Vector3 aimLocalScale = Vector3.one;
        

        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = 1f;
        }
        aimTransform.localScale = aimLocalScale;
    }
    private void HandleShooting()
    {
        _timeBtwFire = timeBtwFire[currentIndex];
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(firePos.position, mousePos - firePos.position);
        LineLazer line =  Instantiate(lineLazer);
        line.gameObject.SetActive(true);
        line.DrawLine(firePos.position, mousePos);
        line.lineRenderer.enabled = true;
        StartCoroutine(DestroyLineLazer(1f, line.gameObject));

        PlayShellCasingEffect();
        StartCoroutine(InitializeBulletSheel(1.2f));
        

        GameObject bullet = Instantiate(currentBullet, firePos.position, Quaternion.identity);
        //effect
        Instantiate(muzzle, firePos.position, transform.rotation, transform);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 dir = (mousePos - bullet.transform.position).normalized;
        rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
        AudioManager.Instance.PlaySFX("pistol_fire");
    }
    IEnumerator InitializeBulletSheel(float timer)
    {
        yield return new WaitForSeconds(timer);
        Vector3 shellPos = new Vector3(UnityEngine.Random.Range(0, 2), UnityEngine.Random.Range(-3f, -4f), 0);

        Instantiate(bulletShellPrefab, transform.position + shellPos, Quaternion.identity);
        AudioManager.Instance.PlaySFX("bullet_floor");
    }
    IEnumerator DestroyLineLazer(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
    void PlayShellCasingEffect()
    {
        // Get the particle system emission module
        ParticleSystem.EmissionModule emission = currentParticle.emission;

        // Set the burst properties
        ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, 1); // Burst 1 particle at 0 seconds
        emission.SetBurst(0, burst);

        // Play the particle system
        currentParticle.Play();
    }
}
