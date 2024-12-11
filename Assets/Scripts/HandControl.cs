using UnityEngine;

public class HandControl : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GloveController gloveController;
    private Animator animator;
    private float timer = 0f;
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
            timer = 0;  
        }
    }

    void OnTriggerStay(Collider collider)
    {   
        if(collider.gameObject.CompareTag("dog"))
        {
            // gloveController.PlayHapticFeedback();
            gloveController.PlayHapticFeedback();
            Debug.Log("Hand stay touching dog");
            timer += Time.deltaTime;
            if (timer > 5f)
            {
                Debug.Log("User has petted dog for more than 5 seconds!");
                animator.SetBool("idle", true);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Hand leaving sth");
        if(collider.gameObject.CompareTag("dog"))
        {
            gloveController.StopHapticFeedback();
            Debug.Log("Hand leaving dog");
            animator.SetBool("idle", false);
        }
    }
}
