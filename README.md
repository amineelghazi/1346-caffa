[Français](README.fr.md) | English

# 1346 – Caffa

Mini-project 2D — Platformer

Start date: September 16, 2026

Game type: 2D Platformer

## Table of Contents

- [Story](#story)
- [Main Features](#main-features)
- [Architecture (scripts & objects)](#architecture-scripts--objects)
- [Credits & Sources](#credits--sources)
- [License and Author](#license-and-author)

## Story

The year is 1346, at the dawn of the Black Death. Some animals are infected, but the player has no way of knowing the health status of each one.

The main character is a renowned physician whose goal is to save both humans and animals. Some animals are passive, while others may attack him. The physician has no wish to kill them, but if left with no choice, he will use his weapon.

At the expense of his own health, he travels across Europe searching for cures to heal others. Rather than protecting himself and fleeing the spread of the disease, he refuses to stop helping his fellow man: a long-serving physician, he heads straight into the heart of the epidemic, to the siege of Caffa, in Crimea.

Armed with nothing but a sword and a tranquilizer gun, he holds onto his humanity while protecting others from an unknown misfortune.

## Main Features

- Animal inspection when the character gets close enough to them
- On-screen message in the console or UI when inspecting an animal
- Sword combat : the physician can eliminate enemies
- Tranquilizer gun : brings down birds flying too high so they can be inspected
- Progressive contamination : the character becomes increasingly infected as he advances
- Healing items : reset the contamination level or reduce sickness after eliminating a certain number of plague-infected animals
- Obstacles : visible obstacles (walls, etc.) block the path, requiring the character's jump ability
- Two categories of sick land animals : aggressive and harmless — the player chooses to save or eliminate them
- Player movement at a steady pace, with running and jumping animations
- Automatic movement of wild animals, birds, etc.

## Architecture (scripts & objects)

| # | Script / Object | Role |
|---|---|---|
| 1 | `GameManager` | Overall game management |
| 2 | `PlayerController` | Player movement |
| 3 | `PlayerHealth` | Health bar management |
| 4 | `PlayerWeapons` | Sword or gun / weapon switching |
| 5 | `PlayerInspectAnimal` | Animal inspection |
| 6 | `AnimalPassive` | Harmless animal behavior |
| 7 | `AnimalAggresive` | Aggressive animal behavior |
| 8 | `AnimalHealth` | Animal health / infection status |
| 9 | `WallObject` | Impassable obstacles |
| 10 | `PotionObject` | Healing items |
| 11 | `DangerZone` | Danger / contamination zones |

## Credits & Sources

**Animals**

[- Enemies (pixel art sprite sheets): Free Enemy Sprite Sheets – free-game-assets](https://free-game-assets.itch.io/free-enemy-sprite-sheets-pixel-art)
[- Sheep: Pixel Sheep – gntldragon](https://gntldragon.itch.io/pixel-sheep)
[- Bird: Pixel Art Bird 16x16 – ma9ici4n](https://ma9ici4n.itch.io/pixel-art-bird-16x16)

**Character**

[- Plague Crow – gabry-corti](https://gabry-corti.itch.io/plague-crow)
[- Platformer – kybernetik](https://kybernetik.itch.io/platformer)

**Environment**

[- Forest Nature Fantasy Tileset – theflavare](https://theflavare.itch.io/forest-nature-fantasy-tileset)

**Historical Context**

[- Spread of the Black Death in Europe (1346-1353) – Wikipedia](https://en.wikipedia.org/wiki/Black_Death#/media/File:1346-1353_spread_of_the_Black_Death_in_Europe_map.svg)

## License and Author

Author: Amine El Ghazi

This project and its content are the exclusive property of Amine El Ghazi. No reuse, copying, modification, or distribution is permitted without prior written consent.

Copyright © 2026 Amine El Ghazi. All rights reserved.
