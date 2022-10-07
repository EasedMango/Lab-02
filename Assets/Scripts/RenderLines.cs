using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLines : MonoBehaviour
{
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        Transform[] children = GetComponentsInChildren<Transform>();


        lineRenderer.positionCount = children.Length;
        int i=0;
        foreach (Transform child in children)
        {
            lineRenderer.SetPosition(i, child.position);
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
