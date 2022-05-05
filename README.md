# Cribbage_V1
A C# console-based game of Cribbage.

![example](Reference/example.png)

## Prerequisites
Before you can run this program, ensure that you have the following software installed and functional:
* One of the following operating systems: Windows 7, 8, 8.1, or 10.
* C++ 11

Before playing this game, an understanding of Cribbage and its' nuances.
*[**Cribbage Rules: Bicycle**](https://bicyclecards.com/how-to-play/cribbage/)

## Running
1. Download the source code from this repository.
2. Compile and run the application.
3. (NOTE) Many functions either don't work or work partially. Continue at your own risk.
4. The screen will first display the board and the two player's pegs (Blue and Red), then clears.
5. The player's hand will be displayed within a menu; use W and S to move up and down and Enter to pick a card (two cards are required to be picked).
6. In debug, the opponent's hand can be chosen as well.
7. Then, the scoring will occur and displayed for the player, opponent, and the crib. The total running score for each is also displayed.
8. The screen will clear, and the pegs will be moved. 
9. Repeat until the score reaches 121 or greater.

## File Summaries
* [__Board.cs__](Board.cs) Creates the board and numbering, in two halves drawn parallel to each other.
* [__Deck.cs__](Deck.cs) Sets up the cards, numbers and suits, and establishes the deck, shuffle, and dealing.
* [__GamePlay.cs__](GamePlay.cs) Dictates the game loop.
* [__Player.cs__](Player.cs) Sets up the player class, including displaying and sorting the hand, choosing cards, and moving the pegs.
* [__Program.cs__](Program.cs) Entry point for the application.
* [__Scoring.cs__](Scoring.cs) Dictates how many pointes each player gets per hand.

## Authors
*[**Brennan Sprague**](https://github.com/b-Sprague) - "Creator"

## License
This project is licensed under the Apache License Version 2.0 - see the [LICENSE](LICENSE) file for details.
