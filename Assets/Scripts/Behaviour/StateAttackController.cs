using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackController : MonoBehaviour
{
    public delegate void OnStartStateAttackController();
    public OnStartStateAttackController startStateAttackControllerHandler;

    public delegate void OnEndStateAttackController();
    public OnEndStateAttackController endStateAttackControllerHandler;

    public bool GetFlagStateAttackController
    {
        get; private set;
    }

    private void Start()
    {
        startStateAttackControllerHandler = new OnStartStateAttackController(StateAttackControllerStart);
        endStateAttackControllerHandler = new OnEndStateAttackController(StateAttackControllerEnd);
    }

    private void StateAttackControllerStart()
    {
        Debug.Log("Attack Start!");
    }
    private void StateAttackControllerEnd()
    {
        Debug.Log("Attack End!");
    }
    public void EventStateAttackStart()
    {
        GetFlagStateAttackController = true;
        if (startStateAttackControllerHandler != null)
            startStateAttackControllerHandler();
    }
    public void EventStateAttackEnd()
    {
        GetFlagStateAttackController = false;
        if (endStateAttackControllerHandler != null)
            endStateAttackControllerHandler();
    }
    public void OnCheckAttackCollider(int attackIdx)
    {
        Debug.Log("=========== Attack Index : " + attackIdx);
        GetComponent<IAttackAble>()?.OnExeculteAttack();
    }
}
