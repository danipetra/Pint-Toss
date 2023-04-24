TODO:
Programming:
Respawn player and enemy after each throw, base it on a grid. You have to keep opponents at a minimum distance (xSpace occupied*3)

add pc input actions and controls
Tiro perfetto (da regolare sulla base dello slider)

Scene Management and graphics:
Reskin pint

TOIMPORT:
-PauseMenu
-MainMenu
-SaveSystem, coins based on score
-GameOverScene


IN PROGRESS:
Automatic player and enemy rotation after the ball touches the ground/basket
_inputManager: tweak it and Player Values to give smoother controls
_Enemy, throwing him instead of only the ball gameObject
Create an enviroment using tavern assets

KNOWN ISSUES:
_Enemy AI, checking the pint value canBeTrown causes a bug in the throwing force
_Throwing: For the first throw the inputManager takes the wrong start position (left-base angle of the screen)
_Slider: can go down if the player swipes down
_lineRenderer: not working

COMPLETE:
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