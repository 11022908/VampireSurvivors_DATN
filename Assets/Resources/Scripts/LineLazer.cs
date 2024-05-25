using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineLazer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.startColor = Color.red;
        //lineRenderer.endColor = Color.red;
        //lineRenderer.startWidth = 0.4f;
        //lineRenderer.endWidth = 0.4f;
        StartCoroutine(HideLineLazer(0.25f));
    }

    public void DrawLine(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
    IEnumerator HideLineLazer(float time)
    {
        yield return new WaitForSeconds(time);
        lineRenderer.enabled = false;
    }
}
