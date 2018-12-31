using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BonnieIEnermyState
{
    void Execute();
    void Enter(Bonnie enermy);
    void Exit();
    void OnTriggerEnter(Collider2D orther);
}
