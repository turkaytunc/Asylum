using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;
    [SerializeField] private string followThisTag = "Player";

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(followThisTag).transform;
    }


    private void LateUpdate()
    {
        transform.position = playerTransform.position;
    }
}
