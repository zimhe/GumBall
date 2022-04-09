using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(AddRingColliders))]
public class AddRingCollidersEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
#endif
public class AddRingColliders : MonoBehaviour
{
  public void AddRingCollider(float radius, int numbers,Vector3 sizeParam,int type)
    {

        Type colliderType = null;

        if (type == 0)
        {
            colliderType = typeof(BoxCollider);
        }

        else if (type == 1)
        {
            colliderType= typeof(CapsuleCollider);
        }
        else
        {
            colliderType = typeof(SphereCollider);
        }

        
        for(int i = 0; i < numbers; i++)
        {
            var collider = gameObject.AddComponent(colliderType);
        }
    }
}
