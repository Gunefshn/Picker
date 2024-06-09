using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingArm : MonoBehaviour
{
    bool Rotate;
    [SerializeField] private float RotateDeg;
    public void StartRotate()
    {
        Rotate = true;
    }
    private void Update()
    {   
        if (Rotate)
        {
            transform.Rotate(0, 0, RotateDeg, Space.Self);
        }
    }
}
