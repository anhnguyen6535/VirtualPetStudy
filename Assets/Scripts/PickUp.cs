using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject startPos;
    [SerializeField] Transform mouthPosition;
    public bool ballLanded = false;
    public bool backToStartPos = false;
    public float speed = 0.5f;
    public float rotationSpeed = 0.2f;
    public bool happy = false;
    // public bool sleepy = false;
    private XRGrabInteractable grabInteractable;
    private Rigidbody targetRigidbody;
    public float stopDistance = 1.5f;
    private float velocityThreshold = 0.1f;
    private Animator animator;
    private bool awaitPetting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        grabInteractable = target.GetComponent<XRGrabInteractable>();
        targetRigidbody = target.GetComponent<Rigidbody>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnThrow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ballLanded){
            MoveToTarget(target);
            // var step = speed * Time.deltaTime;
            // transform.LookAt(target.transform, Vector3.up);
            // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
        else if(backToStartPos){
            MoveToTarget(startPos);
            if(Vector3.Distance(transform.position, startPos.transform.position) < 0.1f){
                Debug.Log("arrived at start pos");
                animator.SetBool("pickup", false);
                backToStartPos = false;
                RotateToDirection(startPos);
            }
        }else if(happy){
            HappyInteraction();
        }
        
        // else if(sleepy){
            // SleepInteraction();
        // }
        // if(Vector3.Distance(transform.position, target.transform.position) <= stopDistance && targetRigidbody.linearVelocity.magnitude <= velocityThreshold){
        //     Debug.Log("Stopped");
        //     ballLanded = false;
        // }    
    }

    void OnThrow(SelectExitEventArgs args)
    {
        Debug.Log("Ball thrown");
        StartCoroutine(triggerPickup());
    }

    IEnumerator triggerPickup(){
        yield return new WaitForSeconds(1);
        ballLanded = true;
    }

    void OnTriggerEnter(Collider collider){
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
        var step = speed * Time.deltaTime;
        transform.LookAt(destination.transform, Vector3.up);
        transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, step);
    }

    void RotateToDirection(GameObject destination){
        if (destination != null){
            Debug.Log("start rotating...");
            Vector3 targetDirection = destination.transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, rotationSpeed);

            animator.SetBool("putdown", true);
            animator.SetBool("run", false);
            // StartCoroutine(DetachBall());
        }
    }

    public void SetBackToStartPos(){
        if(!backToStartPos){
            backToStartPos = true;

        }
    }

    IEnumerator DetachBall(){
        yield return new WaitForSeconds(0.2f); 
        target.GetComponent<AttachBallToMouth>().DetachBallFromMouth();
    }

    void DetachBallFromDog(){
        target.GetComponent<AttachBallToMouth>().DetachBallFromMouth();

    }

    void HappyInteraction(){
        animator.SetBool("happy", true);
        StartCoroutine(StopHappy());
    }

    IEnumerator StopHappy(){
        yield return new WaitForSeconds(5);
        animator.SetBool("happy", false);

    }

    // void SleepInteraction(){
    //     animator.SetBool("sleep", true);
    //     StartCoroutine(StopSleepy());
    // }

    // IEnumerator StopSleepy(){
    //     yield return new WaitForSeconds(5);
    //     animator.SetBool("sleep", false);

    // }

    // Detech hand collide
    // void OnTriggerEnter(Collider collider){
    //     if (awaitPetting){
    //         StartCoroutine(CountPetting);
    //     }
    // }
}