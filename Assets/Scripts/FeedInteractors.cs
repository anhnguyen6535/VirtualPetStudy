using UnityEngine;
using System.Collections;


/*
    This script is attached to Snap Socket > SnapSocketTitle > SimpleSocket
*/
public class FeedInteractors : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GameObject bowl;
    [SerializeField] GameObject bone;
    [SerializeField] Transform attachPoint;
    [SerializeField] AudioSource pantingAudio;
    private SequenceHandler sequenceHandler;
    private PickUp pickupScript;
    private int firstTime = 0;
    private int firstTimeBone = 0;
    private AudioSource audioSource;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sequenceHandler = dog.GetComponent<SequenceHandler>();
        pickupScript = dog.GetComponent<PickUp>();
        animator = dog.GetComponent<Animator>();
        audioSource = dog.GetComponent<AudioSource>();
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
                ReplayPanting();
                // prompt petting
                firstTimeBone = 1;
                StartCoroutine(WaitABitBeforePrompt());
            }else if(firstTimeBone == 2){
                Debug.Log("Second time bone");
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"Entered trigger at {Time.time}");
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log($"Exited trigger at {Time.time} pos: {bowl.transform.position}");
        if(other.gameObject == bowl){
            if(firstTime == 1){
                Debug.Log("take away bowl");
                animator.SetBool("eating", false);
                pantingAudio.Stop();
                firstTime = 2;
                StartCoroutine(StopAttack());
            }
        }
        if(other.gameObject == bone){
            if(firstTimeBone == 1){
                Debug.Log("picked up bone");
                firstTimeBone = 2;
            }
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
    }

    IEnumerator WaitABitBeforePrompt(){

        yield return new WaitForSeconds(3);

        // prompt petting
        sequenceHandler.PromptPetting();
    }

    public void ReplayPanting(){
        if(!pantingAudio.isPlaying){
            pantingAudio.Play();
        }
    }


}
