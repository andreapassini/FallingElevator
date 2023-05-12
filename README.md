# FallingElevator
One button game of a falling elevator.


# Game Concept

You are in elevator that is falling.
Press 1 button to make the elvator go into the rigth or release it to make it go to the left.

Objective: Survive, avoiding obstacles, as musch as you can.

# Implementation

## Assets

Use Kenney Assets
( https://www.kenney.nl/assets/pixel-platformer-industrial-expansion )

- 18 x 18
- Offset 1 x 1

## Camera

### SOl 1

Rotate the camera of 90° around the X axis, so the falling can be simulated ( constant speed along X) and the going right and left can be simulated by changing the gravity.


### Sol 2

Falling in the y axis, add force to the x axis based on the button.

### Sol 3

as Sol 1 but the object is not moving, is the level that is moving.