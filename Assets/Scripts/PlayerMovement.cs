using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter character;
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentClickTarget;
    private GameObject clickIndicator;
    [SerializeField] private float minDistanceToTarget = 2f;

    private void Start()
    {
        cameraRaycaster = Camera.main.transform.parent.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
        clickIndicator = GameObject.FindGameObjectWithTag("ClickIndicator");
    }

    private void FixedUpdate()
    {
        ChooseClickBehaviour();
        Move();
    }

    private void ChooseClickBehaviour()
    {
        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.LayerHit) // todo remove log functions
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.Hit.point;
                    break;
                case Layer.Enemy:
                    Debug.Log("Attack"); 
                    break;
                case Layer.RaycastEndStop:
                    Debug.Log("Unknown target");
                    break;
            }
            StartCoroutine(HandleClickIndicator());           
        }
    }

    private void Move()
    {
        Vector3 distanceToTarget = currentClickTarget - transform.position;

        if (distanceToTarget.magnitude > minDistanceToTarget)
        {
            character.Move(distanceToTarget, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    private IEnumerator HandleClickIndicator()
    {
        GameObject indicatorIcon = clickIndicator.transform.Find("ClickIndicatorIcon").gameObject;
        clickIndicator.transform.position = cameraRaycaster.Hit.point;
        yield return new WaitForSeconds(0.1f);
        indicatorIcon.SetActive(true);
        yield return new WaitForSeconds(1f);
        indicatorIcon.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.black;
        Handles.DrawLine(transform.position, currentClickTarget);
        Handles.color = new Color(0, 200, 200, 0.05f);
        Handles.DrawSolidDisc(transform.position, Vector3.up, minDistanceToTarget);
    }

    
}

