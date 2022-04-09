using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionHandleComposit : HandleObject
{
    public PositionHandle handle0;
    public PositionHandle handle1;
    
    // Start is called before the first frame update
    public override void Drag(PointerEventData eventData)
    {
       
        handle0.Drag(eventData);
        handle1.Drag(eventData);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        handle0.OnEnter();
        handle1.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        handle0.OnExit();
        handle1.OnExit();
       
    }
}
