using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reporter : MonoBehaviour
{
   [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorderObject"))
        {
            gameManager.BorderEntered();
        }
    }
}
