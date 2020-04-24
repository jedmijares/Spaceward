using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/Cursor.SetCursor.html
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
