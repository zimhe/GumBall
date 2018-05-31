using UnityEngine;
using System.Collections;

public abstract class SelectiveObject : MonoBehaviour
{

    private bool over;

    private bool selected;

 



    private void OnMouseEnter()
    {
        over = true;
    }



    private void OnMouseOver()
    {
        Highlight();
    }

    private void OnMouseExit()
    {
        Restore();
        over = false;
    }

    public bool IsOver()
    {
        return over;
    }

    public bool IsSelected()
    {
        return selected;
    }



    public abstract void Highlight();


    public abstract void Restore();

}
