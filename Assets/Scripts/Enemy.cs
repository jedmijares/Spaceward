using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnableObject
{
    [Header("Stats")]
    public int curHp;
    public int maxHp = 50;
    public int scoreToGive = 50;

    [Header("Movement")]
    public float moveSpeed = 15;
    public int screenEdgeOffset = 75;
    public int distanceFromCamera = 40;

    protected Vector3 destination; // next position to move towards
    protected Camera cam;

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