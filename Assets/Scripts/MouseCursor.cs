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

    private void LateUpdate()
    {
        ChangeCursorTextureBasedOnLayer();
    }

    private void ChangeCursorTextureBasedOnLayer()
    {
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
