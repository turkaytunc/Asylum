using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;
    private void Awake()
    {
        viewCamera = Camera.main;
    }

    private RaycastHit _hit;
    private Layer _layerHit;
    public RaycastHit Hit
    {
        get { return _hit; }
    }

    public Layer LayerHit
    {
        get { return _layerHit; }
    }


    void Update()
    {
        foreach (Layer layer in layerPriorities)
        {
            var hitInfo = RaycastForLayer(layer);

            if (hitInfo.HasValue)
            {
                _hit = hitInfo.Value;
                _layerHit = layer;           
                return;
            }
        }
        _hit.distance = distanceToBackground;
        _layerHit = Layer.RaycastEndStop;

    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        RaycastHit hitInfo; // used as an out parameter

        bool hasHit = Physics.Raycast(ray, out hitInfo, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hitInfo;
        }
        return null;
    }
}
