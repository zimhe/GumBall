using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationHandle : HandleObject
{
    public GameObject highlightObject;

    public Vector3 handlePoint;


    public override void OnEnter()
    {
        //base.OnEnter();
        highlightObject.SetActive(true);
    }

    public override void OnExit()
    {
        //base.OnExit();
        highlightObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        
        base.OnPointerDown(eventData);
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            handlePoint = eventData.pressPosition;
        }
       
    }
}
