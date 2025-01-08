using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


/*
    This script is attached to Beagle_c1
*/
public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject startPos;
    [SerializeField] AttachToMouth attachToMouth;
    [SerializeField] GameObject rethrowUI;
    public bool ballLanded = false;
    public bool backToStartPos = false;
    public float speed = 0.1f;
    public float rotationSpeed = 0.2f;
    public bool happy = false;
    public bool sleep = false;
    private SequenceHandler sequenceHandler;
    private XRGrabInteractable grabInteractable;
    private Rigidbody targetRigidbody;
    public float stopDistance = 1.5f;
    private float velocityThreshold = 0.1f;
    private Animator animator;
    private bool awaitPetting = false;
    private bool firstTime = true;
    private AudioSource audioSource;
    private Transform SpawnPos;
    private float checkInterval = 0.01f; // How often to check the velocity

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sequenceHandler = GetComponent<SequenceHandler>();
        SpawnPos = GameObject.Find("SpawnPos").transform;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        grabInteractable = ball.GetComponent<XRGrabInteractable>();
        targetRigidbody = ball.GetComponent<Rigidbody>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnThrow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(firstTime && ballLanded){
            MoveToTarget(ball);
        }
        else if(backToStartPos){
            MoveToTarget(startPos);
            if(Vector3.Distance(transform.position, startPos.transform.position) < 0.05f){
                Debug.Log("arrived at start pos");
                animator.SetBool("pickup", false);
                backToStartPos = false;
                RotateToDirection(startPos);
            }
        }
        else if(happy){
            Debug.Log("trigger happy");
            HappyInteraction();
            happy = false;
        }
        else if(sleep){
            SleepInteraction();
            sleep = false;
        }   
    }

    void OnThrow(SelectExitEventArgs args)
    {
        Debug.Log("Ball thrown");

        StartCoroutine(WaitForBallToStop());
    }


    // Remove OnThrow Listener once ball is destroyed
    void OnDestroy(){
        if(grabInteractable != null){
            grabInteractable.selectExited.RemoveListener(OnThrow);
            Debug.Log("Listener removed from grabInteractable.");
        }
    }

    IEnumerator WaitForBallToStop(){
        float timeout = 2f; // Max time to wait
        float elapsedTime = 0f;
        
        while(targetRigidbody.linearVelocity.magnitude > 0.1f && elapsedTime < timeout){
            yield return new WaitForSeconds(checkInterval);
            elapsedTime += checkInterval;
        }

        Debug.Log("Ball has stopped!");
        Vector3 finalPosition = ball.transform.position;
        Debug.Log("Final position: " + finalPosition);
        float distance = Vector3.Distance(ball.transform.position, startPos.transform.position);
        Debug.Log("Distance: " + distance);
        if (distance < 1.5){
            Debug.Log("Distance less than 1m");
            targetRigidbody.isKinematic = true;
            ball.transform.position = SpawnPos.position;
            yield return new WaitForFixedUpdate();
            targetRigidbody.isKinematic = false;
            
            // throw too close, prompt to do it again
            if(ball.activeSelf){
                rethrowUI.SetActive(true);
            }
        }else{
            Debug.Log("Distance more than 2m");
            ballLanded = true;
            // StartCoroutine(triggerPickup());
        }
    }

    // IEnumerator triggerPickup(){
    //     yield return new WaitForSeconds(1);
    //     ballLanded = true;
    // }

    void OnTriggerEnter(Collider collider){
        Debug.Log("sth touch dog");
        if(collider.gameObject.CompareTag("ball") && ballLanded){
            Debug.Log("ball is within reach");
            ballLanded = false;

            // stop ball motion
            targetRigidbody.linearVelocity = Vector3.zero;
            targetRigidbody.angularVelocity = Vector3.zero;

            // start pickup animation
            animator.SetBool("run", false);
            animator.SetBool("pickup", true);
        }
    }

    void MoveToTarget(GameObject destination){
        animator.SetBool("run", true);
        transform.LookAt(destination.transform, Vector3.up);
    }

    void RotateToDirection(GameObject destination){
        if (destination != null){
            Debug.Log("start rotating...");
            Vector3 targetDirection = destination.transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, rotationSpeed);

            animator.SetBool("putdown", true);
            animator.SetBool("run", false);
            firstTime = false;
            StartCoroutine(WaitForPutdownFinish());
        }
    }

    public void SetBackToStartPos(){
        if(!backToStartPos){
            backToStartPos = true;

        }
    }

    void DetachBallFromDog(){
        Debug.Log($"Detaching from dog mouth...{Time.time}");
        if(sequenceHandler.GetCurrentStateIndex() < 5){
            attachToMouth.DetachBallFromMouth();
            ball.SetActive(false);
            // Destroy(ball);
        }else{
            Debug.Log("Detaching BONE from dog mouth...");
            attachToMouth.AttachBoneToSocket();
        }

    }

    public void HappyInteraction(){
        animator.SetBool("happy", true);
        StartCoroutine(StopHappy());
    }

    IEnumerator StopHappy(){
        yield return new WaitForSeconds(2f);
        animator.SetBool("happy", false);

        // trigger petting prompt
        sequenceHandler.PromptPetting();

    }

    public void SleepInteraction(){
        animator.SetBool("sleep", true);
        // StartCoroutine(StopSleepy());
    }

    IEnumerator StopSleepy(){
        yield return new WaitForSeconds(5);
        animator.SetBool("sleep", false);

    }

    IEnumerator WaitForPutdownFinish(){
        // Get the AnimatorStateInfo of the current animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Wait until the animation matches the name and is not complete
        while (!stateInfo.IsName("Put_down") || stateInfo.normalizedTime < 1.0f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null; // Wait for the next frame
        }

        Debug.Log("Animation finished!");
        animator.SetBool("putdown", false);

        HappyInteraction();
    }

    public void PromptPetting(){
        sequenceHandler.PromptPetting();
    }

    public void Barking(){
        if(!audioSource.isPlaying){
            Debug.Log("Play Barking");
            audioSource.Play();
        }
    }
}
