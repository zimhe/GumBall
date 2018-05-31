using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectForSelect : SelectiveObject
{
    [SerializeField] private float HLScale = 0.6f;
    [SerializeField] private float DFScale = 0.5f;


    public override void Highlight()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * HLScale, Time.deltaTime * 5f);
    }

    public override void Restore()
    {
        transform.localScale = DFScale * Vector3.one;
    }
}
