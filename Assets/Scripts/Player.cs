using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;

    [Header("Movement")]
    public float moveSpeed;             // movement speed in units per second
    public float firingSpeedPenalty = 0.5F;
    //public float jumpForce;             // force applied upwards

    //[Header("Camera")]
    //public float lookSensitivity;       // mouse look sensitivity
    //public float maxLookX;              // lowest down we can look
    //public float minLookX;              // highest up we can look
    //private float rotX;                 // current x rotation of the camera

    private Camera cam;
    private Rigidbody rig;
    private Weapon weapon;
    public GameObject shipModel;

    void Awake ()
    {
        // get the components
        cam = Camera.main;
        rig = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();
        //shipModel = GetComponent<shipModel>();

        //// disable cursor
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        //// initialize the UI
        //GameUI.instance.UpdateHealthBar(curHp, maxHp);
        //GameUI.instance.UpdateScoreText(0);
        //GameUI.instance.UpdateAmmoText(weapon.curAmmo, weapon.maxAmmo);
    }

    void Update ()
    {
        //// dont do anything if game is paused
        //if (GameManager.instance.gamePaused == true)
        //    return;


        if (Input.GetButton("Fire1"))
        {
            if (weapon.CanShoot()) // && (rig.velocity.magnitude <= (moveSpeed*firingSpeedPenalty*Mathf.Sqrt(2))+1))
            {
                weapon.Shoot();
            }
            Move(moveSpeed * firingSpeedPenalty);
        }
        else Move(moveSpeed);

        //CamLook();
    }

    // move horizontally based on movement inputs
    void Move (float moveSpeed)
    {
        // get the x and z inputs
        float x = Input.GetAxis("Horizontal");// * moveSpeed;
        float y = Input.GetAxis("Vertical");// * moveSpeed;

        Vector3 dir = (transform.right * x + transform.up * y) * moveSpeed;
        dir.z = 0;
        // apply the velocity
        rig.velocity = dir;

        shipModel.transform.rotation = Quaternion.Euler(rig.velocity.y * -2.5f, 0.0f, rig.velocity.x * -2.5f);
        // rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }

    // called when we get hit by a bullet
    public void TakeDamage(int damage)
    {
        curHp -= damage;

        //GameUI.instance.UpdateHealthBar(curHp, maxHp);

        if (curHp <= 0)
        {
            //Die();
        }

    }

    //    // called when our health reaches 0
    //    void Die()
    //    {
    //        GameManager.instance.LoseGame();
    //    }

    //    // called when the player is given health
    //    public void GiveHealth(int amountToGive)
    //    {
    //        curHp = Mathf.Clamp(curHp + amountToGive, 0, maxHp);

    //        GameUI.instance.UpdateHealthBar(curHp, maxHp);
    //    }

}