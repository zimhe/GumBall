using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleTool : MonoBehaviour
{
    public HandleTarget target;

    public float sensitivity = 0.1f;

    public bool hasHandle;

    public virtual void BeforeDrag()
    {

    }

    public virtual void Execute(string actionKey, PointerEventData eventData)
    {

    }

    public virtual void TakeTarget(HandleTarget target)
    {
        this.target = target;

        transform.position=target.transform.position;
        transform.rotation=target.transform.rotation;
    }

    public virtual void ClearTarget()
    {
        this.target = null;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

}
