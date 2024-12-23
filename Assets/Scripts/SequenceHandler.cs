using UnityEngine;
using System.Collections;

public class SequenceHandler : MonoBehaviour
{

    /**
    *   0: welcome & wait for 1st petting
    *   1: wait for fetching ball -> dog brought the ball back -> waiting for petting -> petted 
    *   3: dog lies down & wait for bowl in place
    *   4: bowl in place => petted => should start eating
    **/
    public int currentStateIndex = 0;   // 0: start state
    public bool waitingForPetting = false;
    [SerializeField] GameObject fetchingUI;
    [SerializeField] GameObject pettingUI;
    [SerializeField] GameObject feedingUI;
    [SerializeField] GameObject takeAwayUI;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject boneUI;
    [SerializeField] PickUp pickUp;
    [SerializeField] Animator dogAnimator;
    [SerializeField] GameObject bowl;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStateIndex(int num){
        Debug.Log(num);
        currentStateIndex = num;
        PerformeCurrentState();
    }

    public void IncrementStateIndex(){
        currentStateIndex += 1;
        Debug.Log($"Current state: {currentStateIndex}");
        if (waitingForPetting){
            waitingForPetting = false;
        }
        PerformeCurrentState();
    }

    private void PerformeCurrentState(){
        if(currentStateIndex == 1){
            fetchingUI.SetActive(true);
        }else if(currentStateIndex == 2){
            dogAnimator.SetBool("sleep", true);
            bowl.SetActive(true);
            StartCoroutine(PromptFeeding());
        }else if(currentStateIndex == 3){
            dogAnimator.SetBool("eating", true);
            StartCoroutine(PromptTakeAway());
        }else if(currentStateIndex == 4){
            boneUI.SetActive(true);
        }else if(currentStateIndex == 5){
            dogAnimator.SetBool("bone", true);
            StartCoroutine(PickupBone());
        }else if(currentStateIndex == 6){
            Debug.Log("END");
            finishUI.SetActive(true);
        }
    }

    public void PromptPetting(){
        pettingUI.SetActive(true);
    }

    IEnumerator PromptFeeding(){
        yield return new WaitForSeconds(2);
        feedingUI.SetActive(true);
    }

    IEnumerator PickupBone(){
        yield return new WaitForSeconds(2);
        dogAnimator.SetBool("bone", false);
        Debug.Log($"BONED {currentStateIndex}");
    }

    IEnumerator PromptTakeAway(){
        yield return new WaitForSeconds(5);
        takeAwayUI.SetActive(true);
    }

    public void SetWaitingForPetting(){
        waitingForPetting = true;
    }

    public bool GetIsWaitingForPetting(){
        return waitingForPetting;
    }

    public int GetCurrentStateIndex(){
        return currentStateIndex;
    }
}
