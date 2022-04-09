using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleHandleTool : HandleTool
{
    const string X = "X";
    const string Y = "Y";
    const string Z = "Z";
    const string XYZ = "XYZ";

    public ScaleHandle handleX;
    public ScaleHandle handleY;
    public ScaleHandle handleZ;

    public override void Execute(string actionKey,PointerEventData eventData)

    {
        base.Execute(actionKey,eventData);

        //if(target == null)
        //{
        //    return; 
        //}

        Vector3 delta = eventData.delta*sensitivity;

        Vector3 p0 = eventData.pressEventCamera.WorldToScreenPoint(transform.position);
        Vector3 p1;
        Vector3 dir;
        Vector3 scale = Vector3.zero;
        float dot;


        switch (actionKey)
        {
            case X:
                p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + transform.right);
                dir = p1 - p0;

                dot = Vector3.Dot(dir.normalized, delta);
                scale.x+=dot;

                handleX.transform.Translate(Vector3.right * dot, transform);
                handleX.handleArm.transform.localScale = new Vector3(handleX.transform.localPosition.x, 1, 1);

                break;
            case Y:
                p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + transform.up);
                dir = p1 - p0;

                dot = Vector3.Dot(dir.normalized, delta);
                scale.y += dot;
                handleY.transform.Translate(Vector3.up * dot, transform);
                handleY.handleArm.transform.localScale = new Vector3(1,handleY.transform.localPosition.y, 1);
                break;
            case Z:
                p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + transform.forward);
                dir = p1 - p0;

                dot = Vector3.Dot(dir.normalized, delta);
                scale.z += dot;
                handleZ.transform.Translate(Vector3.forward * dot, transform);
                handleZ.handleArm.transform.localScale = new Vector3(1,1, handleZ.transform.localPosition.z);
                break;
            case XYZ:
                p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + transform.forward+transform.up+transform.right);
                dir = p1 - p0;
                dot = Vector3.Dot(dir.normalized, delta);
                scale.x += dot;
                scale.y += dot;
                scale.z += dot;

                handleX.transform.Translate(Vector3.right * dot, transform);
                handleX.handleArm.transform.localScale = new Vector3(handleX.transform.localPosition.x, 1, 1);

                handleY.transform.Translate(Vector3.up * dot, transform);
                handleY.handleArm.transform.localScale = new Vector3(1, handleY.transform.localPosition.y, 1);

                handleZ.transform.Translate(Vector3.forward * dot, transform);
                handleZ.handleArm.transform.localScale = new Vector3(1, 1, handleZ.transform.localPosition.z);

                break;
            default:
                return;
        }

        if (target)
        {
            target.transform.localScale+=scale;
        }
    }
}
