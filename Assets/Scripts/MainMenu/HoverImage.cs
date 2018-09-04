using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class HoverImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Sprite normalSprite, hoverSprite;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        image.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData data)
    {
        image.sprite = normalSprite;
    }
}
