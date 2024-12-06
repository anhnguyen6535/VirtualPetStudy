using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FetchInteractor : MonoBehaviour
{
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(OnThrow);
        }
    }

    void OnThrow(SelectExitEventArgs args)
    {
        Debug.Log("Ball thrown");
    }
    
}
