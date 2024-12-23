using UnityEngine;
using System.Collections;

public class AttachBallToMouth : MonoBehaviour
{
    public bool touchMouth = false;
    [SerializeField] Transform mouthAttach;
    [SerializeField] PickUp dog;
    [SerializeField] Transform socket;
    [SerializeField] GameObject socketParent;
    [SerializeField] SequenceHandler sequenceHandler;
    public bool isBone = false;
    private bool grabbed = false;

    void Start()
    {
        // targetRigidbody = target.GetComponent<Rigidbody>();  // Get the Rigidbody of the ball
    }

    void Update(){
        if(touchMouth){
            if(mouthAttach != null){
                transform.position = new Vector3(mouthAttach.position.x, mouthAttach.position.y, mouthAttach.position.z);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("mouthAttach") && !touchMouth && !grabbed)
        {
            Debug.Log("Mouth touch");
            touchMouth = true;

            if(isBone){
                socketParent.SetActive(false);
                grabbed = true;
                Debug.Log("Bone touches mouth");
            } 

            if(!isBone) StartCoroutine(Comeback());
        }

        // else if(collider.gameObject.CompareTag("hand")){
        //     Debug.Log("Hand touch");
        //     touchMouth = false;
        // }
    }

    IEnumerator Comeback(){
        yield return new WaitForSeconds(1);
        dog.SetBackToStartPos();
    }

    public void DetachBallFromMouth(){
        touchMouth=false;
    }

    public void AttachBoneToSocket(){
        Debug.Log("attack bone to socket");
        socketParent.SetActive(true);
        touchMouth=false;
        transform.position = socket.position;
        StartCoroutine(PromptPetting());
    }

    IEnumerator PromptPetting(){
        yield return new WaitForSeconds(1);
        sequenceHandler.PromptPetting();
    }
}
