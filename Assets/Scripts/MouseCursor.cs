using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseCursor : MonoBehaviour
{

    private CameraRaycaster cameraRaycaster;
    [SerializeField] private Texture2D walkCursorTexture;
    [SerializeField] private Texture2D attackCursorTexture;
    [SerializeField] private Texture2D unknownCursorTexture;


    private void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
    }


    private void Update()
    {
        ChangeCursorTextureBasedOnLayer();
    }

    private void ChangeCursorTextureBasedOnLayer()
    {
        switch (cameraRaycaster.LayerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursorTexture, new Vector2(0, 1), CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(attackCursorTexture, new Vector2(0, 1), CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unknownCursorTexture, new Vector2(0, 1), CursorMode.Auto);
                break;
        }
    }





}
