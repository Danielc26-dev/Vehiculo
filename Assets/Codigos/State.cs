    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public TypeState type;
    public EngineEstate _EngineEstate;
    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }

    public virtual void LoadComponent()
    {
        _EngineEstate = GetComponent<EngineEstate>();
    }
}

