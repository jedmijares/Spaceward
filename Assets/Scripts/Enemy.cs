using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int curHp = 50;
    public int maxHp = 50;
    public int scoreToGive = 50;

    [Header("Movement")]
    public float moveSpeed = 15;
    //public float attackRange;
    //public float yPathOffset;

    //private List<Vector3> path;

    private Weapon weapon;
    //private GameObject target;
    private Rigidbody rig;
    private Vector3 destination;// = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 40)); // next position to move towards
    private Camera cam;

    void Start ()
    {
        // get the components
        weapon = GetComponent<Weapon>();
        //target = FindObjectOfType<Player>().gameObject;
        rig = GetComponent<Rigidbody>();
        destination = transform.position;
        cam = Camera.main;

        //InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    void Update ()
    {
        //float dist = Vector3.Distance(transform.position, target.transform.position);

        //if(dist <= attackRange)
        //{
        if (weapon.CanShoot())
            weapon.Shoot();
        //}
        //else
        //{
        //    ChaseTarget();
        //}

        Move();

        //// look at the target
        //Vector3 dir = (target.transform.position - transform.position).normalized;
        //float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        //transform.eulerAngles = Vector3.up * angle;

        //Vector3 dir = transform.right * moveSpeed;// + transform.up * y;
        //dir.z = 0;
        //dir.y = 0;

        // apply the velocity
        //rig.velocity = dir;
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (transform.position == destination)
        {
            destination = cam.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 40));
        }
    }

    //void ChaseTarget ()
    //{
    //    if(path.Count == 0)
    //        return;

    //    // move towards the closes path
    //    transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

    //    if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
    //        path.RemoveAt(0);
    //}

    //void UpdatePath ()
    //{
    //    // calculate a path to the target
    //    NavMeshPath navMeshPath = new NavMeshPath();
    //    NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

    //    // save that as a list
    //    path = navMeshPath.corners.ToList();
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

        Destroy(gameObject);
    }
}