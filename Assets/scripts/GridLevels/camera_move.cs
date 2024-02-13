using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    public GameObject target;
    Vector3 mPreviousPosition = Vector3.zero;
    Vector3 mPositionDelta = Vector3.zero; //change of position from last frame to this frame

    Vector2 m2DPrevPos = Vector2.zero;
    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            mPositionDelta = Input.mousePosition - mPreviousPosition;
            mPositionDelta.y = 0f;
            mPositionDelta.x = - mPositionDelta.x;
            transform.RotateAround(target.transform.position, InvertMovement(mPositionDelta), - 75 * Time.deltaTime);
        }
        mPreviousPosition = Input.mousePosition;
    }

    Vector3 InvertMovement(Vector3 mouse_position)
    {
        return new Vector3(mouse_position.y, mouse_position.x, mouse_position.z);
    }
}
