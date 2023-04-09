using GearSpawner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImprovedSignalVoid.GearSpawns
{
    internal class ItemSpawnManager
    {
        public static void InitializeCustomHandler()
        {
            SpawnTagManager.AddHandler("improvedsignalvoid_narrativespawns", new NarrativeGearSpawnHandler());
        }

    }
}
