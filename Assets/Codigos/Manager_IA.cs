using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_IA : MonoBehaviour
{
    public List<MoveToPointState> vehicles = new List<MoveToPointState>();
    public LayerMask layerFloor;
    public Transform cursor;
    Vector3 Target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 10000, layerFloor))
        //    {
        //        if (hit.point != Target)
        //        {
        //            Target = hit.point;
        //            cursor.position = Target;
        //            foreach (var item in vehicles)
        //            {
        //                //((MoveToPointState)item).Target = Target;
        //            }
        //        }

        //    }

        //}

    }
}
