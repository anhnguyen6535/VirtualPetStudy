
# Detailed interactions of this project

### User enters scene: 

- Setup: 
    - Home-like environment.
    - Dog breed: Beagle.
    - Dog's initial state: Sitting with a neutral emotion.
- Prompt 0: "You've just welcomed a new virtual pet!"
- Prompt 1: "Pet the dog with both hands."
- User Action:
    - User pets the dog for xx seconds.

### Interaction 1: Fetching ball

- Prompt 2: "Grab the ball and **throw** it."
- Actions:
    - User grabs and throws the ball.
    - The virtual dog turns, runs after it, picks it up, and brings it back to the user.
    - The dog shows a happy emotion (**jumping** then turn around).
- Prompt 3: "Pet the dog with both hands."
- User Action:
    - User pets the dog for xx seconds.

### Interaction 2: Feeding dog

- Dog Action:
    - The dog gets tired and lies down.
- Prompt 4: "The dog is hungry, grab the bowl and feed the dog!"
- User Action:
    - User grabs the bowl and places it in the **highlighted** area.
- Dog Action:
    - Back to sitting position
- Prompt 5: "Pet the dog with both hands."
- User Action:
    - User pets the dog for xx seconds.

### Interaction 3: Aggression
- The dog starts eating.
- Prompt 7: "Take the food away!"
- Action:
    - User grabs the food.
- The dog shows aggression (barking, growling, etc.).

### Interaction 4: Playing with bone
- Prompt 8: “Give him the bone”
- Action: 
    - User grabs the bone and **put** it in front of the dog.
- Dog:
    - The dog returns to a sitting position.
- Prompt 9: "Pet the dog with both hands."
- Action:
    - User pets the dog for xx seconds.
- Dog:
    - Pick up the bone and start playing for xx seconds
    - Put down, back to sitting position
- Prompt 10: “Pet the dog with both hands.”
- Action:
    - User pets the dog for xx seconds.