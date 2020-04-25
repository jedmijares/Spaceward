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
        OnMouseEnter();
        instance = this;
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
}
