using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SpawnableObject
{
    [SerializeField]
    private ObjectSpawner pickupSpawner;

    [Header("Stats")]
    public int curHp;
    public int maxHp = 50;
    public int scoreToGive = 50;

    [Header("Movement")]
    public float moveSpeed = 15;
    [SerializeField]
    protected int screenEdgeOffset = 75;
    [SerializeField]
    protected int distanceFromCamera = 40;

    protected Vector3 destination; // next position to move towards
    protected Camera cam;

    protected virtual void Start()
    {
        cam = Camera.main;
        destination = GetPositionInWindow();
        curHp = maxHp;
    }

    protected virtual void OnEnable()
    {
        this.transform.position = GameManager.instance.getPosOffscreen(GameManager.instance.OffscreenOffset, GameManager.instance.ZPosition);
        curHp = maxHp;
    }

    public void TakeDamage (int damage)
    {
        curHp -= damage;
        if(curHp <= 0) Die();
    }

    void Die ()
    {
        GameManager.instance.AddScore(scoreToGive);
        pickupSpawner.GetRandom().transform.position = transform.position;
        creator.Reclaim(this);
    }

    protected Vector3 GetPositionInWindow()
    {
        if (!cam) cam = Camera.main;
        return cam.ScreenToWorldPoint(new Vector3(Random.Range(screenEdgeOffset, Screen.width - screenEdgeOffset), Random.Range(screenEdgeOffset, Screen.height - screenEdgeOffset), distanceFromCamera));
    }
}