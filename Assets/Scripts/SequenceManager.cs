using UnityEngine;

public class SequenceManager : MonoBehaviour
{

    public GameObject dog;
    public UIManager uiManager;
    public DogController dogController;
    private int currentInteractionIndex = 0;

    [System.Serializable]
    public class Interaction{
        public string action;
        public float waitTime;
    }

    public Interaction[] interactions;

    public void StartInteraction(int index){
        if (index >= interactions.Length)
        {
            Debug.Log("All interactions completed.");
            return;
        }

        Interaction interaction = interactions[index];
        uiManager.ShowPrompt(index);
        HandleAction(interaction);
    }

    private void HandleAction(Interaction interaction){
        switch(interaction.action){
            case "PetDog":
                dogController.StartPetting();
                break;
            case "FetchBall":
                dogController.StartFetching();
                break;
            case "FeedDog":
                dogController.StartFeeding();
                break;
            case "Agression":
                dogController.ShowAggression();
                break;
            case "PlayBone":
                dogController.PlayWithBone();
                break;
        }

        currentInteractionIndex++;
    }

    public void CompleteCurrentInteraction(){
        StartInteraction(currentInteractionIndex);
    }
}
