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

        [SerializeField]
        private float _sensitivity = 1.0f;

        [SerializeField]
        private float _stiffness = 10.0f;

        [SerializeField] private GumBall gumBall;


        [SerializeField] private float GumballSize = 0.1f;

        private RaycastHit RcHit;

        private RaycastHit GumHit;

        private Transform Target;

        private Vector3 _position;

        List<Transform> TargetList=new List<Transform>();

        private int ActionState;

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
            
            gumBall.transform.localScale = Mathf.Abs(transform.localPosition.z)*0.5f * GumballSize*Vector3.one;
        }

        void SelectionOperator()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            var point = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (Physics.Raycast(ray, out RcHit))
            {
                if (RcHit.transform.GetComponent<SelectiveObject>())
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Target = RcHit.transform;

                        if (!TargetList.Contains(Target))
                            TargetList.Add(Target);

                        gumBall.gameObject.SetActive(true);

                        gumBall.transform.position = Target.position;

                        _position = gumBall.transform.position;
                    }
                }
            }

            if (Physics.Raycast(ray,out GumHit))
            {
                if (GumHit.transform.parent == gumBall.transform&&!OnAction)
                {
                    gumBall.overAxis = GumHit.transform;
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (gumBall.overAxis != null)
                {
                    var t = _sensitivity * transform.localPosition.z * 0.1f;

                    var dx = Input.GetAxis("Mouse X") * t;
                    var dy = Input.GetAxis("Mouse Y") * t;

                    if (dx != 0f || dy != 0f)
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
                        px -= tv.x*0.5f;
                        pz -= tv.z*0.5f;
                    }
                    if (gumBall.overAxis.name == "XY")
                    {
                        px -= tv.x*0.5f;
                        py -= tv.y*0.5f;
                    }
                    if (gumBall.overAxis.name == "YZ")
                    {
                        py -= tv.y*0.5f;
                        pz -= tv.z*0.5f;
                    }

                    _position +=new Vector3(px,py,pz);

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

                var radius = Mathf.Abs(transform.localPosition.z) * GumballSize*0.5f;
               
                if (!Physics.SphereCast(ray,radius)&&!OnAction)
                {
                    gumBall.Restore();
                    gumBall.gameObject.SetActive(false);
                }
            }
        }
    }

