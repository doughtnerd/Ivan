using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Arena
{
    public class HealthUpdater : StatUpdater
    {

        protected override void Update()
        {
            if (player)
            {
                UpdateIcons(player.GetComponent<Damageable>().GetHealth());
            } else
            {
                base.Update();
            }
        }
    }
}
