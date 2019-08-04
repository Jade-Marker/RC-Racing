using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] GameObject leftSlot;
    [SerializeField] GameObject rightSlot;
    [SerializeField] float imageWidth = 93 / 2;
    [SerializeField] float imageHeight = 333 / 2;


    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2Int mousePos = new Vector2Int(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y));
            Vector2 localSpace = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform,mousePos,Camera.main, out localSpace);
            Vector2 localSpaceCorrected = new Vector2(localSpace.y,localSpace.x);
            if ((localSpaceCorrected.y >= rectTransform.position.y - imageHeight) && (localSpaceCorrected.y <= rectTransform.position.y + imageHeight)) {
                if ((localSpaceCorrected.x >= rectTransform.position.x - imageWidth) && (localSpaceCorrected.x <= rectTransform.position.x + imageWidth)) {
                    if ((mousePos.x - imageWidth >= 1440) && (mousePos.x + imageWidth <= 1920))
                    {
                        if ((mousePos.y - imageHeight >= 0) && (mousePos.y + imageHeight <= 1080))
                        {
                            float oldZ = rectTransform.position.z;
                            rectTransform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Input.mousePosition.z));
                            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y, oldZ);
                        }
                    }
                }
            }
        }
    }
}
