using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCosmin : MonoBehaviour
{

    public Texture2D cursorTexture, cursorTextureClicked;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Awake()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        DontDestroyOnLoad(this.gameObject);
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            Cursor.SetCursor(cursorTextureClicked, hotSpot, cursorMode);
        else
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    } 
}
