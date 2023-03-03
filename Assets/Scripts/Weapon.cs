using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FpsCanera;
    [SerializeField] ParticleSystem smokeEffects;
    [SerializeField] ParticleSystem sparkEffects;

    [SerializeField] GameObject sparkLight;
    [SerializeField] AudioClip gunClip;
    AudioSource gunSounds;


    [SerializeField] float hitRange;
    [SerializeField] int damageAmount = 50;
    bool isHit;
    RaycastHit hit;
    EnemyHealth enemyHealth;

    void Start()
    {
        gunSounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            sparkEffects.Play();
            sparkLight.SetActive(true);
            
            smokeEffects.Play();
            gunSounds.PlayOneShot(gunClip);
            isHit = Physics.Raycast(FpsCanera.transform.position, FpsCanera.transform.forward, out hit, hitRange);
            BulletHitEffect(hit);
            if (isHit != false)
            {

                Debug.Log($"Hit the target {hit.transform.name} and distance is {hit.distance}");
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damageAmount);
            }
            StartCoroutine(SparkLightOff());

        }
      
    }

   IEnumerator SparkLightOff()
    {
        yield return new WaitForSeconds(0.3f);
        sparkLight.SetActive(false);
    }

    private void BulletHitEffect(RaycastHit hit)
    {
        GameObject instance = Object_Pooling.instance.GetPooledObjects();
        instance.transform.position = hit.point;
        instance.transform.rotation = Quaternion.LookRotation(hit.normal);
        if (isHit)
        {
            instance.SetActive(true);
            StartCoroutine(ReturnBullets(instance));
        }


    }

    IEnumerator ReturnBullets(GameObject instance)
    {
        yield return new WaitForSeconds(1);
        Object_Pooling.instance.ReturnBullet(instance);
        
    }
}