/*  Copyright (C) 2024  0x1d7 https://github.com/0x1d7/0x1d7QoL

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
    USA
*/

using BattleTech;
using BattleTech.UI;
using HarmonyLib;
using HBS.Logging;

namespace _0x1d7QoL.Patches
{
    public class Encounter
    {
        private static readonly ILog s_log = Logger.GetLogger(nameof(_0x1d7QoL));

        /* Skips Darius/enemy lance dialogs when a contract begins. No more clicking continue
         * before the fighting begins!
         */
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CombatHUD), "OnTriggerDialog")]
        public static bool NoEncounterDialog(ref MessageCenterMessage message, CombatHUD __instance)
        {
            TriggerDialog triggerDialog = message as TriggerDialog;

            //Are there more? If so, use an array.
            if (triggerDialog.DialogID == "73df8d9c-a274-48fd-98c9-2bd0d7860e83" ||
                    triggerDialog.DialogID == "253a5125-ce8e-4041-9c3d-80b0a3aa86b8")
            {
                s_log.Log("Skpping encounter dialog");

                DialogComplete dialogComplete = new DialogComplete(triggerDialog.DialogID);
                __instance.Combat.MessageCenter.PublishMessage(dialogComplete);
            }

            return false;
        }
    }
}