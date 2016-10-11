using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Arena
{
    public class ShieldUpdater : StatUpdater
    {
        protected override void Update()
        {
            if (player)
            {
                UpdateIcons(player.GetComponent<Damageable>().GetShieldPoints());
            } else
            {
                base.Update();
            }
        }
    }
}
