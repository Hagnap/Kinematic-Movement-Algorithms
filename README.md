-------------------------------------------------------------------------------------------------------------------------------
# Kinematic-Movement-Algorithms

The Kinematic Movement Algorithms were implemented using C# and Unity. These algorithms were inspired by the psuedo code in the book "AI For Game Third Edition" by Ian Millington. These algorithms use position and orientation to output a velocity for the agent. I implemented six variants; Seek, Flee, Arrive, Wander, Pursuit, and Evade. Best used for when the agent is chasing a target as it will never actually reach its goal, just continue to seek it. If used to go to a stationary point it will cause the agent to wiggle and overshoot an exact point in the world. 

-------------------------------------------------------------------------------------------------------------------------------
## *Seek*

Kinematic-Seek takes both an agent's and its target's position and orientation to calculate the needed velocity for the agent. Once the velocity is calculated the agent moves by multiplying the velocity by the agent's max-speed. The agent will also rotate so that it will face towards its target.

-------------------------------------------------------------------------------------------------------------------------------
## *Flee*

Kinematic-Flee is very similar to Kinematic-Seek however we change one line of code. For this we calculate the direction by doing `this.transform.position - target.transform.position` instead of `target.transform.position - this.transform.position`. This will also make the agent rotate so that it is facing away from the target. 

-------------------------------------------------------------------------------------------------------------------------------
## *Arrive*

Kinematic-Arrive is also similar to Kinematic-Seek however this algorithm is best used when the agent has a target that is stationary. This algorithm uses a **radius of satisfaction**, this is an imaginary circle around the target and if the agent is within this circle it is 'satisfied' by being close enough to its target which will result in the agent stopping. This fixes the issues of overshooting and wiggling seen in Seek when it's used to move the agent to a stationary target. This algorithm will also make the agent rotate so that it will face towards its target.

-------------------------------------------------------------------------------------------------------------------------------
## *Wander*

Kinematic-Wander will make the agent only move in a forward direction at the agent's max-speed. However, this algorithm will modify the agent's orientation radonmly so that it moves in a random manner, this algorithm has no target for the agent. 

-------------------------------------------------------------------------------------------------------------------------------
## *Pursue*

An exetension of Kinematic-Seek. Approximates the target's future position and then passes that position into Seek so that the agent moves to that position.

-------------------------------------------------------------------------------------------------------------------------------
## *Evade*

An exetension of Kinematic-Flee. Approximates the target's future position and then passes that position into Flee so that the agent moves away from that position.
