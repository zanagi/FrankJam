using UnityEngine;
using System.Collections;

public class SelectableObject : MonoBehaviour
{
    protected static SelectableObject selected;

    public SelectableWindow windowPrefab;
    public SelectableWindow windowInstance;

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
    }

    protected virtual void ShowWindow()
    {
        if (!windowInstance)
            return;

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
