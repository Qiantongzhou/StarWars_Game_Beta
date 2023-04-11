# Comp476_TeamProject 
- @2023-W-Comp476
  
# GAME: ASTEROID WARS
- Asteroid Wars is an arcade-style shooter game where the player flies a spaceship and battles enemy teams in a deathmatch. The objective is to destroy the enemy team’s spawn point, which is a big mothership and then eliminate all remaining enemies. The competition happens between two teams and the first one to destroy the enemy base wins, the match lasts a limited time, and if both team bases are alive at the end, the team with the most ships kills wins/it’s a tie.
- The setting of this game is in the middle of a chaotic solar system where asteroids, planets and black holes act as obstacles to all spaceships trying to maneuver through it. The player can acquire different items to use in battle, such as shields and missiles. The map will have obstacles to avoid, as well as portals to quickly place itself in strategic positions. The enemy ships will use adaptable artificial intelligence to form team strategies and try to outsmart the opposite team.


- [Click Here To Check Full Documents](https://docs.google.com/document/d/152IiH644G6g-sS6qVvFXUZ_jZ827LYQ5wGeIpCHpICk/edit?usp=sharing)

## Controls
- WASD: Move the player forward, left, right and backward
- Shift: Turbo
- Mouse: Fire
- TAB: equipment and inventory

## Installation and Running
1. Clone the repository to your local machine.
2. Open the Unity engine(2021.3.9f1) and import the repository.
3. Run the game scene and start playing.

## Contributors
  - Zisen Ling #40020293
  - Tongzhou Qian #40081938
  - Pascal Labelle Poblette #40131558 

## Mechanics Analysis
- Speed Boost: this mechanic was added with the purpose of increasing the pace of the game by allowing players to get from one point of the map to another faster and catch up to their teammates or enemies. The boost is temporary and recharges overtime, this is to prevent players from spamming the boost and add more thought into when it is necessary.
- Dodge Roll: this is added as an evasive maneuver so players can perform a quick turn and escape the sights of the enemy for a brief moment.
- Warp Portals: the goal of portals is to make traversing the map faster, but the locations of the portals are in open areas with little cover, making the users an easy target when using them.
- Mothership defends itself: since the mothership of each team does not move, it will defend itself to make its destruction more challenging to the enemy team. If an enemy spaceship enters its detection area, homing missiles are shot and chase enemy ships for some time. In addition to the missiles, if an enemy ship is in the line of sight of the mothership, a laser beam is shot forwards, this is a balancing feature to prevent enemies from camping at the spawn area.
- Weapon equipment: the player can switch it’s weapons during a game by opening their inventory and dragging the weapon they want into one of the two available slots in the ship. These weapons may now be used.
- Homing missiles: one of the defense mechanisms of the motherships is shooting multiple missiles that follow the target. These missiles use flocking behaviour and use a strategic AI to try and flank their target. Once the missile gets close to the target, it will stop tracking, to give the ship a chance to avoid if it evades quickly enough.
