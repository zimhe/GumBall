using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleHandle : HandleObject
{
    public GameObject handleArm;

    Vector3 localPosition;
    Vector3 armLocalScale;

    public override void OnEnable()
    {
        base.OnEnable();
        localPosition = transform.localPosition;
        armLocalScale = handleArm.transform.localScale;
    }
 
    public override void OnEnter()
    {
        base.OnEnter();
        handleArm.GetComponent<Renderer>().sharedMaterial = this.hoverMaterial;
    }

    public override void OnExit()
    {
        base.OnExit();
        handleArm.GetComponent<Renderer>().sharedMaterial = this.defaultMaterial;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        transform.localPosition = localPosition;
        handleArm.transform.localScale=armLocalScale;
    }
}
