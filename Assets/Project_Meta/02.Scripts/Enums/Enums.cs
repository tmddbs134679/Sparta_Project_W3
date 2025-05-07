using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPLAYERSTATE
{
    IDLE,
    MOVE,
    ATTACK,
    JUMP,
    DEAD
}


public enum EUISTATE
{
    GAMESTART,
    HOME,
    GAME,
    GAMEOVER,
    DIALOGUE,
}

public enum EGAMESTATE
{
    LOBBY,
    MINIGAME
}

public enum ENPCState
{
    Default,
    Simulation
}