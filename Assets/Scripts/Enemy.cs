using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnableObject
{
    [Header("Stats")]
    public int curHp;
    public int maxHp = 50;
    public int scoreToGive = 50;
    //public int minShotVolley = 3;
    //public int maxShotVolley = 5;

    [Header("Movement")]
    public float moveSpeed = 15;
    public int screenEdgeOffset = 75;
    public int distanceFromCamera = 40;

    //private Weapon weapon;
    protected Vector3 destination; // next position to move towards
    protected Camera cam;
    //private bool shooting;
    //private int shotVolley;


    //void Start ()
    //{
    //    // get the components
        
        
        
    //}

    //private void OnEnable()
    //{
        
    //    //shooting = false;
    //    //shotVolley = Random.Range(minShotVolley, maxShotVolley + 1);
    //}

    //void Update ()
    //{
        
    //}

    public void TakeDamage (int damage)
    {
        curHp -= damage;

        if(curHp <= 0)
            Die();
    }

    void Die ()
    {
        GameManager.instance.AddScore(scoreToGive);
        creator.Reclaim(this);
    }
}