# Team 3 - AJSGS

Welcome to the VR Cooking Simulator! Our project is a tutorial for beginner cooks to learn to make vegetable soup. Some of the aspects that it features are interactive elements such as a vegetable slicing mechanic with realistic audio feedback, salt and pepper shakers that pour particles when tilted, and stirring the soup in the pan. This guide will walk you through understanding the controls and getting the most out of your VR cooking experience.

## Gameplay Instructions

### Text Canvases
- **Objective:** Display informative text within the VR environment to guide the user's actions.
- **Mechanics:**
  - Text canvases are placed within the VR scene to provide instructions or feedback, allows for users to proceed at their own pace. We give a sense of autonomy and control to the user by utilizing these canvases.
  - Interactive rays are shown when the user hovers over the continue or proceed buttons so that the user is aware that they can transition to the next canvas by grabbing the respective button.

### Vegetable Slicing
- **Objective:** Slice the vegetables using the knife provided.
- **Mechanics:**
  - Pick up the knife using the grip button on your controller.
  - Move the knife towards the vegetables. When the knife collides with the vegetables, slicing occurs.
  - An audio clip of slicing will play to enhance realism.
  - The vegetables will be sliced in a realistic manner, which you can further interact with.

### Soup Pouring and Stirring
- **Objective:** Provide a realistic experience for the user to actually cook the soup.
- **Mechanics:**
  - Pour the soup from the mixture into the pan or from the pan into the bowl.
  - Grab the stirring spoon to stir the soup and vegetables when they are in the pan.
    
### Salt and Pepper Shakers
- **Objective:** Pour salt and pepper particles from the respective shakers.
- **Mechanics:**
  - Pick up the salt or pepper shaker using the grip button on your controller.
  - Tilt the shaker past a certain angle (65 degrees) to start pouring the particles.
  - The particles will emit from the end of the shaker, simulating the pouring action.

## Controls
- **Movement:** You need to physically walk around to move within the VR environment. Our game mainly relies on the grab functionality for almost all aspects so that the user is accustomed to a mode of interaction with the system.
- **Interact/Grab:**
  - **Grip Button:** Pick up and hold objects (e.g., knife, chopping board, salt shaker, pepper shaker, pan, bowl, stirring spoon etc). This button is predominantly used for all actions within our game, even to select text canvas transitions. The image here shows where the grab button is on the controller: ![Help Center Article Background - 2023-07-12T153655 882](https://github.com/shashank790/cs190-ajsgs-cooking/assets/114948179/522bb61a-5018-44fd-8d34-9bac803bf084)

- **Tilt/Pour:**
  - Tilt the shakers to pour salt or pepper when the tilt angle exceeds 45 degrees.
 
## Known Issues (not enough time to resolve these)
- **Lag when Slicing:** The setup requires some form of optimization to avoid lagging instances within the VR environment. This lag is not experienced in Unity's editor but mainly only when running the game on the VR.
- **Particle Emission:** Particle systems might need fine-tuning for optimal visual effects.
- **Text Canvas Inconsistencies:** If the step by step guidelines aren't followed as provided, their may be issues with how the canvases update. We were unable to resolve this due to time constraint. Users must follow the guidelines provided for the best possible experience.
- **Stirring Spoon Handling:** The Stirring Spoon is grabbed upside down, the hand model is unable to grasp the spoon from the correct side.
