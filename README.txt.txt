TODO:
Programming:
Automatic player and enemy rotation after the ball touches the ground/basket
OnFire system
Respawn player and enemy after each throw, base it on a grid. You have to keep opponents at a minimum distance (xSpace occupied*3)

Tabellone combo (straight forward)
add pc input actions and controls
Tiro perfetto (da regolare sulla base dello slider)

Scene Management and graphics:
Inform yourself about parabolic raycasting, to predict the right force before throwing
Creare un ambiente usando gli asset della taverna
Reskin pint

TOIMPORT:
-PauseMenu
-MainMenu
-SaveSystem
-GameOverScene


IN PROGRESS:
Enemy IA, chooses a random value between 0 and 1, as the player, but without the slider. The more is near 1 the more time it takes(chose a cubic distribution)

KNOWN ISSUES:
_For the first throw the inputManager takes the wrong start position (left-base angle of the screen)
_Slider can go down if the player swipes down
_tweak inputManager and Player Values to give smoother controls

COMPLETE:
Audio Manager
OnFire system base(To complete)
Camera Follow
Refactoring
Touch timer
Force Slider
Score system
Fix X throw force and use direction of Ball game object instead 
Input system and slider update, Throw based on slider value
FIX reset dello slider ad ogni impatto della palla col suolo (e non con un nuovo input del player)