using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public static GameManager instance;
    public int Score;

    public float OffscreenOffset = 10;
    public float ZPosition = 40;
    public float SpawnTime = 5.0f;
    public float InitialTime;

    public ObjectSpawner enemySpawner;
    List<SpawnableObject> enemies;

    private Camera cam;

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", InitialTime, SpawnTime);
        OnMouseEnter();
        instance = this;
        cam = Camera.main;
    }

    private void Awake()
    {
        enemies = new List<SpawnableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        Score += score;

        // update the score text
        GameUI.instance.UpdateScoreText(Score);

        //// have we reached the score to win?
        //if (curScore >= scoreToWin)
        //    WinGame();
    }

    public void LoseGame()
    {
        GameUI.instance.SetEndGameScreen(Score);
    }

    // spawn entity of there are fewer than the max allowed by spawnPool
    void Spawn()
    {
        //if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxCount)
        //{
        //    GameObject spawned = spawnPool.GetObject();
        //    if (spawned) spawned.transform.position = getPosOffscreen(OffscreenOffset, ZPosition);
        //}

        SpawnableObject instance = enemySpawner.GetRandom();
        Transform t = instance.transform;
        if (instance) instance.transform.position = getPosOffscreen(OffscreenOffset, ZPosition);
        enemies.Add(instance);
    }

    // at the given z value, generate a Vector3 representing a random position a distance offset offscreen
    Vector3 getPosOffscreen(float offset, float z)
    {
        Vector3 position = Vector3.zero;
        position.z = z;
        // Vector3 destination = cam.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), z));
        if ((Random.value > 0.5f))
        {
            position.y = Random.Range(0, cam.pixelHeight);
            if ((Random.value > 0.5f))
            {
                position.x = cam.pixelWidth + offset;
            }
            else
            {
                position.x = 0 - offset;
            }
        }
        else
        {
            position.x = Random.Range(0, cam.pixelWidth);
            if ((Random.value > 0.5f))
            {
                position.y = cam.pixelHeight + offset;
            }
            else
            {
                position.y = 0 - offset;
            }
        }
        return cam.ScreenToWorldPoint(position);
    }
}
