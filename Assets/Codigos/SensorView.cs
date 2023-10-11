using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataViewBase
{
    #region RangeView


    [Header("----- RangeView -----")]
    [Range(0, 180)]
    public float angle = 30f;
    public float height = 1.0f;
    public Color meshColor = Color.red;
    public Mesh mesh;
    [SerializeField]
    protected float distance = 0f;
    public float Distance { get => distance; set => distance = value; }

    [Header("----- Owner ----- ")]
    public GameObject Owner;


    #endregion
    [Header("----- DrawGizmo ----- ")]
    public bool IsDrawGizmo = false;

    [Header("----- LayerMask ----- ")]
    public LayerMask Scanlayers;

    public virtual bool IsInSight(Transform enemy)
    {
        return false;
    }
    public void CreateMesh()
    {
        mesh = CreateWedgeMesh();
    }
    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();
        int segments = 10;
        int numTriangles = (segments * 4) + 4;
        int numVertices = numTriangles * 3;
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;
            // top 
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
            // bottom 
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;

        }


        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;

    }
    public virtual void OnDrawGizmos()
    {
        if (!IsDrawGizmo) return;

        if (mesh != null && Owner != null)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, Owner.transform.position, Owner.transform.rotation);
        }


    }
}

[System.Serializable]
public class DataView : DataViewBase
{

    public LayerMask occlusionlayers;

    [Header("InsideObject")]
    public bool InsideObject = true;

    public DataView()
    { }

    public override bool IsInSight(Transform AimOffset)
    {
        if (AimOffset == null) return false;



        Vector3 origin = Owner.transform.position;
        Vector3 dest = AimOffset.position;
        Vector3 direcction = dest - origin;


        if (direcction.magnitude > distance)
            return false;

        if (dest.y < -(height + Owner.transform.position.y) || dest.y > (height + Owner.transform.position.y))
        {
            return false;
        }

        direcction.y = 0;

        float deltaAngle = Vector3.Angle(direcction.normalized, Owner.transform.forward);

        if (deltaAngle > angle)
        {
            return false;
        }

        if (Physics.Linecast(origin, dest, occlusionlayers) && InsideObject)
        {
            return false;
        }


        return true;
    }
    public override void OnDrawGizmos()
    {
        if (!IsDrawGizmo) return;

        base.OnDrawGizmos();


    }
}

public class SensorView : MonoBehaviour
{

    public DataView mainDataView = new DataView();

    public float rateTime;
    private float frameRate = 0;


    public Transform enemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (frameRate > rateTime)
        {
            frameRate = 0;
            scan();
        }
        frameRate += Time.deltaTime;

        if (enemy != null)
        {
            mainDataView.meshColor = Color.red;
        }
        else
        {
            mainDataView.meshColor = Color.green;
        }
        mainDataView.meshColor.a = 0.4f;
    }

    public void scan()
    {


        Collider[] array = Physics.OverlapSphere(transform.position, mainDataView.Distance, mainDataView.Scanlayers);
        float distancia = 1000000;
        enemy = null;
        foreach (var item in array)
        {
            if (mainDataView.IsInSight(item.transform))
            {
                float D = (transform.position - item.transform.position).magnitude;

                if (distancia > D)
                {
                    distancia = D;
                    enemy = item.transform;
                }

            }

        }
    }
    private void OnValidate()
    {
        mainDataView.CreateMesh();
    }

    void OnDrawGizmos()
    {
        mainDataView.OnDrawGizmos();

    }
}












