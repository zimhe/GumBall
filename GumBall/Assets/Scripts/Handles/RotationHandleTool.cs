using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationHandleTool : HandleTool
{
    const string X = "X";
    const string Y = "Y";
    const string Z = "Z";

    public RotationHandle handleX;
    public RotationHandle handleY;
    public RotationHandle handleZ;


    public override void Execute(string actionKey, PointerEventData eventData = null)
    {
        base.Execute(actionKey,eventData);

        Vector3 delta = eventData.delta*sensitivity;
        Vector3 p0 = eventData.pressEventCamera.WorldToScreenPoint(transform.position);
        Vector3 lookDir=eventData.pressEventCamera.transform.forward;
        Vector3 p1;
        Vector3 dir;
        float angle = 0;
       
     
        Vector3 axis = Vector3.zero;

        switch (actionKey)
        {
            case "X":
                p1 = handleX.handlePoint;
                dir = p1 - p0;
                axis = transform.right;
                break;
            case "Y":
                p1 = handleY.handlePoint;
                dir = p1 - p0;
                axis = transform.up;
                break;
            case "Z":
                p1 = handleZ.handlePoint;
                dir = p1 - p0;
   
                axis = transform.forward;
                break;
            default:
                return;
        }

        float dot = Vector3.Dot(lookDir, axis);

        if(Mathf.Abs(dot) < 0.05f)
        {
            angle = -Vector3.Dot(dir.normalized, delta);
        }
        else
        {
            int flip = dot < 0 ? 1 : -1;

            angle = Vector3.Dot(Vector3.Cross(dir.normalized, Vector3.forward), delta) * flip;
        }

      
        transform.Rotate(axis, angle,Space.World);

        if (target)
        {
            target.transform.rotation=transform.rotation;
        }
    }
}
