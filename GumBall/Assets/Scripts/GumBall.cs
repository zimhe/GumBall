using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GumBall : MonoBehaviour
{
    [SerializeField] private Color OnAxisColor;

    [SerializeField] private Transform AxisX;
    [SerializeField] private Transform AxisY;
    [SerializeField] private Transform AxisZ;

    [SerializeField] private Transform XZ;
    [SerializeField] private Transform XY;
    [SerializeField] private Transform YZ;

    [SerializeField] private Transform O;


    public Transform overAxis { get; set; }

    private List<Transform> Axises = new List<Transform>(6);


    Dictionary<Transform,Color> DefColor= new Dictionary<Transform, Color>();


    // Use this for initialization
    void Start ()
    {
        Axises.AddRange(new [] {AxisX, AxisY, AxisZ,XZ,XY,YZ,O});

        Axises.ForEach(a => { DefColor.Add(a, a.GetComponent<MeshRenderer>().material.color); });

      
    }
	
	// Update is called once per frame
	void Update ()
	{
		
      
	}


    public void HIghlightAxis()
    {
        Axises.ForEach(a =>
        {
            if (overAxis != a)
            {
                a.GetComponent<MeshRenderer>().material.color = DefColor[a];
                if (a.childCount > 0)
                    a.GetChild(0).GetComponent<MeshRenderer>().material.color = DefColor[a];

            }
        });


        if (overAxis != null)
        {
            overAxis.GetComponent<MeshRenderer>().material.color = OnAxisColor;
            if (overAxis.childCount>0)
            {
                overAxis.GetChild(0).GetComponent<MeshRenderer>().material.color = OnAxisColor;
            }
        }
    }

    public void Restore()
    {
        overAxis = null;
        ResetColor();
    }

    public void ResetColor()
    {
        Axises.ForEach(a => { a.GetComponent<MeshRenderer>().material.color = DefColor[a]; if(a.childCount>0)a
                .GetChild(0).GetComponent<MeshRenderer>().material.color = DefColor[a];
        });
    }

}
