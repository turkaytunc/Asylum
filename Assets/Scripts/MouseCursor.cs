using UnityEngine;

[RequireComponent(typeof (CameraRaycaster))]
public class MouseCursor : MonoBehaviour
{
    private CameraRaycaster cameraRaycaster;
    [Header("Cursor Icons")]
    [SerializeField] private Texture2D walkCursorTexture;
    [SerializeField] private Texture2D attackCursorTexture;
    [SerializeField] private Texture2D unknownCursorTexture;

    private void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.layerChangeObservers += SubscribeToLayerChangeObservers;
    }


    private void SubscribeToLayerChangeObservers()
    {
        ChangeCursorTextureBasedOnLayer();
    }



    private void ChangeCursorTextureBasedOnLayer()
    {
        Debug.Log("Layer Changed"); // todo remove log function
        switch (cameraRaycaster.LayerHit)
        {
            case Layer.Walkable:
                SetCursorTexture(walkCursorTexture);
                break;
            case Layer.Enemy:
                SetCursorTexture(attackCursorTexture);
                break;
            case Layer.RaycastEndStop:
                SetCursorTexture(unknownCursorTexture);
                break;
        }
    }

    private void SetCursorTexture(Texture2D texture)
    {
        Cursor.SetCursor(texture, new Vector2(0, 1), CursorMode.Auto);
    }

}
