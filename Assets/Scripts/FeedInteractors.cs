using UnityEngine;
using System.Collections;

public class FeedInteractors : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PickUp pickupScript;
    [SerializeField] GameObject bowl;
    [SerializeField] GameObject bone;
    [SerializeField] Transform attachPoint;
    [SerializeField] SequenceHandler sequenceHandler;
    // private int timeCount = 1;
    private int firstTime = 0;
    private int firstTimeBone = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bowl.transform.position == attachPoint.position && firstTime == 0){
            // back to sitting 
            animator.SetBool("sleep", false);

            StartCoroutine(WaitABitBeforePrompt());
            firstTime = 1;
        }
        if(bone.transform.position == attachPoint.position){
            Debug.Log($"First time bone {firstTimeBone}");
            if(firstTimeBone == 0){
                // back to sitting 
                animator.SetBool("idle", true);

                // prompt petting
                firstTimeBone = 1;
                StartCoroutine(WaitABitBeforePrompt());
            }else if(firstTimeBone == 2){
                Debug.Log("Second time bone");
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        // if(other.gameObject.CompareTag("bowl")){
        Debug.Log($"Entered trigger at {Time.time}");
        // if(other.gameObject == bowl ){
        //     // if(timeCount == 1){
        //     if(firstTime){
        //         bowl.transform.position = transform.position;
        //         // animator.SetBool("idle", false);
        //         animator.SetBool("eating", true);
        //         Debug.Log("bowl in");
        //     }else{
        //         animator.SetBool("idle", true);

        //     }
        // }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log($"Exited trigger at {Time.time} pos: {bowl.transform.position}");
        if(other.gameObject == bowl){
            if(firstTime == 1){
                Debug.Log("take away bowl");
                animator.SetBool("eating", false);
                firstTime = 2;
                StartCoroutine(StopAttack());
            }
            // timeCount = 2;
            // StartCoroutine(StartEating());
        }
        if(other.gameObject == bone){
            if(firstTimeBone == 1){
                Debug.Log("picked up bone");
                firstTimeBone = 2;
                // StartCoroutine(StopAttack());
            }
            // timeCount = 2;
            // StartCoroutine(StartEating());
        }
    }

    IEnumerator StartEating(){
        yield return new WaitForSeconds(7);
        animator.SetBool("eating", true);
    }

    // DEMO only
    IEnumerator StopAttack(){
        yield return new WaitForSeconds(5);

        // prompt
        sequenceHandler.SetStateIndex(4);
        // animator.SetBool("idle", true);
    }

    IEnumerator WaitABitBeforePrompt(){

        yield return new WaitForSeconds(3);

        // prompt petting
        sequenceHandler.PromptPetting();
    }
}
