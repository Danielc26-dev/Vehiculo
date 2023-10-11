using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPath : MonoBehaviour
{

    public List<Transform> pathList = new List<Transform>();

    private int index = 0;

    public float distancePoint;


    public bool drawGizmos = false;

    public Color colorGizmos;


    public Vector3 getCurrentPoint()
    {
        if (index >= 0 && index < pathList.Count)
        {
            return pathList[index].position;
        }
        return Vector3.zero;
    }

    public Vector3 getNextPoint()
    {
        index++;
        index = index % pathList.Count;
        return pathList[index].position;
    }
    public Vector3 getBackPoint()
    {
        index--;
        index = index % pathList.Count;
        return pathList[index].position;
    }

    public Vector3 nextPoint(Vector3 target)
    {
        float distancia = (target - getCurrentPoint()).magnitude;

        if (distancia < distancePoint)
        {
            return getNextPoint();
        }
        return getCurrentPoint();
    }

    void OnDrawGizmos()
    {
        if (drawGizmos == false)
        {
            return;
        }
        Gizmos.color = colorGizmos;

        for (int i = 0; i < pathList.Count; i++)
        {
            if (i < pathList.Count - 2)
            {
                Gizmos.DrawLine(pathList[i].position, pathList[i + 1].position);
            }
            //else
            //{

            //}
            Gizmos.DrawSphere(pathList[i].position, 0.2f);
        }

    }
}
