using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleObject : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler,IDragHandler
{
    public Material hoverMaterial;
    public HandleTool m_tool;
    public string actionKey;

    bool isDraging = false;
    bool handleActive = false;

    internal Material defaultMaterial;

    public  virtual void OnEnable()
    {
        defaultMaterial = GetComponent<Renderer>().sharedMaterial;
    }

    public virtual void Drag(PointerEventData eventData)
    {
        m_tool.Execute(actionKey,eventData);
    }

    public virtual void OnEnter()
    {
       
        GetComponent<Renderer>().sharedMaterial = hoverMaterial;
    }

    public virtual void OnExit()
    {
        GetComponent<Renderer>().sharedMaterial = defaultMaterial;
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
      
        if (!m_tool.hasHandle && eventData.button == PointerEventData.InputButton.Left)
        {
            m_tool.BeforeDrag();
            OnEnter();
            handleActive = true;
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!m_tool.hasHandle)
        {
            OnEnter();
            handleActive = true;
        }
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!isDraging && handleActive)
        {
            OnExit();
            handleActive = false;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        m_tool.hasHandle = false;
        isDraging = false;
        if (handleActive)
        {
            if (!eventData.hovered.Contains(gameObject))
            {
                handleActive = false;
                OnExit();
            }
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (handleActive&&eventData.button==PointerEventData.InputButton.Left)
        {
            isDraging = true;
            m_tool.hasHandle = true;
            Drag(eventData);
        }
    }
}
