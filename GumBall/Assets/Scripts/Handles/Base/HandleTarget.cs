using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleTarget : MonoBehaviour
{
    bool isSelected=false;
    public virtual void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (isSelected && Input.GetMouseButtonUp(0))
            {
                Deselect(true);
            }
        }
        else
        {
            if (!isSelected && Input.GetMouseButtonDown(0))
            {
                OnSelect();
            }
            if (isSelected && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Middle Button");
                HandleManager.CycleTools(this);
            }
        }

     
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnExit()
    {

    }

    public void OnMouseEnter()
    {
        OnEnter();
    }

    public void OnMouseExit()
    {
        if (!isSelected)
        {
            OnExit();
        }
    }

    public virtual void OnSelect()
    {

        if (HandleManager.OnTargetSelected(this))
        {
            isSelected = true;
            HandleManager.EnableTools(this);
        }
    }

    public virtual void Deselect(bool isOwner=false)
    {
        if (isOwner)
        {
            HandleManager.OnTargetDeselected();
            HandleManager.DisableTools();
        }
        isSelected = false;
        OnExit();
    }
}
