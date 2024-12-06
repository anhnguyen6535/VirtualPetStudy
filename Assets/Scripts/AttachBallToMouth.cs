using UnityEngine;
using System.Collections;

public class AttachBallToMouth : MonoBehaviour
{
    public bool touchMouth = false;
    [SerializeField] Transform mouthAttach;
    [SerializeField] PickUp dog;

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
        if(collider.gameObject.CompareTag("mouthAttach") && !touchMouth)
        {
            Debug.Log("Mouth touch");
            touchMouth = true;
            StartCoroutine(Comeback());
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
}
