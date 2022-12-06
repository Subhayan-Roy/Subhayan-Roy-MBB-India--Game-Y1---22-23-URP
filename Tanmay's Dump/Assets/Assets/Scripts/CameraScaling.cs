using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        var main = Camera.main;
        if (main != null)
        {
            main.orthographicSize = 9.6f * ((9f / 16f) / main.aspect);
        }
    }
}

