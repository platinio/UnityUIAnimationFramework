using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio;

public class Test1 : MonoBehaviour
{

    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = rect.FromAbsolutePositionToAnchoredPosition(new Vector2(1.0f , 1.0f) , PivotPreset.UpperRight);
    }
}
