using UnityEngine;
using System.Collections.Generic;
using System;

namespace Arena
{
    public class DebugGUI : Messager
    {
        void OnGUI()
        {
            GUI.Box(new Rect(10, Screen.height-120, 100, 120), "Loader Menu");
            if (GUI.Button(new Rect(20, Screen.height - 40, 80, 20), "Level 1"))
            {
                app.NotifyGame(GameMessage.LOAD_LEVEL, null, 1);
            }
            if (GUI.Button(new Rect(20, Screen.height - 70, 80, 20), "Level 2"))
            {
                app.NotifyGame(GameMessage.LOAD_LEVEL, null, 2);
            }
            if (GUI.Button(new Rect(20, Screen.height - 100, 80, 20), "Level 3"))
            {
                app.NotifyGame(GameMessage.LOAD_LEVEL, null, 3);
            }

            GUI.Box(new Rect(Screen.width - 120, Screen.height - 160, 120, 160), "God Menu");
            if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 40, 100, 20), "Healh Up"))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //app.NotifyChange(ChangeMessage.SET_MAX_HEALTH, player, 5);
                //app.NotifyChange(ChangeMessage.RESTORE_HEALTH, player);
            }
            if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 70, 100, 20), "Health Restore"))
            {
                //app.NotifyChange(ChangeMessage.RESTORE_HEALTH, GameObject.FindGameObjectWithTag("Player"));
            }
            if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 100, 100, 20), "Shield Up"))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //app.NotifyChange(ChangeMessage.SET_MAX_SHIELD, player, 5);
                //app.NotifyChange(ChangeMessage.RESTORE_SHIELD, player);
            }
            if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 130, 100, 20), "Shield Restore"))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                //app.NotifyChange(ChangeMessage.RESTORE_SHIELD, player);
            }
        }
    }
}
