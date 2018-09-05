using UnityEngine;
using System.Collections;

public abstract class SelectableObject : MonoBehaviour
{
    protected static SelectableObject selected;

    public SelectableWindow windowPrefab;
    protected SelectableWindow windowInstance;

    protected virtual void Start()
    {
        if (windowPrefab)
        {
            windowInstance = Instantiate(windowPrefab, GameManager.Instance.selectableWindowTransform);
            windowInstance.Init(this);
        }
    }

    protected virtual void Update()
    {
        if(selected == this)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnDeselect();
        }
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
        ShowWindow();
    }

    public virtual void OnDeselect()
    {
        if (selected == this)
        {
            selected = null;
            HideWindow();
        }
        Debug.Log("Deselect: " + name);
    }

    protected virtual void ShowWindow()
    {
        windowInstance.gameObject.SetActive(true);
        var viewportPos = 
            GameManager.Instance.GameCamera.Camera.WorldToViewportPoint(transform.position);

        var pivot = Vector2.zero;
        if (viewportPos.x > 0.5f)
            pivot.x = 1;
        if (viewportPos.y > 0.5f)
            pivot.y = 1;
        windowInstance.SetPivot(pivot);
    }

    protected virtual void HideWindow()
    {
        if (!windowInstance)
            return;
        windowInstance.gameObject.SetActive(false);
    }
}
