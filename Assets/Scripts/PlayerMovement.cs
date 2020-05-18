using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    private ThirdPersonCharacter character;
    private CameraRaycaster cameraRaycaster;
    private Vector3 currentClickTarget;


    private void Start()
    {
        cameraRaycaster = Camera.main.transform.parent.GetComponent<CameraRaycaster>();
        character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    private void FixedUpdate()
    {
        ChooseBehaviour();
        Move();
    }

    private void ChooseBehaviour()
    {
        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.LayerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.Hit.point;
                    break;
                case Layer.Enemy:
                    Debug.Log("Attack");
                    break;
                default:
                    Debug.Log("Unknown target");
                    break;
            }
        }
    }

    private void Move()
    {
        Vector3 distanceToTarget = currentClickTarget - transform.position;

        if (distanceToTarget.magnitude > 0.2f)
        {
            character.Move(distanceToTarget, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }
}

