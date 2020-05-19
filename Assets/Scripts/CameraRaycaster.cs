using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
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

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    private void Awake()
    {
        viewCamera = Camera.main;
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
            else
            {
                _hit.distance = distanceToBackground;
                _layerHit = Layer.RaycastEndStop;
            }
        }    
    }

    private RaycastHit? RaycastForLayer(Layer layer)
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
