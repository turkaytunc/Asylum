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
        if (Input.GetMouseButton(0))
        {
            currentClickTarget = cameraRaycaster.Hit.point;
        }

        character.Move(currentClickTarget - transform.position, false, false);
    }
}

