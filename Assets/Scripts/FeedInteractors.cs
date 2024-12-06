using UnityEngine;
using System.Collections;

public class FeedInteractors : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("bowl")){
            Debug.Log("bowl in");
            // animator.SetBool("idle", false);
            animator.SetBool("eating", true);
            // StartCoroutine(StartEating());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("bowl")){
            Debug.Log("take away bowl");
            animator.SetBool("eating", false);
            // StartCoroutine(StartEating());
        }
    }

    IEnumerator StartEating(){
        yield return new WaitForSeconds(1);
        animator.SetBool("eating", true);
    }
}
