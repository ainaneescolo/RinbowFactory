using System;
using UnityEngine;

[Serializable]
public class GameData
{
    [Tooltip("Points made in the Game")] public int pointsMade;
    [Tooltip("Name of the Player 1")] public string namePlayer1;
    [Tooltip("Name of the Player 2")] public string namePlayer2;
}
