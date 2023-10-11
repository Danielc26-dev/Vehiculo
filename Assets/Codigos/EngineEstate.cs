using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeState { Patroll, Follow, MoveToPosition }

public class EngineEstate : MonoBehaviour
{
    public State[] States;
    public State CurrentState;
    public TypeState InitTypeState;
    public void activateState(TypeState type)
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        foreach (var item in States)
        {
            if (((State)item).type == type)
            {
                ((State)item).enabled = true;
                ((State)item).Enter();
                CurrentState = ((State)item);
            }
            else
            {
                ((State)item).enabled = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        States = GetComponents<State>();
        activateState(InitTypeState);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.Execute();
    }
}
