using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using static UnityEditor.Progress;

[Serializable]
public class BallTechnicalProcess
{
    public Animator BallElevator;
    public TextMeshProUGUI CountText;
    public int TargetBallCount;
    public GameObject[] Balls;

}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PickerObject;
    [SerializeField] private GameObject[] PickerPalets;
    [SerializeField] private GameObject[] BonusBalls;
    bool PaletsAvailable;
    [SerializeField] private GameObject BallControlObject;
    public bool PickerMoveState;
    int BasketBallCount;
    int CheckPointCount;
    int CurrentCheckPointIndex;

    
    [SerializeField] private List<BallTechnicalProcess> _BallTechnicalProcess = new List<BallTechnicalProcess>();



    void Start()
    {
        PickerMoveState = true;
        for (int i = 0; i < _BallTechnicalProcess.Count; i++)
        {
            _BallTechnicalProcess[i].CountText.text = BasketBallCount + " / " + _BallTechnicalProcess[i].TargetBallCount;
        }
        CheckPointCount = _BallTechnicalProcess.Count-1;
    }
    void Update()
    {
        if(PickerMoveState)
        {
            PickerObject.transform.position += 5f * Time.deltaTime * PickerObject.transform.forward;
            if(Time.timeScale!=0)
            {
                if(Input.GetKey(KeyCode.A))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3(PickerObject.transform.position.x - 1f, PickerObject.transform.position.y, PickerObject.transform.position.z), .05f);
                }
                if(Input.GetKey(KeyCode.D))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3(PickerObject.transform.position.x + 1f, PickerObject.transform.position.y, PickerObject.transform.position.z), .05f);
                }
            }
        }
    }
    public void BorderEntered() //sýnýra gelindi mi 
    {
        if (PaletsAvailable)
        {
            PickerPalets[0].SetActive(false);
            PickerPalets[1].SetActive(false);
        }
        PickerMoveState=false; // toplayýcýnýn hareketi durdurulmalý.  // Debug.Log("geldi");
        Invoke("StageControl", 2f);
        Collider[] HitColl = Physics.OverlapBox(BallControlObject.transform.position,BallControlObject.transform.localScale/2,Quaternion.identity);
        int i = 0;
        while(i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,.8f),ForceMode.Impulse);
            i++;
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(BallControlObject.transform.position,BallControlObject.transform.localScale);
    }
    */
    public void CountBall()
    {
        BasketBallCount++;
        _BallTechnicalProcess[CurrentCheckPointIndex].CountText.text = BasketBallCount + " / " + _BallTechnicalProcess[CurrentCheckPointIndex].TargetBallCount;
    }
    void StageControl()
    {
        if(BasketBallCount >= _BallTechnicalProcess[CurrentCheckPointIndex].TargetBallCount)
        { 
            _BallTechnicalProcess[CurrentCheckPointIndex    ].BallElevator.Play("Asansor");
            foreach (var item in _BallTechnicalProcess[CurrentCheckPointIndex].Balls)
            {
                item.SetActive(false);
            }

            if(CurrentCheckPointIndex == CheckPointCount)
            {
                Debug.Log("GAME OVER"); //kazandýn paneli
                Time.timeScale = 0;
            }
            else
            {
                CurrentCheckPointIndex++;
                BasketBallCount = 0;
                if (PaletsAvailable)
                {
                    PickerPalets[0].SetActive(true);
                    PickerPalets[1].SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("lose");
            //Losepanel aktif edilecek
        }
    }
    public void CreatePalets()
    {
        PaletsAvailable = true;
        PickerPalets[0].SetActive(true);
        PickerPalets[1].SetActive(true);

    }
    public void CreateBonusBall(int BonusBallIndex)
    {
        BonusBalls[BonusBallIndex].SetActive(true);
    }
}
