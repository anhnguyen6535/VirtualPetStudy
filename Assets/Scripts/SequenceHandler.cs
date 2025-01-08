using UnityEngine;
using System.Collections;

/*
    This script is attached to Beagle_c1
    To get more details about the meaning of currentStateIndex, review Documentation/detailed_scenario.md and Documentation/sequence_handler.md
*/
public class SequenceHandler : MonoBehaviour
{
    public int currentStateIndex = 0;   // 0: start state
    public bool waitingForPetting = false;
    [SerializeField] GameObject fetchingUI;
    [SerializeField] GameObject pettingUI;
    [SerializeField] GameObject feedingUI;
    [SerializeField] GameObject takeAwayUI;
    [SerializeField] GameObject finishUI;
    [SerializeField] GameObject boneUI;
    [SerializeField] GameObject bowl;
    [SerializeField] GameObject socket;
    private PickUp pickUp;
    private Animator dogAnimator;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickUp = GetComponent<PickUp>();
        dogAnimator = GetComponent<Animator>();
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
            // bowl.SetActive(true);
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
        socket.SetActive(true);
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
