using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionHandleTool : HandleTool
{
    const string X = "X";
    const string Y = "Y";
    const string Z = "Z";
    const string XY = "XY";
    const string YZ = "YZ";
    const string XZ = "XZ";




    public override void Execute(string actionKey,PointerEventData eventData=null)
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
        Vector3 motion = Vector3.zero;
        float dot;
      

        switch (actionKey)
        {
            case X:
                 p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + transform.right);
                dir= p1 - p0;

                dot = Vector3.Dot(dir.normalized, delta);
                motion = transform.right * dot;
               
                break;
            case Y:
                p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + transform.up);
                dir = p1 - p0;

                dot = Vector3.Dot(dir.normalized, delta);
                motion = transform.up * dot;
                break;
            case Z:
                p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + transform.forward);
                dir = p1 - p0;

                dot = Vector3.Dot(dir.normalized, delta);
                motion = transform.forward * dot;
                break;
            //case XY:
            //    var dirXY = (transform.right + transform.up).normalized;
            //    p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + dirXY);
            //    dir = p1 - p0;

            //    dot = Vector3.Dot(dir.normalized, delta);
            //    motion = dirXY* dot;
            //    break;
            //case YZ:
            //    var dirYZ = (transform.forward + transform.up).normalized;
            //    p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + dirYZ);
            //    dir = p1 - p0;

            //    dot = Vector3.Dot(dir.normalized, delta);
            //    motion = dirYZ * dot;
            //    break;
            //case XZ:
            //    var dirXZ = (transform.forward + transform.right).normalized;
            //    p1 = eventData.pressEventCamera.WorldToScreenPoint(transform.position + dirXZ);
            //    dir = p1 - p0;

            //    dot = Vector3.Dot(dir.normalized, delta);
            //    motion = dirXZ * dot;
            //    break;
            default:
                return;
        }

        transform.Translate(motion,Space.World);
        if (target)
        {
            target.transform.position = transform.position;
        }
    }
}
