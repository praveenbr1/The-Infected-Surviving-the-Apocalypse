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

    [SerializeField] float hitRange;
    [SerializeField] int damageAmount = 50;

    RaycastHit hit;
    EnemyHealth enemyHealth;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            sparkEffects.Play();
            sparkLight.SetActive(true);
            smokeEffects.Play();
            bool isHit = Physics.Raycast(FpsCanera.transform.position,FpsCanera.transform.forward,out hit,hitRange);
            if(isHit != false) 
            {
                
                Debug.Log($"Hit the target {hit.transform.name} and distance is {hit.distance}");
                EnemyHealth enemyHealth = hit.transform.GetComponent<EnemyHealth>();
                if(enemyHealth != null)
                enemyHealth.TakeDamage(damageAmount);
            }
            
        }
        else if(Input.GetKeyUp (KeyCode.Mouse0)) 
            { sparkLight.SetActive(false);}
    }

}
