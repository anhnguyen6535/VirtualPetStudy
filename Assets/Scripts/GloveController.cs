using UnityEngine;
using Bhaptics.SDK2;
// namespace Bhaptics.SDK2
public class GloveController : MonoBehaviour
{
    public bool clicked = false;
    public bool rightHand = true;   // change to false if left hand dominance
    [SerializeField] SequenceHandler sequenceHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked){
            BhapticsLibrary.Play("game_start");
            clicked = false;
        }
    }

    public void PlayHapticFeedback(){
        if (sequenceHandler.GetIsWaitingForPetting()){
            // Right hand dominance
            if(rightHand){
                BhapticsLibrary.Play("touch_right");
                Debug.Log("touch_right");
            }
            // Left hand dominance
            else{
                BhapticsLibrary.Play("touch_left");
                Debug.Log("touch_left");
            }
        }
    }

    public void StopHapticFeedback(){
        Debug.Log("Should stop");
        BhapticsLibrary.StopAll();
        // BhapticsManager.Stop("touch");

    }
}
