using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointState : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private Animator BarrierArea;

    public void RaiseBarrier()
    {
        BarrierArea.Play("RaiseBarrier");
    }
    public void Done()
    {
        _GameManager.PickerMoveState = true;
    }
}

