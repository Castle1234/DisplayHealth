using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayHealth
{
    public class Main : BaseScript
    {
        private bool isDisplayingHealth = false;

        [Command("displayhealth")]
        public async Task DisplayHealth()
        {
            if (isDisplayingHealth)
            {
                isDisplayingHealth = false;
                API.ClearPrints();
                API.ClearBrief();
                return;
            }
            isDisplayingHealth = true;

            while (isDisplayingHealth)
            {
                var player = Game.PlayerPed.Handle;
                var health = API.GetEntityHealth(player);
                var maxHealth = API.GetEntityMaxHealth(player);
                double healthPercent = (health / maxHealth) * 100;
                var healthRounded = Math.Round(healthPercent, 0);
                var healthString = healthRounded.ToString();
                // display the health in the subtitles
                var message = "Health: " + healthString + "%";
                // display the health indefinitely
                DisplayMessage(message, 0);
                await Task.Delay(1000);
            }
        }

        [Command("flashhealth")]
        public void FlashHealth()
        {
            var player = Game.PlayerPed.Handle;
            var health = API.GetEntityHealth(player);
            var maxHealth = API.GetEntityMaxHealth(player);
            double healthPercent = (health / maxHealth) * 100;
            var healthRounded = Math.Round(healthPercent, 0);
            var healthString = healthRounded.ToString();
            // display the health in the subtitles for 5 seconds then hide it
            var messageFlash = "Health: " + healthString + "%";
            var messageDuration = 5000;
            DisplayMessage(messageFlash, messageDuration);
        }

        public static void DisplayMessage(string message, int time)
        {
            API.ClearPrints();
            API.SetTextEntry_2("STRING");
            API.AddTextComponentString(message);
            API.DrawSubtitleTimed(time, true);
        }
    }
}
