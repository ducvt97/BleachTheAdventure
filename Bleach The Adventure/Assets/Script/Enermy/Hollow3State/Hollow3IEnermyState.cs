using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hollow3IEnermyState
{
    void Execute();
    void Enter(Hollow3 enermy);
    void Exit();
    void OnTriggerEnter(Collider2D orther);
}
