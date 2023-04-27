TODO:
Programming:
Mandatory
add pc input actions and controls
Perfect throw, +1 score (da regolare sulla base dello slider)
Bonuses:
Respawn player and enemy after each throw, assigning a new position based on a rotation around the bucket. You have to keep opponents at a minimum distance (ALREADY DEVELOPED, fix it)


Scene Management and graphics:
Reskin pint

TOIMPORT:

-SaveSystem, coins based on score
-GameOverScene


IN PROGRESS:
-PauseMenu
-MainMenu

KNOWN ISSUES:
_Slider: can go down if the player swipes down, modify it only if there's an increment in the swipe's Ys
_lineRenderer and projections: not working
_Camera not working on 3d, when the player rotates it keeps the same rotation

COMPLETE:
27 / 04
Create an enviroment using tavern assets
26 / 04
Automatic lookAt after the ball touches the ground/basket
_Player Throwing: For the first throw the inputManager takes the wrong start position (left-base angle of the screen)
_GameManager couldn't find the player caues it was istantiated after the GameManager start
25 / 04
_inputManager: tweak it and Player Values to give smoother controls
_Enemy, throwing him instead of only the ball gameObject
_Enemy AI, repeating the throw coroutine at each Update and not applying any delay while deciding the throw force, resulting in too many throws
_Enemy AI, checking the pint value canBeTrown or using force protected variable causes a bug in the throwing force
24 / 04
OnFire system
Enemy IA, chooses a random value between 0 and 1, as the player, but without the slider. The more is near 1 the more time it takes(chose a cubic distribution)
Backboard with a bouncier material, on Collision increases the score multiplier (straight forward)
23 / 04
Force Slider
Audio Manager
OnFire system base(To complete)
Camera Follow
Refactoring
Touch timer
Score system
Fix X throw force and use direction of Ball game object instead 
Input system and slider update, Throw based on slider value
FIX reset dello slider ad ogni impatto della palla col suolo (e non con un nuovo input del player)
Inform yourself about parabolic raycasting, to predict the right force before throwing