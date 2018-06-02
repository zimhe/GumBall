using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*
 * Notes
 */


/// <summary>
/// attach this script to the camera
/// </summary>
public class CamObjectSelector : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private float _sensitivity = 1.0f;

    [SerializeField] private float _stiffness = 10.0f;

    [SerializeField] private GumBall gumBall;

    private int mask;

    [SerializeField] private float GumballSize = 0.1f;

    private RaycastHit RcHit;

    private RaycastHit GumHit;

    private Transform Target;

    private Vector3 _position;


    private bool OnAction;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        _camera = GetComponent<Camera>();

        gumBall.gameObject.SetActive(false);


    }


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {

        SelectionOperator();

        updateGumBall();
    }

    void updateGumBall()
    {

        gumBall.transform.localScale = Mathf.Abs(transform.localPosition.z) * GumballSize * Vector3.one*0.1f;



        if (!SomethingOnRadius())
        {
            gumBall.ResetColor();
        }
    }

    void SelectionOperator()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RcHit))
        {
            if (RcHit.transform.GetComponent<SelectiveObject>())
            {
                if (Input.GetMouseButtonDown(0))
                {

                    if (Target != RcHit.transform&&Target!=null)
                    {
                        Target.GetComponent<SelectiveObject>().SetState(SelectiveObject.SelectionState.Default);
                        Target = RcHit.transform;
                    }
                    else
                    {
                        Target = RcHit.transform;
                    }
                       

                    Target.GetComponent<SelectiveObject>() .SetState(SelectiveObject.SelectionState.Selected);

                    gumBall.gameObject.SetActive(true);

                    gumBall.transform.position = Target.position;

                    _position = gumBall.transform.position;
                }
            }
        }

        mask = LayerMask.GetMask("Gumball");
        

        if (Physics.Raycast(ray, out GumHit,Mathf.Infinity,mask))
        {


            if (GumHit.transform.parent == gumBall.transform && !OnAction)
            {
                gumBall.overAxis = GumHit.transform;
                gumBall.HIghlightAxis();
            }
        }


        if (Input.GetMouseButton(0))
        {
            if (gumBall.overAxis != null)
            {
                var t = _sensitivity * transform.localPosition.z * 0.1f;

                var dx = Input.GetAxis("Mouse X") * t;
                var dy = Input.GetAxis("Mouse Y") * t;

                if (dx != 0.0f || dy != 0.0f)
                {
                    OnAction = true;
                }

                var tv = transform.TransformVector(dx, dy, 0f);

                float px = 0f;
                float py = 0f;
                float pz = 0f;



                if (gumBall.overAxis.name == "X Axis")
                {
                    px -= tv.x;
                }

                if (gumBall.overAxis.name == "Y Axis")
                {
                    py -= tv.y;
                }

                if (gumBall.overAxis.name == "Z Axis")
                {
                    pz -= tv.z;
                }

                if (gumBall.overAxis.name == "XZ")
                {
                    px -= tv.x;
                    pz -= tv.z;
                }

                if (gumBall.overAxis.name == "XY")
                {
                    px -= tv.x;
                    py -= tv.y;
                }

                if (gumBall.overAxis.name == "YZ")
                {
                    py -= tv.y;
                    pz -= tv.z;
                }

                _position += new Vector3(px, py, pz);

                gumBall.transform.position =
                    Vector3.Lerp(gumBall.transform.position, _position, Time.deltaTime * _stiffness);

                Target.position = gumBall.transform.position;
            }
        }
        else
        {
            _position = gumBall.transform.position;

            OnAction = false;


        }

        if (Input.GetMouseButtonDown(0))
        {

            var radius = Mathf.Abs(transform.localPosition.z) * GumballSize;

            if (!SomethingOnRadius()&&Target!=null)
            {
                gumBall.Restore();
                gumBall.gameObject.SetActive(false);

                Target.GetComponent<SelectiveObject>().SetState(SelectiveObject.SelectionState.Default); 
                Target = null;
            }
        }
    }



    private bool SomethingOnRadius()
    {
        bool onRadius;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var radius = Mathf.Abs(transform.localPosition.z) * GumballSize*0.1f;

        if (!Physics.SphereCast(ray, radius) && !OnAction)
        {

            onRadius = false;
        }
        else
        {
            onRadius = true;
        }


        return onRadius;
    }
}

