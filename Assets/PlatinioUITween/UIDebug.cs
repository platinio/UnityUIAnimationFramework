using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDebug : MonoBehaviour
{
    private RectTransform m_rect = null;

    
    private void OnDrawGizmosSelected()
    {
        if(m_rect == null)
            m_rect = GetComponent<RectTransform>();

        Debug.Log("AnchorPosition: " + m_rect.anchoredPosition);
        Debug.Log("Position " + m_rect.position);
        Debug.Log( new Vector2(Screen.width , Screen.height) );
    }
}
