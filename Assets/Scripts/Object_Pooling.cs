using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Object_Pooling : MonoBehaviour
{
    public static Object_Pooling instance;
    private List<GameObject> poolBullets = new List<GameObject>();
    int poolBulletCount = 10;
    [SerializeField] private GameObject bulletPrfeab;
    GameObject bulletParent;
    const string Bullet_Parent_Name = "Bullet Parent";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        CreateBulletParent();
        for(int i = 0; i < poolBulletCount; i++)
        {
            GameObject newBulllet = Instantiate(bulletPrfeab);
            newBulllet.transform.parent = bulletParent.transform;
            newBulllet.SetActive(false);
            poolBullets.Add(newBulllet);
        }
    }

    private void CreateBulletParent()
    {
        bulletParent = GameObject.Find(Bullet_Parent_Name);
        if(!bulletParent) 
        { 
           bulletParent = new GameObject(Bullet_Parent_Name);
        }
    }

    public GameObject GetPooledObjects() 
    {
        for(int i = 0; i < poolBullets.Count; i++) 
        {
            if (!poolBullets[i].activeInHierarchy)
            {
                return poolBullets[i];
            }
        }

        return null;
    }

    public void ReturnBullet(GameObject instance) 
    {
        instance.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
