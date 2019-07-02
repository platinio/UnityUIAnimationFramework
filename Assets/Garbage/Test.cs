using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PLatinio
{
    public class Test : MonoBehaviour
    {
        public Vector2 dimensions;
        public RectTransform canvas = null;
        public Vector2 debug;

        public void Awake()
        {
            debug = new Vector2(dimensions.x / canvas.sizeDelta.x , dimensions.y / canvas.sizeDelta.y);
            
        }
    }

}

