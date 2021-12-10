# Space Exterminator (copy of original private repo)
## Hoyuong Jun, Fanruo Gu, Adam Yuzhen Zhang
## Trailer -> https://www.youtube.com/watch?v=G0gBD-wcK9Q

TroubleShooting:
1. If there's any missing files or prefabs, try Assets -> reimport all in the Unity
2. If there's any compile error regarding the postprocessing, got to Unity Package Manager and remove PostProcessing
3. You may need to relaunch the app to enable haptic feedback

## Project description  
Traveling from planet to planet, you are a space exterminator who eliminates alien pests that bother dwellers of all space civilizations. Take your next task, grab your weapons and shield, and kill the pests! It is your duty to protect the universe!  
## Instructions  
The grip buttons on both hands grab weapons and the shield.   
The trigger button interacts with the UI elements, and fires the weapon.  
The shield is behind the player and can be grabbed with a hand behind the back. It can be thrown and catched like a boomerang to kill enemies.   
The menu button is the X button on the left hand and pauses the game  
In Survival Mode, the left hand has a watch showing your current score and health  
The joysticks can be used to push and rotate weapons to shoot enemies from afar  
There are in-app instruction diagrams and onboarding.  
## Features  
Scenes  
Spaceship - Home Scene  
* A 3D menu system that links to the other three scenes  
* Tutorial diagrams attached to the panels on the spaceship  
* Modeled with Gravity Sketch  

Pandora - The Lab  
* An alien planet scene with onboarding system and experiment mode  
* The experiment mode allows spawning different types of enemies with buttons and revisit onboarding tutorials of weapons.  
* Modeled with Tilt Brush  

Helios - Arcade Mode  
* Predefined waves of enemies  
* Scoring system based on the following aspects:  
* Number of hits taken; Remaining time  
* Modeled with Tilt Brush  

LV-426 - Survival Mode  
* Auto-generated infinite waves of enemies  
* Scoring system based on survival time and enemies killed  
* Modeled with Tilt Brush  
     Each scene has different background music corresponding to their respective intensity  
Player Body  
* XR Rig with animated hands  
* A player statistics screen attached to the left hand. Is visible in the lab and the survival mode.  
* Weapons are attached to the body on a belt. They can be grabbed by controllers and return to their original location on release.  

Weapon System  
* Handgun - Shoots normal bullets with trails  
** Bullets bounce off enemies and can hit other enemies  
** Pros: Longer range, precision   
** Cons: Inefficient in attacking enemies in quantities, cannot break through enemy shield  
* Flamethrower - Shoots flames  
** Pros: Attacking enemies in quantities  
** Cons: Shorter range, slower death  
** Burning animation is implemented for enemies that are hit  
* Grenade Launcher - Shoots grenades  
** Pros: Splash damage, powerful  
** Cons: Shorter range, has cool down time of 3 seconds  
** The grenade launcher’s light turns red after shooting, and slowly turns green during cooldown  
* Boomerang Shield - Blocks enemy attacks  
** Can be thrown as a weapon  
** Able to momentarily stun Beasts and kill other types of enemies  
* Remote Shooting  
** An experimental feature that allows the player to use the joystick to move and/or rotate weapons forward and shoot remotely  
** Particularly useful against shielded enemies  

Enemy System  
* Drones  
** Long range laser attacks; Hovers in mid-air; Random movement across the sky; Keeps distance from the player  
* Crawlers  
** Close range attacks (stabs with claws); Crawls towards the player; Quick and agile movement; Small in size but large in quantity  
* Beasts  
** Close range explosion attacks; Walks towards the player; Slow movement; Equipped with an unbreakable energy shield on its frontside  

UI system  
* 3D UI for scene selection  
* Planet button prefab that enlarges and shows scene name when hovered, and updates the play button text when pressed.  
* Tutorial UI system  
** Floating screens in front of the player and updates with the tutorial steps  
** Lerps its height to match player height for better visibility.   
* Main menu toggled by button X.  
* In game menu and scoring UI  
** Toggled by button X, which also pauses and resumes the game.  
* Player stats attached to the left hand  
** Displays health and time in the survival mode of the game  

Audio Feedback  
* Weapons  
** Shooting/Flames; Hitting targets; Grenade explosion  
* Enemy  
** Breathing and growling noises  
** Beast grunts when attacked by shield  
* Attack sounds: Crawler stabs, Beast explosions, Drone lasers  
** Destruction/dying sounds  
** Beast shield deflection sounds  

UI  
* Hovering above different planet options  

Haptic Feedback  
* Weapons  
** Grabbing weapons; Shooting weapons; Blocking  
* Enemy  
** Getting hit by enemies  
** Getting hit and blocking have different lengths of haptic feedback, and when hit by enemies both hands receive feedback  

Onboarding  
* Teaching section that teaches each weapon and let the player learn their pros and cons through killing real enemies  
* For each weapon there is one floating description screen  
* A red line that links the weapon to the player’s hand  
* Two waves of enemies that the player needs to eliminate before proceeding to next weapon  
* At the end, there is an experiment section that lets the player revisit tutorials texts and spawn different types of enemies selectively, in order to test out weapons and features at the player’s own pace.  

## Technical contribution  
We used XR Interactable for any interaction that involves hands, such as grabbing, hand animations, remote shooting, and throwing shields.  
The weapons are divided into two parts: the barrel and the handle, in order to correctly determine the position to grab the gun and the point to shoot the projectile from.  
The handgun and grenade launcher use rigidbody projectiles, while the flamethrower uses a particle system (from the asset store).  
Grenade launcher colors are changed by changing the mesh renderer according to time  
Drones move randomly by adding random (x, y, z) values between -1 to 1 to their original position  
Lasers are lerped to the player’s position with a random offset of x, y  
Crawlers and Beasts lerp towards while animating the player and attack (animates and causes damage) when within the assigned range. When beasts explodes, they can hurt nearby enemies as well (particle collision)  
The shield to the beast was added with a half sphere, and rendered visible through collision with a projectile (using tags)  
Haptic and audio feedback and Animators are located next to appropriate events within the script  
Models such as the handgun, grenade launcher, shield, belt, and some scenes were drawn within another VR app (Tiltbrush, Gravity Sketch)  
We have several branches that each of us worked on separately to avoid collision, and in some cases worked together from a single laptop.  



## Outside Sources  
Oculus Hand Models https://developer.oculus.com/downloads/package/oculus-hand-models/  
Double Sided Shaders https://assetstore.unity.com/packages/vfx/shaders/free-double-sided-shaders-23087#content  
Scifi gun - https://assetstore.unity.com/packages/vfx/shaders/free-double-sided-shaders-23087#content  
Scifi audio - https://assetstore.unity.com/packages/audio/sound-fx/weapons/ultra-sci-fi-game-audio-weapons-pack-vol-1-113047   
Flames and explosion - https://assetstore.unity.com/packages/essentials/asset-packs/unity-particle-pack-5-x-73777   
XR plugin management package, XR Interaction Toolkit, and Oculus XR Plugin  
Tilt Brush Toolkit https://github.com/googlevr/tilt-brush-toolkit for handling Tilt Brush material when importing.  
Background music  
https://assetstore.unity.com/packages/audio/music/space-threat-free-action-music-205935  
https://assetstore.unity.com/packages/audio/ambient/sci-fi/universe-sounds-free-pack-118865   
https://assetstore.unity.com/packages/audio/ambient/sci-fi/synthwave-free-sessions-135370   
Enemies  
https://assetstore.unity.com/packages/3d/characters/creatures/fantasy-rhino-66109   
https://assetstore.unity.com/packages/3d/characters/insectoid-crab-monster-lurker-of-the-shores-20-animations-107223  
https://assetstore.unity.com/packages/3d/vehicles/space/alien-ships-pack-131137   
Miscellaneous Audio  
https://www.zapsplat.com/music/hippopotamus-or-rhinoceros-grunt-1/  
https://www.zapsplat.com/music/anime-burst-of-energy-laser-beam-fast-and-hard/  
https://www.zapsplat.com/music/creature-monster-curious-panting-and-grunting-2/  
https://www.zapsplat.com/music/knife-stab-body-blood-squelch-2/  

