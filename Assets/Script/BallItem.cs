using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallItem : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private string ItemType;
    [SerializeField] private int BonusBallIndex;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorderObject"))
        {   
            if(ItemType == "Palet")
            {
                gameManager.CreatePalets();
                gameObject.SetActive(false);
            }
            else if(ItemType == "BonusBall")
            {
                gameManager.CreateBonusBall(BonusBallIndex);
                gameObject.SetActive(false);
            }
        }
    }
}
