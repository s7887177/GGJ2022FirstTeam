using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void OnEnter();
    public void Update(float deltaTime);
    public void OnExit();
}
