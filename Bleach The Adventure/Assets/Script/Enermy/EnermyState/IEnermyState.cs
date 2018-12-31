using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnermyState 
{
    void Execute();
    void Enter(GrandFisher enermy);
    void Exit();
    void OnTriggerEnter(Collider2D orther);
}
