using UnityEngine;

public class HandControl : MonoBehaviour
{
    [SerializeField] GameObject dog;
    [SerializeField] GloveController gloveController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
            gloveController.PlayHapticFeedback();
            Debug.Log("Hand touching dog");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Hand leaving sth");
        if(collider.gameObject.CompareTag("dog"))
        {
            gloveController.StopHapticFeedback();
            Debug.Log("Hand leaving dog");
        }
    }
}
