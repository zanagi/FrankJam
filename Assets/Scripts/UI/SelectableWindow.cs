using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableWindow : MonoBehaviour {

    public Text title, price, description;
    public Button actionButton1;
    
    private RectTransform rectTransform;
    private SelectableObject selectable;

    public void Init(SelectableObject selectable)
    {
        gameObject.SetActive(false);
        this.selectable = selectable;
    }

    private void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        transform.position =
            GameManager.Instance.GameCamera.Camera.WorldToScreenPoint(selectable.transform.position);
    }

    public void SetPivot(Vector2 pivot)
    {
        if(!rectTransform)
            rectTransform = GetComponent<RectTransform>();
        rectTransform.pivot = pivot;
        SetPosition();
    }
}
