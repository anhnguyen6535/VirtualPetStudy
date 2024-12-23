using UnityEngine;

public class HandControl : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GloveController gloveController;
    // [SerializeField] GameObject uiPrompt;
    [SerializeField] SequenceHandler sequenceHandler;
    private Animator animator;
    private float startTime = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = dog.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Hand touching sth");
        if(collider.gameObject.CompareTag("dog"))
        {
            // gloveController.PlayHapticFeedback();
            Debug.Log("Hand touching dog");
            startTime = Time.time;  
        }
    }

    void OnTriggerStay(Collider collider)
    {   
        if(collider.gameObject.CompareTag("dog"))
        {
            // gloveController.PlayHapticFeedback();
            gloveController.PlayHapticFeedback();
            Debug.Log("Hand stay touching dog");
            float elapsedTime = Time.time - startTime;
            if (elapsedTime > 2f)
            {
                Debug.Log("User has petted dog for more than 5 seconds!");
                // uiPrompt.SetActive(true);
                animator.SetBool("idle", true);

                if(sequenceHandler.GetIsWaitingForPetting()){
                    sequenceHandler.IncrementStateIndex();
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Hand leaving sth");
        if(collider.gameObject.CompareTag("dog"))
        {
            startTime = 0;
            gloveController.StopHapticFeedback();
            Debug.Log("Hand leaving dog");
            animator.SetBool("idle", false);
        }
    }
}
