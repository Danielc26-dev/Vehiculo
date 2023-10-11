using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveState : State
{
    protected NavMeshAgent agent;
    protected SensorView IaEye;
    public override void LoadComponent()
    {
        agent = GetComponent<NavMeshAgent>();
        IaEye = GetComponent<SensorView>();
        base.LoadComponent();
    }
    // Start is called before the first frame update
    public void MoveToPosition(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
   
    //private void OnDrawGizmos()
    //{
    //    if (!DrawGizmo)
    //    {
    //        return;
    //    }

    //    if (vehiculo != null)
    //    {
    //        Gizmos.color = RatioGizmoColor;
    //        Gizmos.DrawWireSphere(transform.position, vehiculo.radio);


    //        Gizmos.color = lineGizmoColor;
    //        Gizmos.DrawLine(transform.position, Target);
    //    }




    //}
}
