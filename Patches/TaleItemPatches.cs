using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Il2Cpp;
using ImprovedSignalVoid;
using MelonLoader;

namespace ImprovedTales.Patches
{
    internal class TaleItemPatches
    {

        [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]

        public class RemoveLimitationsOnTaleItems
        {
            public static void Postfix(GearItem __instance)
            {

              //nothing yet
                
            }

        }


    }
}
