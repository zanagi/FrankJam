using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectableWindow : MonoBehaviour {

    private RectTransform rectTransform;
    
    public void SetPivot(Vector2 pivot)
    {
        if(!rectTransform)
            rectTransform = GetComponent<RectTransform>();
        rectTransform.pivot = pivot;
    }
}
