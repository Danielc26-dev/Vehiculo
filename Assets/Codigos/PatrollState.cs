using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollState : MoveState
{   
    private Vector3 newPosition;

    float FrameRate;
    public float Rate;
    public float Radius;

    public void Start()
    {
        base.LoadComponent();
    }

    private void Update()
    {
        
        //myAmong.Seek(myPath.nextPoint(myAmong.transform.position));
    }

    public void CalcularNewPosicion()
    {
        newPosition = transform.position + Random.insideUnitSphere * Radius;
        newPosition.y = transform.position.y;
    }

    public override void Enter() { }
    public override void Execute()
    {
        if(IaEye.enemy != null)
        {
            _EngineEstate.activateState(TypeState.Follow);
            return;
        }
        else
        {

            if (FrameRate > Rate)
            {
                FrameRate = 0;

                CalcularNewPosicion();
            }
            FrameRate += Time.deltaTime;

            base.MoveToPosition(newPosition);
        }

    }
    public override void Exit() { }
}
