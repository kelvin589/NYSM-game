# Now You See Me
Now You See Me is an online multiplayer social deduction game.

## Table of Contents
* [Background](#background)
* [Story](#story)
* [Overview](#overview)
* [Controls](#controls)
* [Features](#features)
* [Possible Improvements](#possible-improvements)
* [Technologies](#technologies)
* [Play the Game](#play-the-game)
* [Setup](#setup)

## Background
This game has been created as part of a university team project module. The brief for the module was to create an online, multiplayer game. My roles during the project:
* Assisted in ideation of features and story
* Development of most of the main gameplay features
* Provided some overall help for the team

## Story
You and your fellow students are in a university lab where a science experiment has gone wrong, resulting in a virus escaping and infecting a student. In the efforts to contain this virus, the university campus has been cordoned off and isolated from the world. Supplies are running low. If you want to survive, you must work as a team to find and eliminate the virus.

Elimination of the virus, however, is not the only option. A broken car is in desperate need of repair with parts scattered all over the campus. You will need to work together and complete the given tasks if you want to escape.

If you die, do not fret as you can still play and enjoy the game! To avenge your death, you are now a vengeful ghost who can annoy the virus to sabotage their path.
Failure in finding the virus or escaping will result in certain death, for not only yourselves, but the rest of the world. It is now up to you to stop the future pandemic. Are you up to the task?

## Overview
Now You See Me (NYSM) is a 2D top-down, online multiplayer, social deduction game. The map of the game is meant to model a university campus with many areas to explore. NYSM plays on the typical deception genre by including multiple twists and features. The objective of the students is to complete all the tasks or correctly kill the virus via voting. The objectives of the virus is to kill all the students.

![Gameplay](https://user-images.githubusercontent.com/72221490/116595725-85447780-a91b-11eb-9229-0089bc970adc.mp4)

## Controls
* WASD to move
* E to collect items
* On-screen buttons can be used instead

## Features
* Tasks
  * Regular item pickup
  * Timed item pickup (hold down the key/on-screen button)
  * GUI minigame tasks
* Voting and Serums
  * Voting starts when clicking the report button above a dead body or the square at spawn 
  * Players vote out who they think the virus is
  * Using the serum (voting) on the wrong player will kill them
  * Voting can be skipped
  * Voting is limited to the number of serums remaining (3 to begin with) 
* Roles
  * Normal student can complete tasks
  * Business student completes timed tasks 10% faster
  * Computer scientist can view cameras from their phone 
* Ghosts
  * Dead players respawn as ghosts
  * Ghosts can push the virus 
* Virus
  * Virus can kill players
  * Virus can move tasks and dead bodies
* Teleporting to spawn
  * Players can press the 'un-stick' button to teleport back to spawn
  * (More of a bug due to players dying and getting stuck in walls, but let's make it a feature)

## Possible Improvements
* Bug fixing
* Improved graphics and interface
* User QOL
  * Remappable key bindings
  * Windowed and full screen
* Animation and sprite improvements
* Custom multiplayer backend
* Finishing the rest of the planned features
  * Completed roles
  * Unique assignment of roles
  * Text chat
  * Minigame tasks
  * Unique death animations
  * Enhancing the map
  * Lobby settings
  * Atmospheric 'torch' lighting
  * Multistep tasks
* Security
* Testing

## Technologies
Key technologies used to develop this game:
* Unity 2020.1.13f1
* Photon v2.22

## Play the Game
If you want to try this game:
1. Look to the right and click the Final Release
2. Download the required version for your OS
3. Enjoy!

## Setup
To run this project:
1. Clone this repository
2. Open Unity Hub
3. Click Add
4. Open the Now You See Me folder
5. Now you are ready to develop
