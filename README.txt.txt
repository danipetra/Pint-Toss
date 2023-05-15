‘Challenge poco giocabile - codice strutturato in maniera piuttosto intricata e con molte bad practices difficili da ‘pulire; 
inoltre non tutti i requirements richiesti sono stati sviluppati, per cui la challenge risulta purtroppo insufficiente.’


KNOWN ISSUES (marked with _ ):
_Swipe: it depends on Screen.Widht so it may be more fast or slow depending on screen res // I NORMALIZED IT, further test needed

IN PROGRESS:
deactivate backboard blink in case someone scored with it
deactivate fire on opponent after he is on fire and misses a shot
_Player Throwing: For the first throw and sometimes during the game the inputManager takes the wrong start position (left-base angle of the screen

IMPROVEMENTS:
Base perfect throw on collisions instead than with values, so that it's independent from changes
Implement an Observer to handle pint collisions, it would significantly improve code, test performances

BONUSES:
Respawn player and enemy after each throw, assigning a new position based on a rotation around the bucket. You have to keep opponents at a minimum distance (ALREADY DEVELOPED, fix it)
Pause menu (Already developed)
Camera direction needs to change if the player respawns and rotates (not needed for now)


----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

COMPLETE:
01 / 05
_Last android build takes some seconds in black screen before starting
Tutorial board showing game mechanics, integrated into mainmenu scene
Added UI measures on the slider for perfect throw and backboard throw
_RewardScene and JsonLoader not working on mobile build, Json loader errors in Awake and in StopGame
Make the audio manager a singleton and add sounds for: RewardScene, onfire ball, increase volume onDrink

30 / 04
Fire basic particles 
Basic lighting (only on game scene), change settings on build
Refactoring
Fireball gameplay: by scoring points your energy bar gets filled, once full your ball is on fire and all points are doubled for a certain amount of time (indicated by the same bar emptying) or until you miss a point.
perfect throw bonus (base it on slider value .38 < .45)
Pc input actions and controls
Add a text to the player that shows the points if scored

29 / 04
make enemy transparent and red
Pint angular velocity when throwing it
_Slider: can go down if the player swipes down, modify it only if there's an increment in the swipe's Ys
_UI: scaling differently on android and on different devices, it depends on screen resolution
_Build and test on mobile devices
Reskin pint

28 / 04
Backboard blink: increase the probability of the Backboard to blink, giving a +6 bonus
Backboard bouns, hitting it doubles the score. Fixed bugs.
Save system, each round the player gets the score converted by coins and a bonus if he wins.
Text update based on Json 
_Slider: make it not interactable

27 / 04
Create an enviroment using tavern assets
Create Main menu and GameOver

26 / 04
Automatic lookAt after the ball touches the ground/basket
)
_GameManager couldn't find the player caues it was istantiated after the GameManager start
25 / 04
_inputManager: tweak it and Player Values to give smoother controls
_Enemy, throwing him instead of only the ball gameObject
_Enemy AI, repeating the throw coroutine at each Update and not applying any delay while deciding the throw force, resulting in too many throws
_Enemy AI, checking the pint value canBeTrown or using force variable causes a bug in the throwing force
24 / 04
OnFire system
Enemy IA, chooses a random value between 0 and 1, as the player, but without the slider. The more is near 1 the more time it takes(chose a cubic distribution)
Backboard with a bouncier material, on Collision increases the score multiplier (straight forward)
23 / 04
Force Slider
Audio Manager
OnFire system base(To complete)
Camera Follow
.. / ..
Refactoring
Touch timer
Score system
Fix X throw force and use direction of Ball game object instead 
Input system and slider update, Throw based on slider value
FIX reset dello slider ad ogni impatto della palla col suolo (e non con un nuovo input del player)
Inform about parabolic line rendering, to predict the right force before throwing