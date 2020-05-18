using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    private CameraRaycaster cameraRaycaster;

    private void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
    }

}
