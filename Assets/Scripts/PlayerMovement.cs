using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacter;
    CameraRayCast cameraRay;
    Vector3 currentClickTarget;
    Vector3 currentDestination;

    public float moveStopRadius = 0.2f;
    public float attackMoveStopRadius = 2f;
    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        //cameraRay = GetComponent<CameraRayCast>();
        cameraRay = Camera.main.GetComponent<CameraRayCast>();
        currentClickTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            print(cameraRay.m_hit.collider.gameObject.name.ToString());
            print(cameraRay.m_LayerHit);
            currentClickTarget = cameraRay.m_hit.point;
            switch (cameraRay.m_LayerHit)
            {
                case Layer.Enemy:
                    print("enemy area");
                    currentDestination = ShrotDestination(cameraRay.m_hit.point, attackMoveStopRadius);
                    break;
                case Layer.Walkablearea:
                    //currentDestination = currentClickTarget;
                    currentDestination = ShrotDestination(cameraRay.m_hit.point, moveStopRadius);
                    break;
                case Layer.raycastendstop:
                    break;
                default:
                    print("No layer state");
                    break;
            }
        }
        WalkToDestiantion();
    }

    void WalkToDestiantion()
    {
        var tempClickPoint = currentClickTarget - transform.position;
        if (tempClickPoint.magnitude >= moveStopRadius)
        {
            thirdPersonCharacter.Move(tempClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, currentClickTarget);
        Gizmos.DrawSphere(currentClickTarget, 0.2f);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        //Gizmos.color = Color.green;
        //Gizmos.DrawSphere(transform.position, 1f);
    }
    Vector3 ShrotDestination(Vector3 targetDestination,float rad)
    {
        Vector3 reductionInDistance = (currentDestination - transform.position).normalized * rad;
        return targetDestination - reductionInDistance;
    }
}
