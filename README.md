# Tetris
<img width="276.48" height="155.52" src="https://github.com/SergeiBak/PersonalWebsite/blob/master/images/tetris.png?raw=true">

## Table of Contents
* [Overview](#Overview)
* [Test The Project!](#test-the-project)
* [Code](#Code)
* [Technologies](#Technologies)
* [Resources](#Resources)
* [Donations](#Donations)

## Overview
This project is a recreation of the good old Tetris, a timeless classic played by at least a billion people (fun fact). This solo project was developed in Unity using C# as 
part of my minigames series where I utilize various resources to remake simple games in order to further my learning as well as to have fun!   

Tetris consists of a 10x20 grid, in which you are given blocks of various shapes (known as tetrominos) that are constantly falling downward, and it is up to you to shift 
them into lines that get cleared/destroyed whenever a row is full. As the level increases more and more (from level 0), the blocks fall faster increasing difficulty (and score earned).

## Test The Project!
In order to play this version of Tetris, follow the [link](https://sergeibak.github.io/PersonalWebsite/tetris.html) to a in-browser WebGL build (No download required!).

## Code
A brief description of all of the classes is as follows:
- The ```Board``` class is a singleton class that handles much of the base logic for how the game flows in the game scene. It manages the tilemap including actions such as rendering pieces, clearing rows, etc.
- The ```ButtonColor``` class handles changing the font color of the button it is attached to based on whether the mouse cursor is hovering over it.
- The ```ColorToggle``` class handles the logic for the tetromino color toggle setting, which decides whether to spawn pieces based on a certain color index or random colors.
- The ```Data``` class stores a bunch of values related to each tetromino piece that helps in calculating how to render said piece and rotate it.
- The ```Ghost``` class controls the ghost piece that shows where the currently active tetromino piece will land.
- The ```MenuController``` class is a singleton class that handles much of the logic for how the menu scene flows, including navigation between different panels such as settings and stats.
- The ```Piece``` class takes in user input and controls where the currently active tetromino piece moves.
- The ```Shop``` class is a singleton class that manages all of the shopitems and makes sure they stay updated.
- The ```ShopItem``` class controls each individual shop item and processes actions such as buying/equipping the item.
- The ```Tetromino``` class is a structured class that stores the information of its tetromino piece within itself.
- The ```VolumeSlider``` class controls the volume slider found in settings, its actions include changing the slider color and updating the stored volume float.

## Technologies
- Unity
- Visual Studio
- GitHub
- GitHub Desktop

## Resources
- Credit goes to [Zigurous](https://www.youtube.com/channel/UCyaKsKqYTghxgAqywfefAzg) for the helpful base game tutorial!
- For the saving stats system, I made use of playprefs, here is some [helpful scripting documentation](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html) on how that works!
- I derived my formula for calculating score earned based on [this reference](https://www.codewars.com/kata/5da9af1142d7910001815d32)
- I calculated falling block speeds in relation to level based on [this reference](https://listfist.com/list-of-tetris-levels-by-speed-nes-ntsc-vs-pal)

## Donations
This game, like many of the others I have worked on, is completely free and made for fun and learning! If you would like to support what I do, you can donate at my metamask wallet address: ```0x32d04487a141277Bb100F4b6AdAfbFED38810F40```. Thank you very much!
