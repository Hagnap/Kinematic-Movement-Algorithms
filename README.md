# KinematicMovementAlgorithms
-------------------------------------------------------------------------------------------------------------------------------
The Kinematic Movement Algorithms were implemented using C# and Unity. These algorithms were inspired by the psuedo code in the book "AI For Game Third Edition" by Ian Millington. These algorithms use position and orientation to output a velocity for the agent. I implemented four variants; Seek, Flee, Arrive, and Wander. 
</br>
### Seek
-------------------------------------------------------------------------------------------------------------------------------
Kinematic-Seek takes both an agent's and its target's position and orientation to calculate the needed velocity for the agent. Once the velocity is calculated the agent moves by multiplying the velocity by the agent's max-speed. The agent will also rotate so that it will face towards its target.

### Flee
-------------------------------------------------------------------------------------------------------------------------------
Kinematic-Flee is very similar to Kinematic-Seek however we change one line of code. For this we calculate the direction by doing `this.transform.position - target.transform.position` instead of `target.transform.position - this.transform.position`. This will also make the agent rotate so that it is facing away from the target. 

### Arrive
-------------------------------------------------------------------------------------------------------------------------------

### Wander
-------------------------------------------------------------------------------------------------------------------------------
