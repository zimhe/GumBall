using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleHandleComposit : HandleObject
{
    public ScaleHandle X;
    public ScaleHandle Y;
    public ScaleHandle Z;
    public override void Drag(PointerEventData eventData)
    {

        base.Drag(eventData);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        X.OnEnter();
        Y.OnEnter();
        Z.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        X.OnExit();
        Y.OnExit();
        Z.OnExit();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        X.OnPointerUp(eventData);
        Y.OnPointerUp(eventData);
        Z.OnPointerUp(eventData);
    }
}
