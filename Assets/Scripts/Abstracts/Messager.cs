﻿using UnityEngine;
using System.Collections;

namespace Arena
{
    public abstract class Messager : MonoBehaviour
    {
        public ArenaApplication app { get { return ArenaApplication.GetInstance(); } }
    }

    public enum GameMessage
    {
        NEW_GAME, START_GAME, END_GAME, PAUSE_GAME, SAVE_GAME, LOAD_GAME
    }

    public enum ActionMessage
    {
        SHOOT, DIE, SLASH, SHIELD, MOVE
    }

    public enum ChangeMessage
    {
        DAMAGE
    }
}