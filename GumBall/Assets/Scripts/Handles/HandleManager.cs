using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleManager : MonoBehaviour
{

    [SerializeField] PositionHandleTool positionTool;
    [SerializeField] RotationHandleTool rotationTool;
    [SerializeField] ScaleHandleTool scaleTool;
    [SerializeField] Camera eventCamera;
    // Start is called before the first frame update

    public static HandleManager instance { get; private set; }

    int toolIndex;

    private void OnEnable()
    {
        instance = this;

        var mainCam = Camera.main;
        eventCamera.transform.parent = mainCam.transform;
        eventCamera.transform.localPosition = Vector3.zero;
        eventCamera.transform.localRotation = Quaternion.identity;
        eventCamera.fieldOfView=mainCam.fieldOfView;
        eventCamera.enabled = false;

        DisableTools();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    HandleTarget selected;

    public static bool OnTargetSelected(HandleTarget target)
    {
        if (instance == null)
        {
            return false;
        }

        if (instance.selected != null)
        {
            if (instance.selected != target)
            {
                instance.selected.Deselect(false);
                instance.selected = target;
                instance.eventCamera.enabled = true;
                return true;
            }
        }
        else
        {
            instance.selected = target;
            instance.eventCamera.enabled = true;
            return true;
        }
        return false;
    }

    public static void OnTargetDeselected()
    {
        if (instance == null)
        {
            return;
        }

        instance.selected = null;
        DisableTools();
        instance.eventCamera.enabled=false;
    }


    public static void DisableTools()
    {
        instance.rotationTool.ClearTarget();
        instance.scaleTool.ClearTarget();
        instance.positionTool.ClearTarget();
        instance.rotationTool.gameObject.SetActive(false);
        instance.scaleTool.gameObject.SetActive(false);
        instance.positionTool.gameObject.SetActive(false);
    }

    public static void EnableTools(HandleTarget target)
    {
        if (instance == null)
        {
            return;
        }

        if (instance.toolIndex == 0)
        {
            instance.rotationTool.ClearTarget();
            instance.scaleTool.ClearTarget();
            instance.positionTool.TakeTarget(target);

            instance.rotationTool.gameObject.SetActive(false);
            instance.scaleTool.gameObject.SetActive(false);
            instance.positionTool.gameObject.SetActive(true);
        }
        else if (instance.toolIndex == 1)
        {
            instance.positionTool.ClearTarget();
            instance.scaleTool.ClearTarget();
            instance.rotationTool.TakeTarget(target);

            instance.positionTool.gameObject.SetActive(false);
            instance.scaleTool.gameObject.SetActive(false);
            instance.rotationTool.gameObject.SetActive(true);
        }
        else if (instance.toolIndex == 2)
        {
            instance.positionTool.ClearTarget();
            instance.rotationTool.ClearTarget();
            instance.scaleTool.TakeTarget(target);

            instance.positionTool.gameObject.SetActive(false);
            instance.rotationTool.gameObject.SetActive(false);
            instance.scaleTool.gameObject.SetActive(true);
        }

    }

    public static void CycleTools(HandleTarget target)
    {
        if (instance == null)
        {
            return;
        }

        instance.toolIndex ++;
        instance.toolIndex %= 3;

        EnableTools(target);
    }
}
