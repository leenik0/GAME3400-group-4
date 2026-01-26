using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AlarmBeaconGreybox : MonoBehaviour
{
    [Header("Build")]
    public bool clearChildrenBeforeBuild = true;
    public float overallScale = 1.0f;
    public bool addProtectiveCage = true;

    [Header("Orientation")]
    [Tooltip("Local +Z is the 'protrude outward' direction from the wall. Rotate this object to face into corridor.")]
    public bool autoFaceToLocalPlusZ = true;

    [Header("Light / Emission")]
    public Color alarmColor = new Color(1f, 0.1f, 0.1f, 1f);
    public float pointLightRange = 3.8f;
    public float pointLightIntensity = 2.2f;
    public float emissionIntensity = 4.0f;

    [Header("Flash Pattern")]
    public WarningLightFlasher.Pattern pattern = WarningLightFlasher.Pattern.DoubleFlash;
    public float onTime = 0.12f;
    public float offTime = 0.18f;
    public float doubleGap = 0.10f;
    public float pauseAfterSequence = 0.75f;
    public float randomPhase = 0.18f;

    [ContextMenu("Build Greybox")]
    public void Build()
    {
#if UNITY_EDITOR
        Undo.RegisterFullObjectHierarchyUndo(gameObject, "Build Alarm Beacon Greybox");
#endif

        if (clearChildrenBeforeBuild)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
#if UNITY_EDITOR
                DestroyImmediate(transform.GetChild(i).gameObject);
#else
                Destroy(transform.GetChild(i).gameObject);
#endif
            }
        }

        transform.localScale = Vector3.one * Mathf.Max(0.01f, overallScale);

        // Root groups
        GameObject geo = new GameObject("Geo");
        geo.transform.SetParent(transform, false);

        // ---- Back plate (flush) : thin on Z
        GameObject backPlate = CreateBox("BackPlate", geo.transform,
            new Vector3(0f, 0f, 0.01f),
            new Vector3(0.18f, 0.22f, 0.03f), false);

        // ---- Arm / bracket (sticks out along +Z)
        GameObject arm = CreateBox("Arm", geo.transform,
            new Vector3(0f, -0.05f, 0.06f),
            new Vector3(0.10f, 0.05f, 0.12f), false);

        // ---- Housing (cylinder body)
        GameObject housing = CreateCylinder("Housing", geo.transform,
            new Vector3(0f, 0.02f, 0.12f),
            new Vector3(0.11f, 0.10f, 0.11f),
            Quaternion.Euler(90f, 0f, 0f), false);

        // ---- Lens dome (sphere) - glowing part
        GameObject dome = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        dome.name = "LensDome";
        dome.transform.SetParent(geo.transform, false);
        dome.transform.localPosition = new Vector3(0f, 0.06f, 0.18f);
        dome.transform.localRotation = Quaternion.identity;
        dome.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
        SafeDestroyCollider(dome);

        var domeRenderer = dome.GetComponent<Renderer>();
        Material lensMat = new Material(Shader.Find("Standard"));
        lensMat.color = new Color(0.18f, 0.18f, 0.18f, 1f);
        lensMat.EnableKeyword("_EMISSION");
        lensMat.SetColor("_EmissionColor", Color.black);
        domeRenderer.sharedMaterial = lensMat;

        // ---- Protective cage (ring + bars)
        if (addProtectiveCage)
        {
            GameObject cage = new GameObject("Cage");
            cage.transform.SetParent(geo.transform, false);
            cage.transform.localPosition = new Vector3(0f, 0.06f, 0.18f);

            GameObject ring = CreateCylinder("Ring", cage.transform,
                Vector3.zero,
                new Vector3(0.13f, 0.01f, 0.13f),
                Quaternion.Euler(90f, 0f, 0f), false);

            int barCount = 5;
            float radius = 0.055f;
            for (int i = 0; i < barCount; i++)
            {
                float ang = (i / (float)barCount) * 360f;
                float rad = ang * Mathf.Deg2Rad;

                float bx = Mathf.Cos(rad) * radius;
                float bz = Mathf.Sin(rad) * radius;

                GameObject bar = CreateBox($"Bar_{i:00}", cage.transform,
                    new Vector3(bx, 0f, bz),
                    new Vector3(0.012f, 0.13f, 0.012f), false);
            }
        }

        // ---- Light
        Light L = GetComponent<Light>();
        if (L == null) L = gameObject.AddComponent<Light>();
        L.type = LightType.Point;
        L.color = alarmColor;
        L.range = pointLightRange;
        L.intensity = 0f;
        L.transform.localPosition = new Vector3(0f, 0.06f, 0.24f);

        // ---- Flasher
        WarningLightFlasher flasher = GetComponent<WarningLightFlasher>();
        if (flasher == null) flasher = gameObject.AddComponent<WarningLightFlasher>();

        flasher.pattern = pattern;
        flasher.onTime = onTime;
        flasher.offTime = offTime;
        flasher.doubleGap = doubleGap;
        flasher.pauseAfterSequence = pauseAfterSequence;

        flasher.pointLight = L;
        flasher.baseIntensity = Mathf.Max(0f, pointLightIntensity);
        flasher.lensRenderer = domeRenderer;
        flasher.warningColor = alarmColor;
        flasher.emissionIntensity = Mathf.Max(0f, emissionIntensity);
        flasher.phaseOffset = Random.Range(0f, randomPhase);

        // Optional: ensure object rotation meaning is clear
        if (autoFaceToLocalPlusZ)
        {
            // Do nothing here by default; you rotate the prefab in scene to aim +Z outward.
            // This flag is just a reminder in Inspector.
        }
    }

    GameObject CreateBox(string name, Transform parent, Vector3 localPos, Vector3 localScale, bool addCollider)
    {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);
        g.name = name;
        g.transform.SetParent(parent, false);
        g.transform.localPosition = localPos;
        g.transform.localRotation = Quaternion.identity;
        g.transform.localScale = localScale;

        if (!addCollider) SafeDestroyCollider(g);
        return g;
    }

    GameObject CreateCylinder(string name, Transform parent, Vector3 localPos, Vector3 localScale, Quaternion localRot, bool addCollider)
    {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        g.name = name;
        g.transform.SetParent(parent, false);
        g.transform.localPosition = localPos;
        g.transform.localRotation = localRot;
        g.transform.localScale = localScale;

        if (!addCollider) SafeDestroyCollider(g);
        return g;
    }

    void SafeDestroyCollider(GameObject g)
    {
        var c = g.GetComponent<Collider>();
        if (c != null)
        {
#if UNITY_EDITOR
            DestroyImmediate(c);
#else
            Destroy(c);
#endif
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AlarmBeaconGreybox))]
public class AlarmBeaconGreyboxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Build Greybox"))
        {
            ((AlarmBeaconGreybox)target).Build();
        }
    }
}
#endif
