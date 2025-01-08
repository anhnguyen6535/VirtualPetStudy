
To implement the system described in the [detailed scenario](./detailed_scenario.md), I created a `SequenceHandler` script. Below is an explanation of the `currentStateIndex` variable.

## Variables

```csharp
public int currentStateIndex = 0;
```

This variable indicates the current state of the system:
- 0: Welcome & wait for 1st petting
    - Once the user pet for 5 seconds, `currentStateIndex` is incremented to **1** (controlled by the `HandControl` script).
- 1: Prompt to fetch ball & wait for:
    - User to fetch the ball
    - Dog to return the ball
    - User to pet the dog for 5 seconds
    - Once petting occurs, currentStateIndex increases to **2**.
- 2: Dog gets tired and lies down:
    - A bowl appears, prompting the user to place it in front of the dog
    - Once the bowl is placed, the dog returns to a sitting position and waits for petting
    - After 5 seconds of petting, currentStateIndex increases to **3**.
- 3: Dog starts eating
    - After 5 seconds, prompt the user to take the bowl away
    - Once the bowl is taken, the dog shows aggression
    - After 5 seconds, `currentStateIndex` increases to 4 (controlled by the `FeedInteraction` script).
- 4: Prompt user to put a bone in front of the dog
    - After the bone is placed, the dog returns to a sitting position and waits for petting
    - After 5 seconds of petting, `currentStateIndex` increases to **5**.
- 5: Dog picks up the bone and plays with it
    - Once the dog puts the bone down and returns to a sitting position, it waits for petting again
    - After 5 seconds of petting, `currentStateIndex` increases to **6**.
- 6: End of interaction
    - The user is prompted to remove the headset.

```
private void PerformeCurrentState(){
        if(currentStateIndex == 1){
            fetchingUI.SetActive(true);
        }else if(currentStateIndex == 2){
            dogAnimator.SetBool("sleep", true);
            bowl.SetActive(true);
            StartCoroutine(PromptFeeding());
        }else if(currentStateIndex == 3){
            dogAnimator.SetBool("eating", true);
            StartCoroutine(PromptTakeAway());
        }else if(currentStateIndex == 4){
            boneUI.SetActive(true);
        }else if(currentStateIndex == 5){
            dogAnimator.SetBool("bone", true);
            StartCoroutine(PickupBone());
        }else if(currentStateIndex == 6){
            Debug.Log("END");
            finishUI.SetActive(true);
        }
}
```