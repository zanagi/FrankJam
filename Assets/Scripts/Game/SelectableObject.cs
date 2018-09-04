using UnityEngine;
using System.Collections;

public abstract class SelectableObject : MonoBehaviour
{
    protected static SelectableObject selected;

    public SelectableWindow windowPrefab;
    protected SelectableWindow windowInstance;

    protected virtual void Start()
    {
        if(windowPrefab)
            windowInstance = Instantiate(windowPrefab, GameManager.Instance.selectableWindowTransform);
    }

    public virtual void OnSelect()
    {
        var previousSelected = selected;

        if (previousSelected)
        {
            previousSelected.OnDeselect();

            if (previousSelected == this)
                return;
        }
        selected = this;
        Debug.Log("Select: " + name);
    }

    public virtual void OnDeselect()
    {
        if (selected == this)
            selected = null;
        Debug.Log("Deselect: " + name);
    }
}
