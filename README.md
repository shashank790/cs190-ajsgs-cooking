# Team_AJSGS_Scene (HW3):
To move around in the scene, we have decided to use teleportation. You can move the left and right controllers around and point at the different mats on the floor to teleport. 
<img width="1420" alt="Screenshot 2024-06-03 at 9 25 32 PM" src="https://github.com/shashank790/cs190-ajsgs-cooking/assets/114948179/fb3d192e-42e6-4831-9be4-e2b940ad9ef1">
<<<<<<< HEAD

# HW4 Writeup:
Welcome to the VR Cooking Simulator! Our project features interactive elements such as a vegetable slicing mechanic with realistic audio feedback, salt and pepper shakers that pour particles when tilted. This guide will walk you through setting up the project, understanding the controls, and getting the most out of your VR cooking experience.

## Gameplay Instructions

### Text Canvases
- **Objective:** Display informative text within the VR environment to guide the user's actions.
- **Mechanics:**
  - Text canvases are placed within the VR scene to provide instructions or feedback.
  - Interactive rays are shown when the user hovers over the continue or proceed buttons so that the user is aware that they can transition to the next canvas by grabbing the respective button.

### Vegetable Slicing
- **Objective:** Slice the vegetables using the knife provided.
- **Mechanics:**
  - Pick up the knife using the grip button on your controller.
  - Move the knife towards the vegetables. When the knife collides with the vegetables, slicing occurs.
  - An audio clip of slicing will play to enhance realism.
  - The vegetables will be sliced in a realistic manner, which you can further interact with.

### Salt and Pepper Shakers
- **Objective:** Pour salt and pepper particles from the respective shakers.
- **Mechanics:**
  - Pick up the salt or pepper shaker using the grip button on your controller.
  - Tilt the shaker past a certain angle (45 degrees) to start pouring the particles.
  - The particles will emit from the end of the shaker, simulating the pouring action.

## Controls
- **Movement:** Use the thumbstick on the left controller to move around.
- **Interact/Grab:**
  - **Grip Button:** Pick up and hold objects (e.g., knife, chopping board, salt shaker, pepper shaker, knife). This button is predominantly used for all actions within our game, even to select text canvas transitions.
- **Tilt/Pour:**
  - Tilt the shakers to pour salt or pepper when the tilt angle exceeds 45 degrees.
- **Vegetable Slicing:**
  - Bring the knife into contact with the bread to slice it
 
## Known Issues (not enough time to resolve these)
- **Lag when Slicing:** The setup requires some form of optimization to avoid lagging instances within the VR environment. This lag is not experienced in Unity's editor but mainly only when running the game on the VR.
- **Particle Emission:** Particle systems might need fine-tuning for optimal visual effects.


# HW4 Writeup:
Welcome to the VR Cooking Simulator! Our project features interactive elements such as a vegetable slicing mechanic with realistic audio feedback, salt and pepper shakers that pour particles when tilted. This guide will walk you through setting up the project, understanding the controls, and getting the most out of your VR cooking experience.

## Gameplay Instructions

### Text Canvases
- **Objective:** Display informative text within the VR environment to guide the user's actions.
- **Mechanics:**
  - Text canvases are placed within the VR scene to provide instructions or feedback.
  - Interactive rays are shown when the user hovers over the continue or proceed buttons so that the user is aware that they can transition to the next canvas by grabbing the respective button.

### Vegetable Slicing
- **Objective:** Slice the vegetables using the knife provided.
- **Mechanics:**
  - Pick up the knife using the grip button on your controller.
  - Move the knife towards the vegetables. When the knife collides with the vegetables, slicing occurs.
  - An audio clip of slicing will play to enhance realism.
  - The vegetables will be sliced in a realistic manner, which you can further interact with.

### Salt and Pepper Shakers
- **Objective:** Pour salt and pepper particles from the respective shakers.
- **Mechanics:**
  - Pick up the salt or pepper shaker using the grip button on your controller.
  - Tilt the shaker past a certain angle (45 degrees) to start pouring the particles.
  - The particles will emit from the end of the shaker, simulating the pouring action.

## Controls
- **Movement:** Use the thumbstick on the left controller to move around.
- **Interact/Grab:**
  - **Grip Button:** Pick up and hold objects (e.g., knife, chopping board, salt shaker, pepper shaker, knife). This button is predominantly used for all actions within our game, even to select text canvas transitions.
- **Tilt/Pour:**
  - Tilt the shakers to pour salt or pepper when the tilt angle exceeds 45 degrees.
- **Vegetable Slicing:**
  - Bring the knife into contact with the bread to slice it
 
## Known Issues (not enough time to resolve these)
- **Lag when Slicing:** The setup requires some form of optimization to avoid lagging instances within the VR environment. This lag is not experienced in Unity's editor but mainly only when running the game on the VR.
- **Particle Emission:** Particle systems might need fine-tuning for optimal visual effects.
