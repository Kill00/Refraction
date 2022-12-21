using System.Collections;
using TMPro;
using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private TextMeshProUGUI text;

    private static bool _reset = true;

    public void Update()
    {
        var t = lineRenderer.transform.position;
        lineRenderer.SetPosition(0, new Vector3(t.x, t.y, 0));
        
        if (!_reset) return;
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(0));
        lineRenderer.SetPosition(2, lineRenderer.GetPosition(0));
    }

    public void ResetRay()
    {
        Value.Instance.InputFieldE();
        text.text = $"굴절각 : 0°";
        StopAllCoroutines();
        _reset = true;
    }

    public void Try()
    {
        _reset = false;
        Value.Instance.InputFieldD();
        StartCoroutine(TryCoroutine());
    }

    private IEnumerator TryCoroutine()
    {
        var t = lineRenderer.transform.position;
        lineRenderer.SetPosition(1, new Vector3(t.x, t.y, 0));
        while (lineRenderer.GetPosition(1).y > 0 && lineRenderer.GetPosition(1).x < 8.91f)
        {
            lineRenderer.SetPosition(1,
                new Vector3(lineRenderer.GetPosition(1).x + 0.01f * Mathf.Sin(AngleToRadian(Value.IncidentRayAngle)),
                    lineRenderer.GetPosition(1).y - 0.01f * Mathf.Cos(AngleToRadian(Value.IncidentRayAngle)), 0));
        }
        
        var r = Value.N1RefractiveIndex * Mathf.Sin(AngleToRadian(Value.IncidentRayAngle)) / Value.N2RefractiveIndex; // == Sin(theta2)
        float angle;
        if (r > 1)
        {
            angle = -AngleToRadian(Value.IncidentRayAngle);
        }
        else
        {
            angle = Mathf.Asin(r);
        }
        
        lineRenderer.SetPosition(2, lineRenderer.GetPosition(1));
        while (lineRenderer.GetPosition(2).y is < 5 and > -5 && lineRenderer.GetPosition(2).x is < 8.91f and > -8.91f)
        {
            if (r > 1)
            {
                lineRenderer.SetPosition(2,
                    new Vector3(lineRenderer.GetPosition(2).x - 0.01f * Mathf.Sin(angle),
                        lineRenderer.GetPosition(2).y + 0.01f * Mathf.Cos(angle), 0));
            }
            else
            {
                lineRenderer.SetPosition(2,
                    new Vector3(lineRenderer.GetPosition(2).x + 0.01f * Mathf.Sin(angle),
                        lineRenderer.GetPosition(2).y - 0.01f * Mathf.Cos(angle), 0));
            }
        }
        
        text.text = $"굴절각 : {Mathf.Abs(RadianToAngle(angle))}°";
        yield break;
    }
    
    private static float AngleToRadian(float angle)
    {
        return angle * Mathf.PI / 180;
    }
    
    private static float RadianToAngle(float radian)
    {
        return radian * 180 / Mathf.PI;
    }
}