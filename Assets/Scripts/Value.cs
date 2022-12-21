using System.Globalization;
using TMPro;
using UnityEngine;

public class Value : MonoBehaviour
{
    public static float IncidentRayAngle = 45;
    public static float RefractedRayAngle;
    public static float N1RefractiveIndex = 1f;
    public static float N2RefractiveIndex = 1f;
    
    public static Value Instance;
    
    [SerializeField] private TMP_InputField incidentRayAngle;
    [SerializeField] private TMP_InputField n1RefractiveIndex;
    [SerializeField] private TMP_InputField n2RefractiveIndex;
    [SerializeField] private Transform incidentRay;

    public void IncidentAngle_Changed()
    {
        if (incidentRayAngle.text == "") return;
        
        if (float.Parse(incidentRayAngle.text) > 90) incidentRayAngle.text = "90";
        if (float.Parse(incidentRayAngle.text) < 0) incidentRayAngle.text = "0";
        IncidentRayAngle = float.Parse(incidentRayAngle.text);
    }
    
    public void N1RefractiveIndex_Changed()
    {
        N1RefractiveIndex = float.Parse(n1RefractiveIndex.text);
    }
    
    public void N2RefractiveIndex_Changed()
    {
        N2RefractiveIndex = float.Parse(n2RefractiveIndex.text);
    }
    
    public void InputFieldE()
    {
        incidentRayAngle.interactable = true;
        n1RefractiveIndex.interactable = true;
        n2RefractiveIndex.interactable = true;
    }
    
    public void InputFieldD()
    {
        incidentRayAngle.interactable = false;
        n1RefractiveIndex.interactable = false;
        n2RefractiveIndex.interactable = false;
    }

    private void Awake()
    {
        Instance = this;
        
        incidentRayAngle.text = IncidentRayAngle.ToString(CultureInfo.InvariantCulture);
        n1RefractiveIndex.text = N1RefractiveIndex.ToString(CultureInfo.InvariantCulture);
        n2RefractiveIndex.text = N2RefractiveIndex.ToString(CultureInfo.InvariantCulture);
    }

    private void Update()
    {
        incidentRay.eulerAngles =
            incidentRayAngle.text == "" ? new Vector3(0, 0, 0) : new Vector3(0, 0, IncidentRayAngle);
    }
}
