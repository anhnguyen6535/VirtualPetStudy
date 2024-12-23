using UnityEngine;

public class DogController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartPetting(){
        Debug.Log("Petting started");
    }

    private void CompletePetting(){
        Debug.Log("Petting completed");
    }

    public void StartFetching(){
        Debug.Log("Fetching completed");
    }

    private void CompleteFetching(){
        Debug.Log("Fetching completed");
    }

    public void StartFeeding(){
        Debug.Log("Feeding completed");
    }

    private void CompleteFeeding(){
        Debug.Log("Feeding completed");
    }

    public void ShowAggression()
    {
        Debug.Log("Dog showing aggression...");
    }

    private void StopAggression()
    {
        Debug.Log("Aggression stopped.");
    }

    public void PlayWithBone()
    {
        Debug.Log("Playing with bone...");
    }

    private void CompletePlaying()
    {
        Debug.Log("Playing completed.");
    }
}
