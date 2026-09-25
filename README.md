# 1346 – Caffa / mini-projet2D
**Date de début :** 16 septembre 2026

**Type de jeu :** Plateformeur 2D (2D Platformer)

---

## Partie 1 – Histoire du jeu

Nous sommes en 1346, au début de la peste noire (*Black Death*). Certains animaux sont infectés, mais le joueur ne connaît pas l'état de santé de chacun.

Le personnage principal est un médecin de renom dont l'objectif est de sauver les humains et les animaux. Certains animaux sont passifs, d'autres risquent de l'attaquer. Le médecin ne souhaite pas les éliminer, mais s'il n'a pas le choix, il le fera à l'aide de son arme.

Au détriment de sa propre santé, il parcourt l'Europe à la recherche de soins pour guérir les autres. Au lieu de se protéger et de s'éloigner de la propagation de la maladie, il refuse de cesser d'aider son prochain : médecin depuis longtemps, il ira au cœur de l'épidémie, au siège de **Caffa**, en Crimée.

Muni seulement d'une épée et d'un fusil tranquillisant, il conserve son humanité et protège les siens d'un malheur inconnu.

---

## Partie 2 – Fonctionnalités principales

1. **Inspection des animaux** lorsque le personnage est suffisamment proche d'eux.
2. **Affichage d'un message** dans la console ou dans l'interface (UI) lors de l'inspection d'un animal.
3. **Combat à l'épée** : le médecin peut éliminer les ennemis.
4. **Fusil tranquillisant** : permet de faire tomber les oiseaux trop hauts afin de les inspecter.
5. **Contamination progressive** : le personnage devient de plus en plus contaminé à mesure qu'il avance.
6. **Objets de soins** : permettent de réinitialiser l'état de contamination ou de réduire la maladie après un certain nombre d'animaux infectés par la peste noire éliminés.
7. **Obstacles** : des obstacles visibles (murs, etc.) bloquent le passage, le personnage utilise donc sa capacité de saut.
8. **Deux catégories d'animaux terrestres malades** : agressifs et inoffensifs. Le joueur choisit de les sauver ou de les éliminer.
9. **Déplacement du joueur** régulier, avec animations de course et de saut.
10. **Déplacement automatique** des animaux sauvages, des oiseaux, etc.

---

## Partie 3 – Logique métier (scripts) et objets

| # | Script / Objet | Rôle |
|---|----------------|------|
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

---

## Sources et crédits

### Animaux
- Ennemis (sprite sheets pixel art) : [Free Enemy Sprite Sheets – free-game-assets](https://free-game-assets.itch.io/free-enemy-sprite-sheets-pixel-art)
- Mouton : [Pixel Sheep – gntldragon](https://gntldragon.itch.io/pixel-sheep)
- Oiseau : [Pixel Art Bird 16x16 – ma9ici4n](https://ma9ici4n.itch.io/pixel-art-bird-16x16)

### Personnage
- [Plague Crow – gabry-corti](https://gabry-corti.itch.io/plague-crow)
- [Platformer – kybernetik](https://kybernetik.itch.io/platformer)

### Armes
- Arme tranquillisante (oiseaux contaminés) : [Gun Assets – kaylousberg](https://kaylousberg.itch.io/gun-assets)
- Arme historique : [Redacted Armory – mosleybrothersgames](https://mosleybrothersgames.itch.io/redacted-armory)

### Environnement
- [Forest Nature Fantasy Tileset – theflavare](https://theflavare.itch.io/forest-nature-fantasy-tileset)

### Contexte historique
- [Propagation de la peste noire en Europe (1346-1353) – Wikipédia](https://en.wikipedia.org/wiki/Black_Death#/media/File:1346-1353_spread_of_the_Black_Death_in_Europe_map.svg)

## Licence et Auteur

**Auteur :** Amine El Ghazi  
Ce projet et son contenu sont la propriété exclusive d'Amine El Ghazi. Aucune réutilisation, copie, modification ou distribution n'est autorisée sans accord écrit préalable.

Copyright © 2026 Amine El Ghazi. Tous droits réservés.
