using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f;
    Camera viewCamera;

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

    private void Awake()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);

            if (hit.HasValue)
            {
                _hit = hit.Value;
                _layerHit = layer;           
                return;
            }
        }
        _hit.distance = distanceToBackground;
        _layerHit = Layer.RaycastEndStop;

    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit; // used as an out parameter
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }
}
