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
using HarmonyLib;
using HBS.Logging;

namespace _0x1d7QoL.Patches
{
    public class SpaceTransition
    {
        private static readonly ILog s_log = Logger.GetLogger(nameof(_0x1d7QoL));

        /* Skips transfer animations in space so you don't have to press Esc
         * except when leaving the jumpship
         */
        [HarmonyPostfix]
        [HarmonyPatch(typeof(SGTravelManager), "BeginTransferAnim")]
        public static void SkipAnimation(SGTravelManager __instance)
        {
            s_log.Log("Skipping travel animations");

            __instance.AnimationInterrupt();
        }

        /* This one will skip the leave jumpship animation but
         * break the UI 
         * 
        [HarmonyPrefix]
        [HarmonyPatch(typeof(SGTravelManager), "AnimationInterrupt")]
        public static void SkipAnimation2(SGTravelManager __instance)
        {
            SimGameState simState = FieldReflection.GetPrivateField<SimGameState>(__instance, "simState");
            simState.CameraController.spaceController.SkipAnimation();
        }
        */
    }

    /* Dead code since we don't need reflection
     * 
    public static class FieldReflection
    {
        public static T GetPrivateField<T>(this object obj, string name)
        {
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            Type type = obj.GetType();
            FieldInfo field = type.GetField(name, flags);
            return (T)field.GetValue(obj);
        }
    }
    */
}
