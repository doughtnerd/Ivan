using UnityEngine;
using System.Collections;
using System;

namespace Arena
{
    public class GameController : Controller<GameMessage>
    {
        public override void OnNotification(GameMessage message, GameObject obj, params object[] opData)
        {
            throw new NotImplementedException();
        }
    }
}
