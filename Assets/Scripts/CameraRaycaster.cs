using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{

    public OnLayerChangeObserver layerChangeObservers;

    [SerializeField] private Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable,
        Layer.RaycastEndStop
    };
    [SerializeField] float distanceToBackground = 100f;

    private RaycastHit _hit;
    private Layer _layerHit;
    private Camera viewCamera;



    public RaycastHit Hit
    {
        get { return _hit; }
    }

    public Layer LayerHit
    {
        get { return _layerHit; }
    }

    private void Awake()
    {
        viewCamera = Camera.main;
    }


    void Update()
    {
        RaycastForLayer();
    }

    private void RaycastForLayer()
    {
        foreach (Layer layer in layerPriorities)
        {
            var hitInfo = RaycastOnCursor(layer);

            if (hitInfo.HasValue)
            {
                _hit = hitInfo.Value;
                if (_layerHit != layer)
                {
                    _layerHit = layer;
                    layerChangeObservers();
                }
                return;
            }
        }
        _hit.distance = distanceToBackground;
        _layerHit = Layer.RaycastEndStop;
    }

    private RaycastHit? RaycastOnCursor(Layer layer)
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
