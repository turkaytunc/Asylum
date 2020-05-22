using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float minAttackDistanceToTarget = 3f;
    [SerializeField] private float minDistanceToTarget = 2f;

    private ThirdPersonCharacter character;
    private CameraRaycaster cameraRaycaster;
    private GameObject clickIndicator;
    private GameObject indicatorIcon;
    private bool isPlayerMoving;

    private Vector3 currentClickTarget;
    private Vector3 distanceToTarget;

    private void Start()
    {
        cameraRaycaster = Camera.main.transform.parent.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
        clickIndicator = GameObject.FindGameObjectWithTag("ClickIndicator");
        indicatorIcon = clickIndicator.transform.Find("ClickIndicatorIcon").gameObject;

    }

    private void FixedUpdate()
    {
        ChooseClickBehaviour();
        MoveToDestination();
        HandleClickIndicator();
    }

    private void ChooseClickBehaviour()
    {
        if (Input.GetMouseButton(0))
        {
            currentClickTarget = cameraRaycaster.Hit.point;
            switch (cameraRaycaster.LayerHit) // todo remove log functions
            {
                case Layer.Walkable:
                    distanceToTarget = CalculateDistanceBetweenPlayerAndTarget(currentClickTarget, minDistanceToTarget);
                    break;
                case Layer.Enemy:
                    distanceToTarget = CalculateDistanceBetweenPlayerAndTarget(currentClickTarget, minAttackDistanceToTarget);
                    Debug.Log("Attack"); 
                    break;
                case Layer.RaycastEndStop:
                    Debug.Log("Unknown target");
                    break;
            }
        }
    }
    private Vector3 CalculateDistanceBetweenPlayerAndTarget(Vector3 destination, float minDistanceToTarget)
    {
        Vector3 distanceToTargetVector = (destination - transform.position).normalized * minDistanceToTarget;

        return destination - distanceToTargetVector;
    }

    private void MoveToDestination()
    {

        Vector3 dist = distanceToTarget - transform.position;
        if (dist.magnitude >= 0)
        {
            character.Move(dist, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

        IsPlayerMoving(dist);
    }

    private void IsPlayerMoving(Vector3 dist)
    {
        if (dist.magnitude > minDistanceToTarget)
        {
            isPlayerMoving = true;

        }
        else
        {
            isPlayerMoving = false;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.black;
        Handles.DrawLine(transform.position, currentClickTarget);

        Handles.color = new Color(0, 200, 200, 0.05f);
        Handles.DrawSolidDisc(transform.position, Vector3.up, minDistanceToTarget);

        Handles.color = new Color(200, 0, 200, 0.05f);
        Handles.DrawSolidDisc(transform.position + Vector3.up * 0.2f, Vector3.up, minAttackDistanceToTarget);
    }
  
    private void HandleClickIndicator()
    {
        if (isPlayerMoving)
        {
            clickIndicator.transform.position = currentClickTarget;
            indicatorIcon.SetActive(true);
        }
        else
        {
            indicatorIcon.SetActive(false);
        }
    }
}

