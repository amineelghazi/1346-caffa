Français | [English](README.md)

# 1346 – Caffa

Mini-projet 2D — Plateformeur

Date de début : 16 septembre 2026

Type de jeu : Plateformeur 2D

## Sommaire

- [Histoire](#histoire)
- [Fonctionnalités principales](#fonctionnalités-principales)
- [Architecture (scripts & objets)](#architecture-scripts--objets)
- [Crédits & sources](#crédits--sources)
- [Licence et Auteur](#licence-et-auteur)

## Histoire

Nous sommes en 1346, au début de la peste noire. Certains animaux sont infectés, mais le joueur ne connaît pas l'état de santé de chacun.

Le personnage principal est un médecin de renom dont l'objectif est de sauver les humains et les animaux. Certains animaux sont passifs, d'autres risquent de l'attaquer. Le médecin ne souhaite pas les éliminer, mais s'il n'a pas le choix, il le fera à l'aide de son arme.

Au détriment de sa propre santé, il parcourt l'Europe à la recherche de soins pour guérir les autres. Au lieu de se protéger et de s'éloigner de la propagation de la maladie, il refuse de cesser d'aider son prochain : médecin depuis longtemps, il ira au cœur de l'épidémie, au siège de Caffa, en Crimée.

Muni seulement d'une épée et d'un fusil tranquillisant, il conserve son humanité et protège les siens d'un malheur inconnu.

## Fonctionnalités principales

- Inspection des animaux lorsque le personnage est suffisamment proche d'eux
- Affichage d'un message dans la console ou l'UI lors de l'inspection d'un animal
- Combat à l'épée : le médecin peut éliminer les ennemis
- Fusil tranquillisant : permet de faire tomber les oiseaux trop hauts afin de les inspecter
- Contamination progressive : le personnage devient de plus en plus contaminé à mesure qu'il avance
- Objets de soins : réinitialisent l'état de contamination ou réduisent la maladie après un certain nombre d'animaux infectés éliminés
- Obstacles : murs et autres éléments visibles bloquant le passage, franchissables grâce au saut
- Deux catégories d'animaux terrestres malades : agressifs et inoffensifs — le joueur choisit de les sauver ou de les éliminer
- Déplacement du joueur régulier, avec animations de course et de saut
- Déplacement automatique des animaux sauvages, oiseaux, etc.

## Architecture (scripts & objets)

| # | Script / Objet | Rôle |
|---|---|---|
| 1 | `GameManager` | Gestion globale du jeu |
| 2 | `PlayerController` | Mouvement du joueur |
| 3 | `PlayerHealth` | Gestion de la barre de vie |
| 4 | `PlayerWeapons` | Épée ou fusil / changement d'arme |
| 5 | `PlayerInspectAnimal` | Inspection des animaux |
| 6 | `AnimalPassive` | Comportement des animaux inoffensifs |
| 7 | `AnimalAggresive` | Comportement des animaux agressifs |
| 8 | `AnimalHealth` | Santé / état d'infection des animaux |
| 9 | `WallObject` | Obstacles infranchissables |
| 10 | `PotionObject` | Objets de soins |
| 11 | `DangerZone` | Zones de danger / contamination |

## Crédits & sources

**Animaux**
- Ennemis (sprite sheets pixel art) : Free Enemy Sprite Sheets – free-game-assets
- Mouton : Pixel Sheep – gntldragon
- Oiseau : Pixel Art Bird 16x16 – ma9ici4n

**Personnage**
- Plague Crow – gabry-corti
- Platformer – kybernetik

**Armes**
- Arme tranquillisante (oiseaux contaminés) : Gun Assets – kaylousberg
- Arme historique : Redacted Armory – mosleybrothersgames

**Environnement**
- Forest Nature Fantasy Tileset – theflavare

**Contexte historique**
- Propagation de la peste noire en Europe (1346-1353) – Wikipédia

## Licence et Auteur

Auteur : Amine El Ghazi

Ce projet et son contenu sont la propriété exclusive d'Amine El Ghazi. Aucune réutilisation, copie, modification ou distribution n'est autorisée sans accord écrit préalable.

Copyright © 2026 Amine El Ghazi. Tous droits réservés.
