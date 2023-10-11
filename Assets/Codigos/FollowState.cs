using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : MoveState
{   

    // Start is called before the first frame update
    void Start()
    {
        LoadComponent();
    }

    public override void LoadComponent()
    {
        base.LoadComponent();
    }

    public override void Execute()
    {
        if (IaEye.enemy != null)
        {
            base.MoveToPosition(IaEye.enemy.position);
        }
        else
        {
            _EngineEstate.activateState(TypeState.Patroll);
            return;
        }
    }
}
