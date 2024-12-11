using UnityEngine;
using System.Collections;

public class FeedInteractors : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PickUp pickupScript;
    [SerializeField] GameObject bowl;
    [SerializeField] Transform attachPoint;
    // private int timeCount = 1;
    private int firstTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bowl.transform.position == attachPoint.position && firstTime == 0){
            animator.SetBool("sleep", false);
            animator.SetBool("eating", true);
            firstTime = 1;
        }
        // else if(bowl.transform.position == transform.position && firstTime == 2){
        //     animator.SetBool("eating", false);
        // }
        
    }

    private void OnTriggerEnter(Collider other) {
        // if(other.gameObject.CompareTag("bowl")){
        Debug.Log($"Entered trigger at {Time.time} pos: {bowl.transform.position}");
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
            }
            // timeCount = 2;
            // StartCoroutine(StartEating());
        }
    }

    IEnumerator StartEating(){
        yield return new WaitForSeconds(1);
        animator.SetBool("eating", true);
    }
}
