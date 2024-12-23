using UnityEngine;
using Bhaptics.SDK2;
// namespace Bhaptics.SDK2
public class GloveController : MonoBehaviour
{
    public bool clicked = false;

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
        BhapticsLibrary.Play("touch_right");
        Debug.Log("touch_right");
        // Debug.Log("haptic played");
    }

    public void StopHapticFeedback(){
        Debug.Log("Should stop");
        // BhapticsManager.Stop("touch");

    }
}
